using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopMenu : MonoBehaviour
{
    private int lastMaxScore;
    private HideOverTime hideScript;
    private bool thingsUnhiden = false;

    [SerializeField]
    AchievedScore achievedScoreScript;


    private void OnEnable()
    {
        lastMaxScore = PlayerPrefs.GetInt("maxScore", 0);
        hideScript = GetComponent<HideOverTime>();
    }

    private void Update()
    {
        if (!hideScript.GetIsHided() && !thingsUnhiden)
        {
            thingsUnhiden = true;
            achievedScoreScript.SetAchievedScoreText(GameManager.GetCurrScore(), lastMaxScore);
        }
    }


    public void Retry()
    {
        GameManager.gameState = Const.GameState.PLAYING;
        //Time.timeScale = 1f;

        PlayerPrefs.SetInt("currTimeLeft", ClockManager.GetDefaultStartTime());
        GameManager.SetCurrScore(0);


        SceneManager.LoadScene("Game");

    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }

}
