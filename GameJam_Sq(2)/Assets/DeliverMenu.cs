using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliverMenu : MonoBehaviour
{
    private MoveCamera camera;

    //public static bool gameIsPaused = false;

    //public GameObject pauseMenuUI;

    // Update is called once per frame

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>();
    }

    //public void Resume()
    //{
    //    pauseMenuUI.SetActive(false);
    //    Time.timeScale = 1f;
    //    gameIsPaused = false;
    //}

    //void Pause()
    //{
    //    pauseMenuUI.SetActive(true);
    //    Time.timeScale = 0f;
    //    gameIsPaused = true;
    //}

    public void LoadDeliveryMenu()
    {
        camera.cameraPosition = Const.CameraPositions.FURNANCE;
        //Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");

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
