using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : Singleton<DestroyObjects>
{
    public List<GameObject> ListOfGameObject;

    public void DestroyObject()
    {
        if (ListOfGameObject == null)
        {
            ListOfGameObject = new List<GameObject>();
        }
        else
        {
            for (int i = 0; i < ListOfGameObject.Count; i++)
                Destroy(ListOfGameObject[i]);
            ListOfGameObject.Clear();
        }
    }
    
}
