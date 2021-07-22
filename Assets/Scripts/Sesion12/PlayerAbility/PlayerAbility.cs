using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAbility : MonoBehaviour
{
    public delegate void FNotifyAbilityActivate(int NotifyAbilityIndex,PlayerAbility playerAbilityReference);
    public static event FNotifyAbilityActivate OnActivateAbility;
    protected int AbilityIndex;
    protected PlayerControllerCommand player;

    bool isInCooldown;
    public float cooldownTime=1f;
    WaitForSeconds wait;


    private void Awake()
    {
        wait = new WaitForSeconds(cooldownTime);
    }
    virtual public void Initialize(PlayerControllerCommand owner)
    {
        player = owner;
    }

    virtual public void Activate()
    {

    }

    virtual public bool TryToCommitAbility()
    {
        if (isInCooldown) return false;


        StartCoroutine(BeginCooldown());
        OnActivateAbility?.Invoke(AbilityIndex,this);

        return true;
    }
    IEnumerator BeginCooldown()
    {
        isInCooldown = true;
        yield return wait;
        isInCooldown = false;
    }

     
}
