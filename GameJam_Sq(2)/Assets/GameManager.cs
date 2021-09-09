using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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

    private TextMeshProUGUI recipeText;
    private List<Utils.TMProName> namesToChange;

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
                recipeText = GameObject.Find("Pizza_Recipe_Text").GetComponent<TextMeshProUGUI>();
                GenerateGoalIngredientsList();
                SetRecipe(goalIngrendients);

                //StartCoroutine(GenerateGoalIngredientsCoroutine());

                break;


            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GenerateGoalIngredientsCoroutine()
    {
        while (true)
        {
            GenerateGoalIngredientsList();
            SetRecipe(goalIngrendients);
            //PrintIngredientsList(goalIngrendients);
            yield return new WaitForSeconds(5);
        }
    }

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
        // ToDo: Averiguar porque verga se pinta solo el Recipe y a veces >:(((

        recipeText.text = "Recipe: ";

        //int currWordsAmmount = 1;
        namesToChange = new List<Utils.TMProName>(_list.Count);
        for (int i = 0; i < _list.Count - 1; i++)
        {

            if (i == 1)
            {
                int wordsAmmount = Utils.GetWordsAmmount(_list[i].name);
                namesToChange.Add(new Utils.TMProName(Color.green, wordsAmmount, recipeText.textInfo.wordCount));

                //recipeText.ForceMeshUpdate();
                //Utils.PaintTMProWords(recipeText, Color.red, currWordsAmmount, currWordsAmmount + lastWordsAmmount);
                //recipeText.UpdateVertexData();
            }

            recipeText.text += _list[i].name + ", ";
            //currWordsAmmount += lastWordsAmmount;
        }
        recipeText.text += _list[_list.Count - 1].name + ".";

        recipeText.ForceMeshUpdate();
        Utils.PaintTMProWords(recipeText, namesToChange);
        recipeText.UpdateVertexData();

        //recipeText.ForceMeshUpdate();
        //Utils.PaintTMProChars(recipeText, Color.green, 10, recipeText.textInfo.characterCount);
        //Utils.PaintTMProWords(recipeText, Color.red, 2, recipeText.textInfo.wordCount - 1);
        //recipeText.UpdateVertexData();


        // TODO: Posar-ho en funcio a part i fer que funcioni segons el numero de la paraula

        //recipeText.ForceMeshUpdate();

        ////for (int i = 7; i < recipeText.textInfo.characterCount; i++)
        ////{
        ////TMP_CharacterInfo charInfo = recipeText.textInfo.characterInfo[i];
        ////int vertexIdx = charInfo.vertexIndex;
        ////Color32 targetColor = new Color32(0, 200, 0, 200);

        ////for (int idx = 0; idx < 4; idx++)
        ////{
        ////    recipeText.textInfo.meshInfo[charInfo.materialReferenceIndex].colors32[charInfo.vertexIndex + idx] = targetColor;
        ////}

        ////}

        ////TMP_CharacterInfo charInfo0 = recipeText.textInfo.characterInfo[0];
        ////for (int idx = 0; idx < 4; idx++)
        ////{
        ////    recipeText.textInfo.meshInfo[charInfo0.materialReferenceIndex].colors32[charInfo0.vertexIndex + idx] = new Color32(255, 255, 255, 255);
        ////}

        //for (int i = 1; i < recipeText.textInfo.wordCount; i++)
        //{
        //    TMP_WordInfo wordInfo = recipeText.textInfo.wordInfo[i];
        //    Color32 targetColor = new Color32(0, 200, 0, 200);
        //    for (int j = 0; j < wordInfo.characterCount; j++)
        //    {
        //        TMP_CharacterInfo charInfo = recipeText.textInfo.characterInfo[wordInfo.firstCharacterIndex + j];
        //        int vertexIdx = charInfo.vertexIndex;

        //        for (int idx = 0; idx < 4; idx++)
        //        {
        //            recipeText.textInfo.meshInfo[charInfo.materialReferenceIndex].colors32[charInfo.vertexIndex + idx] = targetColor;
        //        }
        //    }
        //}

        //recipeText.UpdateVertexData();


    }


   #region INGREDIENTS_LIST_REGION
    private void GenerateGoalIngredientsList()
    {
        int listMaxRange = 5;
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

            goalIngrendients.Add(fullIngrendientsList[ingredientId]);
        }

    }

    public List<IngredientClass> GetGoalIngredientsList()
    {
        return goalIngrendients;
    }

    public bool CheckIfGoalIngredient(string ingredientName)
    {
        foreach(IngredientClass ingredient in goalIngrendients)
        {
            if (ingredientName == ingredient.name)
            {
                //ToDo: Marcar ingredient en verd a la recipe
                return true;
            }
        }

        return false;
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
