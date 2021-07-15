using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ETeams { TeamA,TeamB}

public interface IDamageable
{
   public ETeams GetTeam();

    public void ApplyDamage(float Damage);
     
}
