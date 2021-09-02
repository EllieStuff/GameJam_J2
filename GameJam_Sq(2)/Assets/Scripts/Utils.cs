using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    
    public static int FindInList<T>(List<T> listToSearch, T elementToFind)
    {
        for(int i = 0; i < listToSearch.Count; i++)
        {
            if (listToSearch[i].Equals(elementToFind))
                return i;
        }

        return -1;
    }


}
