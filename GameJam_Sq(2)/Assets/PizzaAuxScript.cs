using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaAuxScript : MonoBehaviour
{
    private PizzaScript main;

    private List<string> removeWhenExitPause = new List<string>();
    private bool pauseChecked = false;

    private void Start()
    {
        main = GetComponentInParent<PizzaScript>();
    }

    private void Update()
    {
        if(PauseMenu.gameIsPaused && !pauseChecked)
        {
            pauseChecked = true;
        }
        else if(!PauseMenu.gameIsPaused && pauseChecked)
        {
            pauseChecked = false;
            RemoveWhenExitPause();
        }
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
        if (col.gameObject.tag != "Untagged" && col.gameObject.tag != "Table")
        {
            if (main.GetRefreshIngredients())
            {
                main.RemoveIngredient(col.gameObject.tag);
            }
            else
            {
                removeWhenExitPause.Add(col.gameObject.tag);
            }
        }
    }

    private void RemoveWhenExitPause()
    {
        foreach(string ingredientTag in removeWhenExitPause)
        {
            main.RemoveIngredient(ingredientTag);
        }

        removeWhenExitPause = new List<string>();
    }

}
