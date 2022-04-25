using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levelselector : MonoBehaviour
{
    void Update()
    {
        GameObject theFood = GameObject.Find("food");
        food foodScript = theFood.GetComponent<food>();
        int nextLevel = foodScript.levelmax - 1;
        this.GetComponent<Text>().text = "Level " + nextLevel.ToString();
    }

}
