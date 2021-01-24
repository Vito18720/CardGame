using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static List<GameObject> GetPoolObjects(GameObject prefab, int numberList, GameObject pool)
    {
        List<GameObject> poolList = new List<GameObject>();

        for (int i = 0; i < numberList; i++)
        {
            GameObject instance = Instantiate(prefab);
            instance.transform.SetParent(pool.transform);
            instance.gameObject.SetActive(false);
            instance.gameObject.name = instance.gameObject.name + "_" + i;
            poolList.Add(instance as GameObject);
        }

        return poolList;
    }
}
