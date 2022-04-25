using UnityEngine;
using UnityEngine.UI;

public class finalscore : MonoBehaviour
{
    [SerializeField] private Text Score;
    Player mody = new Player("mody", 3);
    private void Update()
    {

        Score.text = "Your Final score is " + mody.getScore().ToString();
    }
}
