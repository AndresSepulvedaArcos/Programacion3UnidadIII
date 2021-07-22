using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAbility : MonoBehaviour
{
    public delegate void FNotifyAbilityActivate(int NotifyAbilityIndex,PlayerAbility playerAbilityReference);
    public delegate void FNotifyAbilityInit(FAbilityData abilityDataReference);

    public static event FNotifyAbilityActivate OnActivateAbility;
    public static event FNotifyAbilityInit OnSkillInitialized;
    
    public float coolDown { get {return  abilityData.cooldownBase; } }

    FAbilityData abilityData;
    protected PlayerControllerCommand player;

    bool isInCooldown; 
    WaitForSeconds wait;

 
    virtual public void Initialize(PlayerControllerCommand owner,FAbilityData abilitydataRef)
    {
        player = owner;
        abilityData = abilitydataRef;
        wait = new WaitForSeconds(coolDown);
        OnSkillInitialized?.Invoke(abilityData);

    }

    virtual public void Activate()
    {

    }

    virtual public bool TryToCommitAbility()
    {
        if (isInCooldown) return false;
        if (abilityData.mana > player.mana.currentValue) return false;

        StartCoroutine(BeginCooldown());

        player.ConsumeResource(abilityData.mana);

        OnActivateAbility?.Invoke(abilityData.abilityIndex,this);

        return true;
    }
    IEnumerator BeginCooldown()
    {
        isInCooldown = true;
        yield return wait;
        isInCooldown = false;
    }

     
}
