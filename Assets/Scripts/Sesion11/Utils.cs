using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils  
{
    //T se usa como Genrico / comodin 
    public static T GetRandomItem<T>(T[] array)
    {
        int randomIndex = Random.Range(0, array.Length);
        return array[randomIndex];

    }
    public static T[] Shuffle<T>(T[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;

        while(n>1)
        {
            n--;
            int k = rng.Next(n + 1);
            T tmpValue = array[k];
            array[k] = array[n];
            array[n] = tmpValue;

        }
        return array;
    }

}
