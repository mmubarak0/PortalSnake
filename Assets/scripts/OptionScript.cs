using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject PauseUI;
    public GameObject OptionUI;
    Player mody = new Player("mody", 3);

    private void Start() {
        OptionUI = transform.GetChild(0).gameObject;
    }

    public void Setting()
    {
        GameUI.SetActive(false);
        PauseUI.SetActive(false);
        OptionUI.SetActive(true);
    }

    public void Savesetting()
    {
        // reading the input from the readinput script
        GameObject theInput = GameObject.Find("readinput");
        ReadInput op = theInput.GetComponent<ReadInput>();
        int speed = op.getInputlev();
        int initsize = op.getInputinit();

        // assigning the value to player class to save it for the next play
        mody.setSpeed(speed);
        mody.setInitSize(initsize);

        OptionUI.SetActive(false);
        PauseUI.SetActive(true);
    }
}
