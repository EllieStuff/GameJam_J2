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
        PauseMenu.gameIsPaused = false;

        PlayerPrefs.SetInt("currTimeLeft", ClockManager.GetDefaultStartTime());
        GameManager.SetCurrScore(0);


        SceneManager.LoadScene("Game");

    }

    public void LoadMenu()
    {
        PauseMenu.gameIsPaused = false;

        SceneManager.LoadScene("Menu");
    }

}
