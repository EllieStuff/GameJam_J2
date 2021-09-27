using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        gameManager.Reinit();
        Application.targetFrameRate = 60;

    }


    public void PlayGame()
    {
        GameManager.gameState = Const.GameState.PLAYING;
        GameManager.SetCurrScore(0);
        PlayerPrefs.SetInt("currTimeLeft", ClockManager.GetDefaultStartTime());
        PauseMenu.gameIsPaused = false;

        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        if(PlayerPrefs.GetInt("maxScore", 0) < GameManager.GetCurrScore())
        {
            PlayerPrefs.SetInt("maxScore", GameManager.GetCurrScore());
        }

        Debug.Log("Quit");
        Application.Quit();
    }

    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
