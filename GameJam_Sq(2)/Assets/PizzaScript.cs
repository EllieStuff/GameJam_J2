using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaScript : MonoBehaviour
{
    //public string[] ingredients;
    public List<string> ingredients = new List<string>();

    private TextMeshProUGUI ingredientsText;

    // Start is called before the first frame update
    void Start()
    {
        ingredientsText = GameObject.Find("Pizza_Ingredients_Text").GetComponent<TextMeshProUGUI>();
    }
    

    void RefreshText()
    {
        ingredientsText.text = "";
        foreach(string ingredient in ingredients)
        {
            ingredientsText.text += "- " + ingredient + "<br>";
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table" && col.gameObject.tag != "Wall Colliders")
        {
            if (!(col.CompareTag("Pan") && ingredients.Contains("Pan")))
            {
                ingredients.Add(col.gameObject.tag);
                RefreshText();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table")
        {
            ingredients.Remove(col.gameObject.tag);
            RefreshText();
        }
    }

}
