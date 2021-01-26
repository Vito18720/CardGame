using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase pooler donde guardo una funcion estatica con la que de manera general hago una lista o pool de objetos, en base al prefab, el numero de objetos y el 
//padre, para luego devlverla (object pool)
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
