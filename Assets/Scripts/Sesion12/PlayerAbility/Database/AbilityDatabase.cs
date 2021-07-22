using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FAbilityData
{
    public int abilityIndex;
    public float mana;
    public float cooldownBase ;
    public Sprite icon;
    public GameObject prefabAbility;
}

[CreateAssetMenu(fileName ="AbilityDatabase",menuName = "ArcosLOL")]
public class AbilityDatabase : ScriptableGeneric<AbilityDatabase>
{
    
    public FAbilityData[] abilities;

     
}
