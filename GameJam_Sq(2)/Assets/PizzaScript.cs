using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaScript : MonoBehaviour
{
    const float INGREDIENT_MARGIN = 30.0f;
    const float DEFAULT_INGREDIENT_Y = -30.0f;

    //public string[] ingredients;
    public GameObject ingredientTextPrefab;
    public List<TextMeshProUGUI> ingredients = new List<TextMeshProUGUI>();

    private TextMeshProUGUI ingredientsText;
    private bool refreshIngredients = true;
    private GameManager gameManager;
    private List<Utils.TMProName> namesToChange = new List<Utils.TMProName>();

    // Start is called before the first frame update
    void Start()
    {
        ingredientsText = GameObject.Find("Pizza_Ingredients_Text").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }
    

    void RefreshText()
    {
        //ingredientsText.text = "Ingredients: <br>";
        //foreach(string ingredient in ingredients)
        //{
        //    ingredientsText.text += "- " + ingredient + "<br>";
        //}

        // ToDo: Mirar perque surt dels limits de l'array quan poses algun ingredient aqui

        //ingredientsText.ForceMeshUpdate();
        //Utils.PaintTMProWords(ingredientsText, namesToChange);
        //ingredientsText.UpdateVertexData();
    }

    public void SetRefreshIngredients(bool newState)
    {
        refreshIngredients = newState;
    }

    public bool GetRefreshIngredients()
    {
        return refreshIngredients;
    }

    public bool IsValidIngredient(string colTag)
    {
        return colTag != "Untagged" && colTag != "Table" && colTag != "Wall Colliders";
    }

    public List<string> GetCurrIngredients()
    {
        List<string> strIngredients = new List<string>();
        foreach(TextMeshProUGUI ingr in ingredients)
        {
            strIngredients.Add(ingr.text);
        }
        return strIngredients;
    }


    public void EraseNameToChange(string nameToErase)
    {
        bool erased = false;
        int nameToEraseLength = nameToErase.Split().Length;

        for(int i = 0; i < namesToChange.Count; i++)
        {
            if (!erased)
            {
                if(namesToChange[i].name == nameToErase)
                {
                    erased = true;
                    namesToChange.RemoveAt(i);

                    if (i < namesToChange.Count)
                        namesToChange[i].SetFirstWordIdx(namesToChange[i].firstWordIdx - nameToEraseLength);
                }
            }
            else
            {
                namesToChange[i].SetFirstWordIdx(namesToChange[i].firstWordIdx - nameToEraseLength);
            }
        }
    }
    
    public void AddIngredient(string _ingredientName)
    {
        TextMeshProUGUI newIngr = Instantiate(ingredientTextPrefab, ingredientsText.transform).GetComponent<TextMeshProUGUI>();
        newIngr.text = _ingredientName;
        if (gameManager.CheckIfGoalIngredient(_ingredientName)) newIngr.color = Color.green;
        else newIngr.color = Color.red;

        Vector3 currPos = newIngr.transform.localPosition;
        newIngr.transform.localPosition = new Vector3(currPos.x, DEFAULT_INGREDIENT_Y - INGREDIENT_MARGIN * ingredients.Count, currPos.z);
        ingredients.Add(newIngr);

        //for (int i = 0; i < gameManager.GetGoalIngredientsList().Count; i++)
        //{
        //    int totalWordsAmmount = Utils.GetWordsAmmount(ingredients.ToArray());
        //    int nameWordsAmmount = Utils.GetWordsAmmount(_ingredientName);

        //    if (gameManager.CheckIfGoalIngredient(_ingredientName))
        //    {
        //        namesToChange.Add(new Utils.TMProName(_ingredientName, Color.green, nameWordsAmmount, totalWordsAmmount + i));
        //    }
        //    else
        //    {
        //        namesToChange.Add(new Utils.TMProName(_ingredientName, Color.red, nameWordsAmmount, totalWordsAmmount + i));
        //    }

        //}

        //RefreshText();
    }

    public void RemoveIngredient(string _ingredientName)
    {
        //string targetText = " - " + _ingredientName;
        for(int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].text == _ingredientName)
            {
                Destroy(ingredients[i].gameObject);
                ingredients.RemoveAt(i);
                i--;
                continue;
            }
            Vector3 ingrPos = ingredients[i].transform.localPosition;
            ingredients[i].transform.localPosition = new Vector3(ingrPos.x, DEFAULT_INGREDIENT_Y - INGREDIENT_MARGIN * i, ingrPos.z);
        }
        //RefreshText();
    }


    //private void OnTriggerEnter(Collider col)
    //{
    //    if (refreshIngredients && IsValidIngredient(col.gameObject.tag))
    //    {
    //        if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
    //        {
    //            string currIngredientName = col.gameObject.tag;
    //            ingredients.Add(currIngredientName);

    //            //for (int i = 0; i < gameManager.GetGoalIngredientsList().Count; i++)
    //            //{
    //            //    int totalWordsAmmount = Utils.GetWordsAmmount(ingredients.ToArray());
    //            //    int nameWordsAmmount = Utils.GetWordsAmmount(currIngredientName);

    //            //    if (gameManager.CheckIfGoalIngredient(currIngredientName))
    //            //    {
    //            //        namesToChange.Add(new Utils.TMProName(currIngredientName, Color.green, nameWordsAmmount, totalWordsAmmount + i));
    //            //    }
    //            //    else
    //            //    {
    //            //        namesToChange.Add(new Utils.TMProName(currIngredientName, Color.red, nameWordsAmmount, totalWordsAmmount + i));
    //            //    }

    //            //}

    //            RefreshText();
    //        }
    //    }

    //    //if (col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table" && col.gameObject.tag != "Wall Colliders")
    //    //{
    //    //    if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
    //    //    {
    //    //        ingredients.Add(col.gameObject.tag);
    //    //        RefreshText();
    //    //    }
    //    //}
    //}

    //private void OnTriggerExit(Collider col)
    //{
    //    //if (refreshIngredients && IsValidIngredient(col.gameObject.tag))
    //    //{
    //    //    if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
    //    //    {
    //    //        string currIngredientName = col.gameObject.tag;
    //    //        ingredients.Remove(currIngredientName);
    //    //        //EraseNameToChange(currIngredientName);

    //    //        RefreshText();
    //    //    }
    //    //}

    //    if (refreshIngredients && col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table")
    //    {
    //        ingredients.Remove(col.gameObject.tag);
    //        RefreshText();
    //    }
    //}


}
