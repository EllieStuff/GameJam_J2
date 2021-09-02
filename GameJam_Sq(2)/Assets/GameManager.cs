using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Const.GameState gameState = Const.GameState.INIT;

    private int rareCostumerRatio = 10;
    private int numOfClients = 0;
    private float currSatisfaction = 0.0f;
    private float maxSatisfaction = 0.0f;
    private List<IngredientClass> goalIngrendients;
    private List<IngredientClass> fullIngrendientsList = new List<IngredientClass>()
    {
        // Aquests no fan falta???
        //new IngredientClass("Pizza", true, null),
        //new IngredientClass("Oven", false, null),
        //new IngredientClass("Table", false, null),

        // No comestibles
        new IngredientClass("Pan", false, new List<string>(){ "Kitchen Knife" }),
        new IngredientClass("Plate", false, new List<string>(){ "Board" }),
        new IngredientClass("Board", false, new List<string>(){ "Plate" }),
        new IngredientClass("Kitchen Knife", false, new List<string>(){ "Pan" }),

        // Comestibles
        new IngredientClass("Pineapple", true, null),
        new IngredientClass("Pineapple Slice", true, null),
        new IngredientClass("Cooked Pineapple", true, null),
        new IngredientClass("Cooked Pineapple Slice", true, new List<string>(){ "Pineapple Slice", "Cooked Pineapple", "Pineapple" }),

        new IngredientClass("Tomato", true, null),
        new IngredientClass("Tomato Slice", true, null),
        new IngredientClass("Cooked Tomato", true, null),
        new IngredientClass("Cooked Tomato Slice", true, new List<string>(){ "Tomato Slice", "Cooked Tomato", "Tomato" }),
        
        new IngredientClass("Cheese", true, new List<string>(){ "Cheese Slice", "Cooked Cheese", "Cooked Cheese Slice" }),
        new IngredientClass("Cheese Slice", true, new List<string>(){ "Cooked Cheese Slice", "Cheese Slice", "Cooked Cheese" }),
        new IngredientClass("Cooked Cheese", true, null),
        new IngredientClass("Cooked Cheese Slice", true, null),

        new IngredientClass("Carrot", true, null),
        new IngredientClass("Carrot Slice", true, null),
        new IngredientClass("Cooked Carrot", true, null),
        new IngredientClass("Cooked Carrot Slice", true, null),

        new IngredientClass("Lettuce", true, null),
        new IngredientClass("Lettuce Leaf", true, null),
        new IngredientClass("Cooked Lettuce", true, null),
        new IngredientClass("Cooked Lettuce Leaf", true, null),

        new IngredientClass("Ham", true, null),
        new IngredientClass("Ham Slice", true, null),
        new IngredientClass("Cooked Ham", true, null),
        new IngredientClass("Cooked Ham Slice", true, null),

        new IngredientClass("Bacon", true, null),
        new IngredientClass("Bacon Slice", true, null),
        new IngredientClass("Cooked Bacon", true, null),
        new IngredientClass("Cooked Bacon Slice", true, null),

        new IngredientClass("Fish", true, null),
        new IngredientClass("Fish Slice", true, null),
        new IngredientClass("Cooked Fish", true, null),
        new IngredientClass("Cooked Fish Slice", true, null),

        new IngredientClass("Fish", true, null),
        new IngredientClass("Fish Slice", true, null),
        new IngredientClass("Cooked Fish", true, null),
        new IngredientClass("Cooked Fish Slice", true, null),

        new IngredientClass("Sausage", true, null),
        new IngredientClass("Sausage Slice", true, null),
        new IngredientClass("Cooked Sausage", true, null),
        new IngredientClass("Cooked Sausage Slice", true, null),

        // Altres
        new IngredientClass("Species", true, null)

    };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        switch (gameState)
        {
            case Const.GameState.INIT:
                gameState = Const.GameState.MAIN_MENU;
                SceneManager.LoadScene("Menu");

                break;

            case Const.GameState.MAIN_MENU:
                // Whatever we need

                break;

            case Const.GameState.PLAYING:
                // Whatever we need

                break;


            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   #region INGREDIENTS_LIST_REGION
    private void GenerateGoalIngredientsList()
    {
        int listMaxRange = fullIngrendientsList.Count - (fullIngrendientsList.Count / 10);
        if (listMaxRange < 2) 
            listMaxRange = 2;
        int ingredientsNum = Random.RandomRange(1, listMaxRange);
        bool rareCostumer = Random.RandomRange(0, rareCostumerRatio) == 0;

        goalIngrendients = new List<IngredientClass>(ingredientsNum);
        for (int i = 0; i < ingredientsNum; i++)
        {
            int ingredientId = Random.RandomRange(0, fullIngrendientsList.Count);
            if (!rareCostumer && !fullIngrendientsList[ingredientId].edible)
            {
                do
                {
                    ingredientId = Random.RandomRange(0, fullIngrendientsList.Count);
                } while (!fullIngrendientsList[ingredientId].edible);
            }
        }

    }

    public List<IngredientClass> GetGoalIngredientsList()
    {
        return goalIngrendients;
    }


   #endregion


   #region SCORE_REGION
    public float CalculateCurrSatifaction(List<string> finalIngredients)
    {
        float tmpScore = 0.0f;
        
        float numPercentatge = 4.0f;
        int ingredientsDiff = Mathf.Abs(finalIngredients.Count - goalIngrendients.Count);
        float numPunctuation = (ingredientsDiff * numPercentatge) / (goalIngrendients.Count);

        
        float ingredientsCoincidencePercentatge = 6.0f;
        int currCoincidences = 0;
        foreach(IngredientClass ingredient in goalIngrendients)
        {
            int elementId = Utils.FindInList<string>(finalIngredients, ingredient.name);
            if (elementId >= 0)
            {
                currCoincidences++;
                finalIngredients.RemoveAt(elementId);
            }
        }
        //int ingredientsCoincidenceDiff = Mathf.Abs(finalIngredients.Count - goalIngrendients.Count);  //crec que aquesta no
        float ingredientsCoincidencePunctuation = (currCoincidences * ingredientsCoincidencePercentatge) / (goalIngrendients.Count);

        tmpScore = numPunctuation + ingredientsCoincidencePunctuation;
        currSatisfaction = (currSatisfaction * (numOfClients - 1) + tmpScore) / numOfClients;   //ToDo: Add new client when pizza delivered

        if(currSatisfaction > maxSatisfaction && numOfClients > 0)
        {
            //ToDo: Fer mitjana amb satisfaccio i numero de clients

        }


        return tmpScore;
    }

    public float GetCurrSatisfaction()
    {
        return currSatisfaction;
    }

   #endregion



}
