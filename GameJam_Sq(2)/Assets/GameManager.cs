using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static Const.GameState gameState = Const.GameState.INIT;
    
    private static float currScore = 0;
    private static float currSatisfaction = 0;
    
    //private int roundNum = 0;
    private int rareCostumerRatio = 10;
    //private int numOfClients = 0;
    //private float currSatisfaction = 0.0f;
    //private float maxSatisfaction = 0.0f;
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
        //new IngredientClass("Species", true, null)

    };

    private TextMeshProUGUI recipeText;
    private List<Utils.TMProName> namesToChange;
    private PizzaScript pizzaScript;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        Reinit();
    }

    public void Reinit()
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
                //Debug.Log(roundNum);
                //roundNum++;

                recipeText = GameObject.Find("Pizza_Recipe_Text").GetComponent<TextMeshProUGUI>();
                pizzaScript = GameObject.FindGameObjectWithTag("Pizza").GetComponent<PizzaScript>();
                GenerateGoalIngredientsList();
                SetRecipe(goalIngrendients);
                CalculateCurrSatifaction(new List<string>());

                //StartCoroutine(GenerateGoalIngredientsCoroutine());

                break;


            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == Const.GameState.PLAYING && recipeText != null)
            RefreshRecipe(goalIngrendients);
    }

    //IEnumerator GenerateGoalIngredientsCoroutine()
    //{
    //    while (true)
    //    {
    //        GenerateGoalIngredientsList();
    //        SetRecipe(goalIngrendients);
    //        //PrintIngredientsList(goalIngrendients);
    //        yield return new WaitForSeconds(5);
    //    }
    //}

    //void PrintIngredientsList(List<IngredientClass> _list)
    //{
    //    Debug.Log("New Goal ingredients:");
    //    foreach(IngredientClass ingredient in _list)
    //    {
    //        Debug.Log(ingredient.name);
    //    }
    //    Debug.Log(" ----- ");
    //}

    void SetRecipe(List<IngredientClass> _list)
    {
        recipeText.text = "Recipe: ";

        for (int i = 0; i < _list.Count - 1; i++)
        {
            recipeText.text += _list[i].name + ", ";
        }

        recipeText.text += _list[_list.Count - 1].name + ".";

    }

    private void RefreshRecipe(List<IngredientClass> _goalList)
    {
        recipeText.text = "Recipe: ";

        namesToChange = new List<Utils.TMProName>();
        string currIngredientName;
        int totalWordsAmmount = Utils.GetWordsAmmount(recipeText.text);
        int nameWordsAmmount;
        for (int i = 0; i < _goalList.Count - 1; i++)
        {
            currIngredientName = _goalList[i].name;
            if (CheckIfCurrIngredient(currIngredientName))
            {
                nameWordsAmmount = Utils.GetWordsAmmount(currIngredientName);
                namesToChange.Add(new Utils.TMProName(currIngredientName, Color.green, nameWordsAmmount, totalWordsAmmount));
            }

            recipeText.text += currIngredientName + ", ";
            totalWordsAmmount = Utils.GetWordsAmmount(recipeText.text);
        }

        currIngredientName = _goalList[_goalList.Count - 1].name;
        if (CheckIfCurrIngredient(currIngredientName))
        {
            nameWordsAmmount = Utils.GetWordsAmmount(currIngredientName);
            namesToChange.Add(new Utils.TMProName(currIngredientName, Color.green, nameWordsAmmount, totalWordsAmmount));
        }
        recipeText.text += currIngredientName + ".";


        recipeText.ForceMeshUpdate();
        Utils.PaintTMProWords(recipeText, namesToChange);
        recipeText.UpdateVertexData();

    }


   #region INGREDIENTS_LIST_REGION
    private void GenerateGoalIngredientsList()
    {
        int listMaxRange = 6;
        int ingredientsNum = Random.RandomRange(2, listMaxRange);
        bool rareCostumer = Random.RandomRange(0, rareCostumerRatio) == 0;

        goalIngrendients = new List<IngredientClass>(ingredientsNum);
        List<int> idsUsed = new List<int>(ingredientsNum);
        for (int i = 0; i < ingredientsNum; i++)
        {
            int ingredientId = Random.RandomRange(0, fullIngrendientsList.Count);
            if (!rareCostumer && !fullIngrendientsList[ingredientId].edible)
            {
                do
                {
                    ingredientId = Random.RandomRange(0, fullIngrendientsList.Count);
                } while ((!rareCostumer && !fullIngrendientsList[ingredientId].edible)
                        || (Utils.FindInList<int>(idsUsed, ingredientId) >= 0));
            }
            else
            {
                while (Utils.FindInList<int>(idsUsed, ingredientId) >= 0)
                {
                    ingredientId = Random.RandomRange(0, fullIngrendientsList.Count);
                }
            }

            goalIngrendients.Add(fullIngrendientsList[ingredientId]);
            idsUsed.Add(ingredientId);
        }

    }

    public List<IngredientClass> GetGoalIngredientsList()
    {
        return goalIngrendients;
    }

    public List<string> GetPizzaIngredients()
    {
        return pizzaScript.GetCurrIngredients();
    }

    public bool CheckIfGoalIngredient(string _ingredientName)
    {
        foreach(IngredientClass ingredient in goalIngrendients)
        {
            if (_ingredientName == ingredient.name)
            {
                //ToDo: Marcar ingredient en verd a la currIngredientsList
                return true;
            }
        }

        return false;
    }
    public bool CheckIfCurrIngredient(string _ingredientName)
    {
        //string targetName = " - " + _ingredientName;
        foreach (string ingredientName in pizzaScript.GetCurrIngredients())
        {
            if (_ingredientName == ingredientName)
            {
                //ToDo: Marcar ingredient en verd a la goalList
                return true;
            }
        }

        return false;
    }


    #endregion


    #region SCORE_REGION




    public float CalculateCurrSatifaction(List<string> _finalIngredients)
    {
        float tmpScore = 0.0f;

        float numPercentatge = 0.2f;
        float ingredientsCoincidencePercentatge = 0.8f;
        float speedPercentatge = 0.2f;


        // Num
        int ingredientsDiff = Mathf.Abs(_finalIngredients.Count - goalIngrendients.Count);
        float numPunctuation = (((goalIngrendients.Count - ingredientsDiff) * numPercentatge) * 10) / (goalIngrendients.Count);
        if (numPunctuation < 0) numPunctuation = 0;
        if (_finalIngredients.Count == 0) return -10;

        // Coincidence
        int currCoincidences = 0;
        bool minimumFlag = false;
        foreach (IngredientClass ingredient in goalIngrendients)
        {
            int elementId = Utils.FindInList<string>(_finalIngredients, ingredient.name);
            if (elementId >= 0)
            {
                currCoincidences++;
                _finalIngredients.RemoveAt(elementId);
                minimumFlag = true;
            }
            else
            {
                currCoincidences--;
            }
        }
        float ingredientsCoincidencePunctuation = ((currCoincidences * ingredientsCoincidencePercentatge) * 10) / (goalIngrendients.Count);
        //if (ingredientsCoincidencePunctuation < 0) ingredientsCoincidencePunctuation = 0;
        //if (ingredientsCoincidencePunctuation < 0.1f) ingredientsCoincidencePunctuation = -5 * (goalIngrendients.Count - currCoincidences);
        if (!minimumFlag) return -20;

        // Speed
        float expectedMedianSpeed = 5.0f;
        float bestExpectedTime = goalIngrendients.Count * expectedMedianSpeed;
        float realTakenTime = ClockManager.GetTimeTaken();

        float speedPunctuation;
        if(bestExpectedTime >= realTakenTime)
        {
            speedPunctuation = speedPercentatge * 10;
        }
        else
        {
            float timeDiff = realTakenTime - bestExpectedTime;
            speedPunctuation = ((bestExpectedTime - timeDiff) * speedPercentatge * 10) / bestExpectedTime;
            if (speedPunctuation < 0) speedPunctuation = 0;
        }


        // Total
        tmpScore = currSatisfaction = (numPunctuation + ingredientsCoincidencePunctuation + speedPunctuation) * 10;

        return tmpScore;
    }

    public static float GetCurrSatisfaction()
    {
        return currSatisfaction;
    }

    public static void SetCurrScore(int _currScore)
    {
        currScore = _currScore;
    }

    public static int GetCurrScore()
    {
        return (int)currScore;
    }


    //public float GetCurrSatisfaction()
    //{
    //    return currSatisfaction;
    //}

    #endregion



}
