using UnityEngine;
using UnityEngine.UI;

public class instruction : MonoBehaviour
{
    [SerializeField] private Text instruc;
    public GameObject Player;

    private void Update() {
        GameObject thefood = GameObject.Find("food");
        food foodScript = thefood.GetComponent<food>();
        float x = Mathf.Round(foodScript.xpos);
        float y = Mathf.Round(foodScript.ypos);
        instruc.text = "the food is at position (" + x.ToString() + ", " + y.ToString() + ")";
        if (Player.transform.position.x > x) {
            if (Player.transform.position.y > y) {
                instruc.text = "Move down \nor Move left";
            } else {
                instruc.text = "Move up \nor Move left";
            }
        } else {
            if (Player.transform.position.y > y) {
                instruc.text = "Move down \nor Move right";
            } else {
                instruc.text = "Move up \nor Move right";
            }
        } 

    }
}
