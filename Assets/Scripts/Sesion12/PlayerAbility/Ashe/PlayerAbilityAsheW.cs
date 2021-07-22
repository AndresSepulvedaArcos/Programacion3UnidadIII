using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityAsheW : PlayerAbility
{

    public GameObject prefabAsheWArrow;

    public override void Initialize(PlayerControllerCommand owner)
    {
        base.Initialize(owner);
        AbilityIndex = 1;
    }
    public override void Activate()
    {
        if (!TryToCommitAbility()) return;

        GameObject arrow;
        float spreadAngle = 25f;
        float angleStep = spreadAngle / 8f;

        for (int i = 0; i < 8; i++)
        {
            arrow= Instantiate(prefabAsheWArrow, transform.position, player.transform.rotation);
            float angle = -spreadAngle + i * angleStep;
            arrow.transform.Rotate(0,angle ,0);
            arrow.GetComponent<ProjectileBase>().ShootToDirection(arrow.transform.forward,player);

        }

    }
}
