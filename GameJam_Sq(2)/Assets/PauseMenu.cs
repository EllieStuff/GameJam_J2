using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField]
    AudioSource mainAudio;
    [SerializeField]
    AudioSource clockAudio;

    public GameObject pauseMenuUI;

    private float mainVolumeCopy, clockVolumeCopy;
    private float mainPitchCopy, clockPitchCopy;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        mainAudio.volume = mainVolumeCopy;
        mainAudio.pitch = mainPitchCopy;
        clockAudio.volume = clockVolumeCopy;
        clockAudio.pitch = clockPitchCopy;

        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

        mainVolumeCopy = mainAudio.volume;
        mainPitchCopy = mainAudio.pitch;
        clockVolumeCopy = clockAudio.volume;
        clockPitchCopy = clockAudio.pitch;

        mainAudio.volume = mainAudio.volume / 3;
        mainAudio.pitch = 0.8f;
        clockAudio.volume = 0.01f;
        clockAudio.pitch = 0.8f;
    }

    public void LoadMenu()
    {
        GameManager.gameState = Const.GameState.MAIN_MENU;
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
