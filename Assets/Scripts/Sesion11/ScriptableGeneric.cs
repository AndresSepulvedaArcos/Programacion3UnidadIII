using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Arcos/DatabaseMenu")]

public class ScriptableGeneric<T> : ScriptableObject where T:ScriptableObject
{
    public static T instance;

    private void Awake()
    {
        instance = this as T;

    }
}
