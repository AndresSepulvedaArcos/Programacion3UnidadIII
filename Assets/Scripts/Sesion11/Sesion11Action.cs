using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sesion11Action : MonoBehaviour
{
    public delegate void FNotify();
    FNotify notify;

    Action notifyAction;
    Action<int> callFunctionInt;
    Action<int, bool> callFunctionTwoParam;

    Func<int, int> callFunctionWithRet;

    Action callNothing;

    List<int> sortList = new List<int>();
    private void Start()
    {
        notify += CallDebug;

        notify.Invoke();

        notifyAction += CallDebug;

        notifyAction();

        callFunctionInt += CallDebugWithParamater;
        callFunctionInt(5);

        callFunctionTwoParam += CallDebugWithParamaters;

        callFunctionTwoParam(2, false);

        callFunctionWithRet += CallFunctionWithReturn;

        Debug.Log(callFunctionWithRet(9));

        callNothing += () =>
        {
            Debug.Log("esto existe en el lambda");
        };
        callNothing();

        callFunctionTwoParam += (number, isTrue) =>
          {
              Debug.LogFormat("Desde Lambda---> para el numero {0} el valor es {1}", number, isTrue);

          };

        callFunctionTwoParam(123,true);

        
       
    }

    
    void CallDebug()
    {
        Debug.Log("Call debug");
    }

    void CallDebugWithParamater(int Number)
    {
        Debug.Log(Number);

    }
    void CallDebugWithParamaters(int Number,bool isTrue)
    {
        Debug.Log(Number+ " el valor es "+ isTrue);

    }

    int CallFunctionWithReturn(int Number)
    {
        return Number + 1;

    }


}
