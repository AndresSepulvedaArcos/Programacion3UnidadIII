using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGeneric<T> : MonoBehaviour where T:MonoBehaviour
{
    public static T instance;

    public GameObject prefab;
    public int poolSize = 10;
    List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        instance = this as T;
    }

    private void Initialized()
    {
        GameObject tmpObj = null;
        for (int i = 0; i < poolSize; i++)
        {
            tmpObj = Instantiate(prefab);
            tmpObj.SetActive(false);
            pool.Add(tmpObj);
        }
    }
}
