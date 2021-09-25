using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour
{
    private static int DEFAULT_START_TIME = 180;

    private static int timeLeft = DEFAULT_START_TIME;
    private static int startingTimeLeft = timeLeft;

    [SerializeField]
    GameObject stopMenu;

    private TextMeshProUGUI timerText;
    private AudioSource audio;
    //private int musicSpeedUp = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();


        timeLeft = PlayerPrefs.GetInt("currTimeLeft", DEFAULT_START_TIME);
        startingTimeLeft = timeLeft;
        StartMusicState(timeLeft);

        StartCoroutine(ClockCoroutine());
    }


    public static int GetDefaultStartTime()
    {
        return DEFAULT_START_TIME;
    }

    public static int GetTimeLeft()
    {
        return timeLeft;
    }

    public static int GetTimeTaken()
    {
        return startingTimeLeft - timeLeft;
    }


    Color GetTimerTextColor(int _timeLeft)
    {
        if(_timeLeft <= 10)
        {
            if (_timeLeft % 2 == 0)
            {
                return Color.red;
            }
            else
            {
                return Color.white;
            }
        }
        else if (_timeLeft <= 30)
        {
            if (_timeLeft % 5 == 0)
            {
                return Color.red;
            }
            else
            {
                return Color.white;
            }
        }
        else if (_timeLeft <= 60)
        {
            if (_timeLeft % 10 == 0)
            {
                return Color.yellow;
            }
            else
            {
                return Color.white;
            }
        }
        else
        {
            if(_timeLeft % 20 == 0)
            {
                return Color.green;
            }
            else
            {
                return Color.white;
            }
        }

    }

    void StartMusicState(int _timeLeft)
    {
        if (_timeLeft <= 5)
        {
            audio.pitch = 1.4f;
        }
        else if (_timeLeft <= 10)
        {
            audio.pitch = 1.2f;
        }
        else if (_timeLeft <= 30)
        {
            audio.pitch = 1.1f;
        }
        else if (_timeLeft <= 60)
        {
            audio.pitch = 1.05f;
        }
        else
        {
            audio.pitch = 1.0f;
        }

    }

    void SetMusicState(int _timeLeft)
    {
        if (_timeLeft == 60)
        {
            audio.pitch = 1.05f;
        }
        else if (_timeLeft == 30)
        {
            audio.pitch = 1.1f;
        }
        else if (_timeLeft == 10)
        {
            audio.pitch = 1.2f;
        }
        else if (_timeLeft == 5)
        {
            audio.pitch = 1.4f;
        }

    }

    IEnumerator ClockCoroutine()
    {
        float clockDelayTimer = 0;
        while(timeLeft >= 0)
        {
            if (!PauseMenu.gameIsPaused)
            {
                timerText.text = timeLeft.ToString();
                if (GameManager.gameState == Const.GameState.DELIVERING)
                {
                    yield return new WaitForEndOfFrame();
                    timerText.color = Color.grey;
                }
                else 
                    timerText.color = GetTimerTextColor(timeLeft);

                SetMusicState(timeLeft);


                if (clockDelayTimer < 1.0f)
                {
                    clockDelayTimer += Time.deltaTime;
                }
                else
                {
                    clockDelayTimer = 0.0f;
                    timeLeft--;
                }
            }

            if (GameManager.gameState == Const.GameState.DELIVERING)
                yield return new WaitForEndOfFrame();

            yield return new WaitForEndOfFrame();

        }

        EndGame();
        
    }

    private void EndGame()
    {
        GameManager.gameState = Const.GameState.END_GAME;
        //Time.timeScale = 0;
        audio.pitch = 1.0f;
        audio.volume = audio.volume / 4;
        stopMenu.SetActive(true);

        if (PlayerPrefs.GetInt("maxScore", 0) < GameManager.GetCurrScore())
            PlayerPrefs.SetInt("maxScore", GameManager.GetCurrScore());

    }

}
