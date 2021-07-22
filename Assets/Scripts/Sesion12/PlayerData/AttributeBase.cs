using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttributeBase 
{
    public float baseValue;
    public float currentValue;

    public float percent { get { return currentValue / baseValue; } }

    public AttributeBase(float InitialValue)
    {
        baseValue = currentValue = InitialValue;
    }

    public float Subtract(float ValueToRest)
    {
       return currentValue = Mathf.Clamp(currentValue - ValueToRest, 0f, baseValue);

    }
    public float Add(float ValueToRest)
    {
        return currentValue = Mathf.Clamp(currentValue + ValueToRest, 0f, baseValue);

    }
}
