using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientClass
{
    public string name = "noname";
    public bool edible;
    public List<string> similarities;

    public IngredientClass(string _name = "noname", bool _edible = true, List<string> _similarities = null)
    {
        name = _name;
        edible = _edible;
        similarities = _similarities;
    }
}
