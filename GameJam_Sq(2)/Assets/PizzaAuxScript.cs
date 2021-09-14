using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAuxScript : MonoBehaviour
{
    //public bool isColliding = false;
    //public string colTag = "";

    private PizzaScript main;

    private void Start()
    {
        main = GetComponentInParent<PizzaScript>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (main.GetRefreshIngredients() && main.IsValidIngredient(col.gameObject.tag))
        {
            if (!(col.CompareTag("Pan") && main.ingredients.Contains("Pan")))
            {
                main.AddIngredient(col.gameObject.tag);
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (main.GetRefreshIngredients() && col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table")
        {
            main.RemoveIngredient(col.gameObject.tag);
        }
    }

}
