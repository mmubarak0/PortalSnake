using UnityEngine;
using UnityEngine.UI;

public class Wscore : MonoBehaviour
{
    [SerializeField] private Text Score;
    int finalScore = 1;
    Player mody = new Player("mody", 3);
    // Update is called once per frame
    private void Update()
    {
        GameObject thePlayer = GameObject.Find("Player");
        PlayerMovement playerScript = thePlayer.GetComponent<PlayerMovement>();
        finalScore = playerScript.score;
        Score.text = "Score = " + finalScore.ToString();
        updateScore();
    }

    private void updateScore()
    {
        mody.setScore(finalScore);
    }
}
