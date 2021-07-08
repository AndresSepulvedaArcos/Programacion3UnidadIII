using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonGeneric<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T instance;

    private void Awake()
    {
        instance = this as T;
    }

    
}
