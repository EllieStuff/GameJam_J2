using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliverMenu : MonoBehaviour
{
    private MoveCamera camera;
    private GameManager gameManager;

    //public GameObject standartUI;

    // Update is called once per frame

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
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
        //standartUI.SetActive(false);
        camera.cameraPosition = Const.CameraPositions.FURNANCE;
        GameObject.FindGameObjectWithTag("Pizza").GetComponent<PizzaScript>().SetRefreshIngredients(false);
        //Time.timeScale = 0f;
    }

    public void UnloadDeliveryMenu()
    {
        camera.cameraPosition = Const.CameraPositions.TABLE;
        GameObject.FindGameObjectWithTag("Pizza").GetComponent<PizzaScript>().SetRefreshIngredients(true);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        //gameManager.gameState = Const.GameState.PLAYING;
        SceneManager.LoadScene("Game");
        //gameManager.Reinit();

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
