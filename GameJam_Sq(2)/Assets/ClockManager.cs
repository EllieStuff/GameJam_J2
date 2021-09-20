using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockManager : MonoBehaviour
{
    private static int DEFAULT_START_TIME = 180;

    private static int timeLeft = DEFAULT_START_TIME;

    private TextMeshProUGUI timerText;


    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        
        timeLeft = PlayerPrefs.GetInt("currTimeLeft", DEFAULT_START_TIME);

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


    IEnumerator ClockCoroutine()
    {
        float clockDelayTimer = 0;
        while(timeLeft >= 0)
        {
            if (!PauseMenu.gameIsPaused)
            {
                timerText.text = timeLeft.ToString();
                timerText.color = GetTimerTextColor(timeLeft);

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

            yield return new WaitForEndOfFrame();
        }



        // ToDo: End Game stuff
        // ...
        // ...
        // ...

        
    }

}
