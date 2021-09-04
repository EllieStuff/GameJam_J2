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

    // Start is called before the first frame update
    void Start()
    {
        ingredientsText = GameObject.Find("Pizza_Ingredients_Text").GetComponent<TextMeshProUGUI>();
    }
    

    void RefreshText()
    {
        ingredientsText.text = "Ingredients: <br>";
        foreach(string ingredient in ingredients)
        {
            ingredientsText.text += "- " + ingredient + "<br>";
        }
    }

    public void SetRefreshIngredients(bool newState)
    {
        refreshIngredients = newState;
    }

    private bool IsValidIngredient(string colTag)
    {
        return colTag != "Untagged" && colTag != "Table" && colTag != "Wall Colliders";
    }

    private void OnTriggerEnter(Collider col)
    {
        if (refreshIngredients && IsValidIngredient(col.gameObject.tag))
        {
            if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
            {
                ingredients.Add(col.gameObject.tag);
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
        if (refreshIngredients && IsValidIngredient(col.gameObject.tag))
        {
            if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
            {
                ingredients.Remove(col.gameObject.tag);
                RefreshText();
            }
        }

        //if (col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table")
        //{
        //    ingredients.Remove(col.gameObject.tag);
        //    RefreshText();
        //}
    }

}
