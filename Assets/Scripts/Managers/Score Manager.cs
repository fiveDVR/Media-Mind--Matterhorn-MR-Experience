using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int scoreCounter;
    public TextMeshProUGUI scoreText;


    public void ScoreWin() {
        scoreCounter+=100;
        scoreText.text = scoreCounter.ToString();
    }
}
