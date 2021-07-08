using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLAmbda : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(() =>
       {
           Debug.Log("clicking");
       });
    }
}
