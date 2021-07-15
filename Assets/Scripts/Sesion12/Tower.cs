using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tower : MonoBehaviour,IDamageable
{
    public ETeams team;
    public float Hp = 100;
    public TextMeshPro hpText;
    public void ApplyDamage(float Damage)
    {
        Hp -= Damage;
        hpText.SetText(Hp.ToString());
        if (Hp <= 0)
            Destroy(gameObject);
    }

    public ETeams GetTeam()
    {
        return team;
    }

    // Start is called before the first frame update
    void Start()
    {
        hpText.SetText(Hp.ToString());
    }

     
}
