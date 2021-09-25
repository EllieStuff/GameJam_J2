using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour
{
    const float
        CLOCK_VOL_AT_DEFAULT = 0.01f,
        CLOCK_VOL_AT_60 = 0.04f,
        CLOCK_VOL_AT_30 = 0.15f,
        CLOCK_VOL_AT_10 = 0.3f,
        CLOCK_VOL_AT_5 = 0.5f;


    private static int DEFAULT_START_TIME = 180;

    private static int timeLeft = DEFAULT_START_TIME;
    private static int startingTimeLeft = timeLeft;

    [SerializeField]
    GameObject stopMenu;
    [SerializeField]
    AudioClip clockClip;
    [SerializeField]
    AudioClip alarmClip;

    private TextMeshProUGUI timerText;
    private AudioSource mainAudio;
    private AudioSource clockAudio;
    //private int musicSpeedUp = 0;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        mainAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        clockAudio = GetComponent<AudioSource>();

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
            if (_timeLeft % 2 == 0 || _timeLeft <= 5)
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
        clockAudio.clip = clockClip;

        if (_timeLeft <= 5)
        {
            mainAudio.pitch = 1.4f;
            clockAudio.volume = CLOCK_VOL_AT_5;
        }
        else if (_timeLeft <= 10)
        {
            mainAudio.pitch = 1.2f;
            clockAudio.volume = CLOCK_VOL_AT_10;
        }
        else if (_timeLeft <= 30)
        {
            mainAudio.pitch = 1.1f;
            clockAudio.volume = CLOCK_VOL_AT_30;
        }
        else if (_timeLeft <= 60)
        {
            mainAudio.pitch = 1.05f;
            clockAudio.volume = CLOCK_VOL_AT_60;
        }
        else
        {
            mainAudio.pitch = 1.0f;
            clockAudio.volume = CLOCK_VOL_AT_DEFAULT;
        }

        clockAudio.loop = true;
        clockAudio.Play();

    }

    void SetMusicState(int _timeLeft)
    {
        if (_timeLeft == 60)
        {
            mainAudio.pitch = 1.05f;
            clockAudio.volume = CLOCK_VOL_AT_60;
        }
        else if (_timeLeft == 30)
        {
            mainAudio.pitch = 1.1f;
            clockAudio.volume = CLOCK_VOL_AT_30;
        }
        else if (_timeLeft == 10)
        {
            mainAudio.pitch = 1.2f;
            clockAudio.volume = CLOCK_VOL_AT_10;
        }
        else if (_timeLeft == 5)
        {
            mainAudio.pitch = 1.4f;
            clockAudio.volume = CLOCK_VOL_AT_5;
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
                    timerText.color = new Color(200, 200, 200);
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


        PlayAlarm();
        EndGame();
        
    }

    private void PlayAlarm()
    {
        clockAudio.volume = 1.0f;
        clockAudio.clip = alarmClip;
        clockAudio.loop = false;
        clockAudio.Play();

        StartCoroutine(WaitAndPlay(3, 3));

    }

    private void EndGame()
    {
        GameManager.gameState = Const.GameState.END_GAME;
        PauseMenu.gameIsPaused = true;
        //Time.timeScale = 0;
        stopMenu.SetActive(true);

        if (PlayerPrefs.GetInt("maxScore", 0) < GameManager.GetCurrScore())
            PlayerPrefs.SetInt("maxScore", GameManager.GetCurrScore());

    }

    IEnumerator WaitAndPlay(float _waitSeconds, float _lerpDuration)
    {
        float targetVolume = mainAudio.volume / 3;
        mainAudio.volume = 0.0f;
        mainAudio.pitch = 0.8f;

        yield return new WaitForSeconds(_waitSeconds);

        float lerpTimer = 0;
        while(lerpTimer < _lerpDuration)
        {
            mainAudio.volume = Mathf.Lerp(0, targetVolume, lerpTimer);

            yield return new WaitForEndOfFrame();
            lerpTimer += Time.deltaTime;
        }


        mainAudio.volume = targetVolume;

    }

}
