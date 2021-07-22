using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : HpBar
{
    protected override void OnEnable()
    {
        PlayerControllerCommand.OnManaChange += OnAtttributeChange;
    }
    protected override void OnDisable()
    {
        PlayerControllerCommand.OnManaChange -= OnAtttributeChange;
    }
}
