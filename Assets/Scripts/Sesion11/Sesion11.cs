using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sesion11 : MonoBehaviour
{
    public int[] intArray;
    public string[] stringArray = new string[8];
    public ScriptableTable table;

    // Start is called before the first frame update
    void Start()
    {
       
        intArray = new int[8];
        for (int i = 0; i < intArray.Length; i++)
        {
            intArray[i] = Random.Range(0, 1000);

        }
        
        Debug.Log(Utils.GetRandomItem(stringArray));

        Debug.Log(Utils.GetRandomItem(intArray));

        stringArray = Utils.Shuffle(stringArray);

        GameManagerGeneric.instance.GameManagerCall();

        ScriptableTable.instance.GetData();

    }

     
}
