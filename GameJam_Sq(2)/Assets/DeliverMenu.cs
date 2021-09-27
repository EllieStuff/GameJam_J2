using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliverMenu : MonoBehaviour
{
    [SerializeField]
    GameObject scoreText;
    
    private float scoreTextSpeed = 4.0f;
    private int scoreTextYMove = 80;
    private Vector3 
        initialScorePos,
        targetScorePos;

    private MoveCamera camera;
    private GameManager gameManager;

    //public GameObject standartUI;

    // Update is called once per frame

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MoveCamera>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        initialScorePos = scoreText.transform.position;
        targetScorePos = new Vector3(initialScorePos.x, initialScorePos.y + scoreTextYMove, initialScorePos.z);
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
        GameManager.gameState = Const.GameState.DELIVERING;

        GameObject.FindGameObjectWithTag("Pizza").GetComponent<PizzaScript>().SetRefreshIngredients(false);
        //Time.timeScale = 0f;

        StartCoroutine(LerpScorePos(initialScorePos, targetScorePos, scoreTextSpeed));
    }

    public void UnloadDeliveryMenu()
    {
        camera.cameraPosition = Const.CameraPositions.TABLE;
        GameManager.gameState = Const.GameState.PLAYING;

        GameObject.FindGameObjectWithTag("Pizza").GetComponent<PizzaScript>().SetRefreshIngredients(true);

        StartCoroutine(LerpScorePos(targetScorePos, initialScorePos, scoreTextSpeed));
    }

    public void RestartScene()
    {
        GameManager.gameState = Const.GameState.PLAYING;
        Time.timeScale = 1f;

        PlayerPrefs.SetInt("currTimeLeft", ClockManager.GetTimeLeft());
        GameManager.SetCurrScore(GameManager.GetCurrScore() + (int)gameManager.CalculateCurrSatifaction(gameManager.GetPizzaIngredients()));


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


    IEnumerator LerpScorePos(Vector3 _initPos, Vector3 _targetPos, float _speed)
    {
        float lerpTimer = 0.0f;
        while (lerpTimer < 1.0f)
        {
            yield return new WaitForEndOfFrame();
            lerpTimer += (Time.deltaTime * _speed);

            scoreText.transform.position = Vector3.Lerp(_initPos, _targetPos, lerpTimer);
        }

        scoreText.transform.position = _targetPos;

    }


}
