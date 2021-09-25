using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOverTime : MonoBehaviour
{
    public GameObject goToHide;
    public float hidingTime = 5.0f;

    private bool hided = false;

    // Start is called before the first frame update
    void Start()
    {
        goToHide.SetActive(false);
        StartCoroutine(HideOverTimeCoroutine());

    }


    public bool GetIsHided()
    {
        return hided;
    }


    IEnumerator HideOverTimeCoroutine()
    {
        yield return new WaitForSeconds(hidingTime);
        goToHide.SetActive(true);

    }

}
