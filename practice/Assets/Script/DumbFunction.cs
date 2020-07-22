using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbFunction<T,U> : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<T> GetKeys(Dictionary<T,U> data) 
    {
        List<T> returnList = new List<T>();
        foreach (KeyValuePair<T, U> dicData in data) 
        {
            returnList.Add(dicData.Key);
        }
        return returnList;
    }

    public static void DestroyAllChild(Transform parent) 
    {
        foreach (Transform child in parent) 
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
