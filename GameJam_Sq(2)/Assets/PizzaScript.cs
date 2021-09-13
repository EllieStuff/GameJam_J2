using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaScript : MonoBehaviour
{
    //public string[] ingredients;
    public List<string> ingredients = new List<string>();

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
        ingredientsText.text = "Ingredients: <br>";
        foreach(string ingredient in ingredients)
        {
            ingredientsText.text += "- " + ingredient + "<br>";
        }

        // ToDo: Mirar perque surt dels limits de l'array quan poses algun ingredient aqui

        //ingredientsText.ForceMeshUpdate();
        //Utils.PaintTMProWords(ingredientsText, namesToChange);
        //ingredientsText.UpdateVertexData();
    }

    public void SetRefreshIngredients(bool newState)
    {
        refreshIngredients = newState;
    }

    private bool IsValidIngredient(string colTag)
    {
        return colTag != "Untagged" && colTag != "Table" && colTag != "Wall Colliders";
    }

    public List<string> GetCurrIngredients()
    {
        return ingredients;
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

    private void OnTriggerEnter(Collider col)
    {
        if (refreshIngredients && IsValidIngredient(col.gameObject.tag))
        {
            if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
            {
                string currIngredientName = col.gameObject.tag;
                ingredients.Add(currIngredientName);

                //for (int i = 0; i < gameManager.GetGoalIngredientsList().Count; i++)
                //{
                //    int totalWordsAmmount = Utils.GetWordsAmmount(ingredients.ToArray());
                //    int nameWordsAmmount = Utils.GetWordsAmmount(currIngredientName);

                //    if (gameManager.CheckIfGoalIngredient(currIngredientName))
                //    {
                //        namesToChange.Add(new Utils.TMProName(currIngredientName, Color.green, nameWordsAmmount, totalWordsAmmount + i));
                //    }
                //    else
                //    {
                //        namesToChange.Add(new Utils.TMProName(currIngredientName, Color.red, nameWordsAmmount, totalWordsAmmount + i));
                //    }

                //}

                RefreshText();
            }
        }

        //if (col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table" && col.gameObject.tag != "Wall Colliders")
        //{
        //    if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
        //    {
        //        ingredients.Add(col.gameObject.tag);
        //        RefreshText();
        //    }
        //}
    }

    private void OnTriggerExit(Collider col)
    {
        //if (refreshIngredients && IsValidIngredient(col.gameObject.tag))
        //{
        //    if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
        //    {
        //        string currIngredientName = col.gameObject.tag;
        //        ingredients.Remove(currIngredientName);
        //        //EraseNameToChange(currIngredientName);

        //        RefreshText();
        //    }
        //}

        if (col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table")
        {
            ingredients.Remove(col.gameObject.tag);
            RefreshText();
        }
    }

}
