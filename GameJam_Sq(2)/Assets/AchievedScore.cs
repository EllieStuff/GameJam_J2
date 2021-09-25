using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievedScore : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }


    public void SetAchievedScoreText(int _achievedScore, int _lastMaxScore)
    {
        if (_lastMaxScore < _achievedScore)
            scoreText.text = "Your Score was... " + _achievedScore.ToString() + "! <br> You beat your record!!";
        else
            scoreText.text = "Your Score was... " + _achievedScore.ToString() + "!";
    }

}
