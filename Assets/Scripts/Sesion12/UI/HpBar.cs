using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image bar;

    protected virtual void OnDisable()
    {
        PlayerControllerCommand.OnHpChange -= OnAtttributeChange;
    }
    protected virtual void OnEnable()
    {
        PlayerControllerCommand.OnHpChange += OnAtttributeChange; 
    }

    public void OnAtttributeChange(AttributeBase attributeChanged)
    {
        bar.fillAmount = attributeChanged.percent;

    }
}
