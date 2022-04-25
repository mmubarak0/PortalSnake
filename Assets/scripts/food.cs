using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    [HideInInspector] public int xpos;
    [HideInInspector] public int ypos;
    [SerializeField] int levelmin = 0;
    public int levelmax = 1;
    [HideInInspector] public int a;
    public List<BoxCollider2D> foodArea = new List<BoxCollider2D>();
    public GameObject stairs;
    [HideInInspector] public List<Bounds> bounds = new List<Bounds>();
    public GameObject parti;
    public int nextLevel;
    Player mody = new Player("mody", 3);

    private void Start()
    {
        for (int i = 0; i < foodArea.Count; i++)
        {
            bounds.Add(this.foodArea[i].bounds);
        }

        parti.GetComponent<ParticleSystem>().Stop();
        RandomizePosition();

        stairs.SetActive(true);
        stairs.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(particlePlay());
        }
        else if (other.gameObject.tag == "playerBody")
        {
            RandomizePosition();
        } else if (other.gameObject.tag == "Ground")
        {
            RandomizePosition();
        }
    }
    private void RandomizePosition()
    {
        if (mody.getScore() >= 0 && mody.getScore() < 10)
        {
            levelmax = 2;
            stairs.transform.GetChild(levelmax - 1).gameObject.SetActive(false);
        }
        else if (mody.getScore() > 10 && mody.getScore() < 20)
        {
            levelmin = 1;
            levelmax = 3;
            stairs.transform.GetChild(levelmax - 1).gameObject.SetActive(false);
            stairs.transform.GetChild(levelmin - 1).gameObject.SetActive(true);
        } else if (mody.getScore() > 20 && mody.getScore() < 30)
        {
            levelmin = 2;
            levelmax = 4;
            stairs.transform.GetChild(levelmax - 1).gameObject.SetActive(false);
            stairs.transform.GetChild(levelmin - 1).gameObject.SetActive(true);
        } else if (mody.getScore() > 30 && mody.getScore() < 40)
        {
            levelmin = 3;
            levelmax = 5;
            stairs.transform.GetChild(levelmax - 1).gameObject.SetActive(false);
            stairs.transform.GetChild(levelmin - 1).gameObject.SetActive(true);
        } else if (mody.getScore() > 40 && mody.getScore() < 50)
        {
            levelmin = 4;
            levelmax = 6;
            stairs.transform.GetChild(levelmax - 1).gameObject.SetActive(false);
            stairs.transform.GetChild(levelmin - 1).gameObject.SetActive(true);
        } else if (mody.getScore() > 50 && mody.getScore() < 60)
        {
            levelmin = 5;
            levelmax = 7;
            stairs.transform.GetChild(levelmax - 1).gameObject.SetActive(false);
            stairs.transform.GetChild(levelmin - 1).gameObject.SetActive(true);
        } else if (mody.getScore() > 50)
        {
            levelmin = 0;
            levelmax = 7;
            stairs.gameObject.SetActive(false);
        }

        a = Random.Range(levelmin, levelmax);

        xpos = ((int)Random.Range(bounds[((int)a)].min.x, bounds[((int)a)].max.x));
        ypos = ((int)Random.Range(bounds[((int)a)].min.y, bounds[((int)a)].max.y));

        Debug.Log("the food is at position (" + xpos + ", " + ypos + ")");

        this.transform.position = new Vector3(xpos, ypos, 0.0f);
    }

    IEnumerator particlePlay()
    {
        parti.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(.2f);
        RandomizePosition();
    }

}
