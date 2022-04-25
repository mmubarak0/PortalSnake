using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    int speed = 3;
    public int Size = 3;
    public float Timer = 60f;
    public Text TimerUI;
    // bool alive = true;
    bool HeNotDie = true;
    private bool portalClosed = false;
    [HideInInspector] public int score = 1;
    public Transform PlayerBody;
    public float speedFactor = 1.0f;
    private Vector2 direction = Vector2.right;
    public List<Transform> PlayerBodyList = new List<Transform>();
    public AudioManager[] soundlist;
    public List<Transform> Blacks = new List<Transform>();
    public List<Transform> Whites = new List<Transform>();
    public List<Transform> Reds = new List<Transform>();
    public List<Transform> Blues = new List<Transform>();
    public float AB=0f, BC=0f, CD=0f;
    Player mody = new Player("mody", 3);

    private void Awake()
    {
        /*
        it is the first function to be excecuted in the game 
        */

        // Adding audio source component to every song in the sounds list 
        foreach (AudioManager s in soundlist)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }

    }

    private void Start()
    {
        /*
        start is called once before the first frame update
        */

        // printing the Score value to console
        Debug.Log("Score = " + score);
        // instantiating the player with initial size and speed
        startPlaying();
        for (int i = 0; i < Blacks.Count; i++)
        {
            // Debug.Log(black[Blacks[i].gameObject.name] + " " + Blacks[i].gameObject.name);
        }
    }

    private void Update()
    {
        /*
        update function is called at every frame update not in a fixed time 
        */

        // change the variable direction based on the input of the user
        changeDirection();

        mody.setScore(score);
        Timefunction();
    }

    private void FixedUpdate()
    {
        for (int i = PlayerBodyList.Count - 1; i > 0; i--)
        {
            PlayerBodyList[i].position = PlayerBodyList[i - 1].position;
        }
        this.transform.position = new Vector3(
            ((int) this.transform.position.x) + ((int) direction.x * speed),
            ((int) this.transform.position.y) + ((int) direction.y * speed),
            0.0f
        );

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        /*
        this function gets excecuted when the player hit something
        */

        // this is how to check for the collider object type
        // you can also use tag instead of using name
        if (col.gameObject.name == "food")
        {
            playsound("eat");
            if (score % 5 == 0)
            {
                // score += 5;
                for (int i = 1; i < Size; i++)
                {
                    CreateNewBody();
                }
            }
            else
            {
                CreateNewBody();
            }
            Debug.Log("Score = " + ++score);
        }
        else if (col.gameObject.tag == "playerBody" || col.gameObject.tag == "Ground")
        {
            StartCoroutine(playdeath("death"));
        } else if (col.gameObject.tag == "Shadow")
        {
            // inShadow = true;
        }
        
        switch (col.gameObject.tag)
        {
            case ("Black"):
                for (int i = 0; i < Blacks.Count; i++)
                {
                    if (col.gameObject.name == Blacks[i].gameObject.name && portalClosed == false)
                    {
                        Debug.Log(Blacks[i].gameObject.name);
                        StartCoroutine(BlackPortal(i));
                    }
                }
                break;
            case ("White"):
                for (int i = 0; i < Whites.Count; i++)
                {
                    if (col.gameObject.name == Whites[i].gameObject.name && portalClosed == false)
                    {
                        Debug.Log(Whites[i].gameObject.name);
                        StartCoroutine(WhitePortal(i));
                    }
                }
                break;
            case ("Red"):
                for (int i = 0; i < Reds.Count; i++)
                {
                    if (col.gameObject.name == Reds[i].gameObject.name && portalClosed == false)
                    {
                        Debug.Log(Reds[i].gameObject.name);
                        StartCoroutine(RedPortal(i));
                    }
                }
                break;
            case ("Blue"):
                for (int i = 0; i < Blues.Count; i++)
                {
                    if (col.gameObject.name == Blues[i].gameObject.name && portalClosed == false)
                    {
                        Debug.Log(Blues[i].gameObject.name);
                        StartCoroutine(BluePortal(i));
                    }
                }
                break;
        }
    }

    private void CreateNewBody()
    {
        /*
        This function creates new player bodies at the position of the head
        then add them to the player body list
        */

        // create new body of type transform
        Transform newbody = Instantiate(this.PlayerBody);

        // get the created body at the position of the previous body in the body list
        newbody.position = PlayerBodyList[PlayerBodyList.Count - 1].position;

        // adding the player to the body list
        PlayerBodyList.Add(newbody);
    }
    private void playerDead()
    {
        /*
        This function kills the player instantly and remove the gameobjects
        and clear the list then move the player to lose screen
        */

        // first I distroyed the instantianted gameObjects in the body list
        // except the head of the body so that is why i started at 1 index
        for (int i = 1; i < PlayerBodyList.Count; i++)
        {
            Destroy(PlayerBodyList[i].gameObject);
        }

        // clear the list including the head
        // will add it later in the start playing function
        PlayerBodyList.Clear();

        // this line pause the game
        // Time.timeScale = 0f;

        // Loading the lose screen scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void startPlaying()
    {
        /*
        this function used to create the player initial size
        and this add all the body part to to the playerbody list
        if the player change some setting on pause menu it will
        store them on the player class
        */

        // reading the input from the readinput script
        GameObject theInput = GameObject.Find("readinput");
        ReadInput op = theInput.GetComponent<ReadInput>();
        int speed = op.getInputlev();
        int initsize = op.getInputinit();

        // assigning the value to player class to save it for the next play
        mody.setSpeed(speed);
        mody.setInitSize(initsize);

        // using the stored setting in the player class 
        if (mody.getInitSize() > 1) { Size = mody.getInitSize(); }
        if (mody.getSpeed() == 1) { Time.fixedDeltaTime = 0.1f; }
        else if (mody.getSpeed() == 2) { Time.fixedDeltaTime = 0.09f; }
        else if (mody.getSpeed() == 3) { Time.fixedDeltaTime = 0.08f; }
        else if (mody.getSpeed() == 4) { Time.fixedDeltaTime = 0.07f; }
        else if (mody.getSpeed() == 5) { Time.fixedDeltaTime = 0.06f; }
        else if (mody.getSpeed() == 6) { Time.fixedDeltaTime = 0.05f; }
        else if (mody.getSpeed() == 7) { Time.fixedDeltaTime = 0.04f; }
        else if (mody.getSpeed() == 8) { Time.fixedDeltaTime = 0.03f; }
        else { Time.fixedDeltaTime = 0.03f; }

        // adding the HEAD or the player to the beggining of the list 
        // so that other body object can follow it
        // and let it is position at zero 
        PlayerBodyList.Add(this.transform);
        this.transform.position = new Vector3(AB, BC, CD);

        // creating the body objects at the initial size given by Size variable
        for (int i = 1; i < Size; i++)
        {
            PlayerBodyList.Add(Instantiate(this.PlayerBody));
        }

        // set the score back to 1 
        score = 1;
        Debug.Log("Game Started");
    }

    IEnumerator playdeath(string name)
    {
        /*
        This is a Coroutine function I used for the following 
            to play the sound desired by it's name
            then wait for one second to kill the player and display lose scene
        */
        if (HeNotDie){playsound(name);}
        HeNotDie = false;
        yield return new WaitForSeconds(2f);
        playerDead();
    }

    public void playsound(string name)
    {
        /* 
        Play the sound with it is name as a parameter 
        */
        AudioManager s = Array.Find(soundlist, soundlist => soundlist.name == name);
        s.source.Play();
    }

    void changeDirection()
    {
        /*
        This function takes no parameter it only do the following 
        1- if the player press one of the arrow keys lets say arrow down
        2- this function catch the input as the player press
        3- then check the condition of the button that being pressed
        4- change the direction variable based on it
        5- direction variable type is Vector2
        6- Vector2 has two properties x and y 
        7- which increase the value of position x and y when calling direction
        8- then when i say Vector2.up/down I will increase the postion in the y 
        9- I did also check for the reversed direction to not let the player 
            move along his body direction
        */
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
    }

    IEnumerator BlackPortal(int j)
    {
        int s = j + 1;
        if (s > 35)
        {
            s = 0;
        }
        this.transform.position = new Vector3(Blacks[s].position.x, Blacks[s].position.y, Blacks[s].position.z);
        portalClosed = true;
        yield return new WaitForSeconds(.5f);
        portalClosed = false;
    }

    IEnumerator WhitePortal(int j)
    {
        int s = j + 1;
        if (s > 35)
        {
            s = 0;
        }
        this.transform.position = new Vector3(Whites[s].position.x, Whites[s].position.y, Whites[s].position.z);
        portalClosed = true;
        yield return new WaitForSeconds(.5f);
        portalClosed = false;
    }

    IEnumerator RedPortal(int j)
    {
        int s = j + 1;
        if (s > 35)
        {
            s = 0;
        }
        this.transform.position = new Vector3(Reds[s].position.x, Reds[s].position.y, Reds[s].position.z);
        portalClosed = true;
        yield return new WaitForSeconds(.5f);
        portalClosed = false;
    }

    IEnumerator BluePortal(int j)
    {
        int s = j + 1;
        if (s > 35)
        {
            s = 0;
        }
        this.transform.position = new Vector3(Blues[s].position.x, Blues[s].position.y, Blues[s].position.z);
        portalClosed = true;
        yield return new WaitForSeconds(.5f);
        portalClosed = false;
    }

    void Timefunction()
    {
            Timer -= Time.deltaTime * speedFactor;
            TimerUI.text = "Time Left : " + Timer.ToString("F0");
    }

}