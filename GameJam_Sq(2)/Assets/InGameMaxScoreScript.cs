using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameMaxScoreScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SetScoreText(GameManager.GetCurrScore());

    }

    public void SetScoreText(int _score)
    {
        GetComponent<TextMeshProUGUI>().text = "Curr. Score: " + _score.ToString();
    }

}
