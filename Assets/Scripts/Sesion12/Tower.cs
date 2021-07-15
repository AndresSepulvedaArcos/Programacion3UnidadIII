using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum ETowerState { Waiting,Attacking}
public class Tower : MonoBehaviour,IDamageable
{
    public ETeams team;
    public float Hp = 100;
    public TextMeshPro hpText;
    public float detectionArea = 14f;
    public float attackCooldown = 1f;

    public Transform currentTarget;
    public ETowerState currentState = ETowerState.Waiting;

    public Transform attackPoint;
    public GameObject projectilePrefab;
    public void ApplyDamage(float Damage)
    {
        Hp -= Damage;
        hpText.SetText(Hp.ToString());
        if (Hp <= 0)
            Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionArea);
    }
    public ETeams GetTeam()
    {
        return team;
    }

    // Start is called before the first frame update
    void Start()
    {
        hpText.SetText(Hp.ToString());

        StartCoroutine(BehaviourTree());
    }

    IEnumerator BehaviourTree()
    {


        do
        {
            if (gameObject == null)
            {
                yield break;
            }

            switch (currentState)
            {
                case ETowerState.Waiting:
                   
                    CheckTarget();
                    break;
                case ETowerState.Attacking:


                    // Attack();
                    CheckAttackRange();
                    break;

            }
            yield return new WaitForSeconds(0.1f);
        } while (gameObject.activeInHierarchy);


    }

    void CheckAttackRange()
    {
        if(currentTarget==null)
        {
            CheckAttack();
            return;
        }

        if ((currentTarget.position - transform.position).magnitude > detectionArea)
        {
            currentState = ETowerState.Waiting;
            currentTarget = null;



        }
    }
    private void CheckTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionArea);
        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].GetComponent<IDamageable>() == null) continue;

            if (colliders[i].GetComponent<IDamageable>().GetTeam() != team)
            {

                currentTarget = colliders[i].transform;
                currentState = ETowerState.Attacking;
                StartCoroutine(Attack());
                return;
            }
        }
    }

    public virtual IEnumerator Attack()
    {
        if (currentTarget == null)
        {
            CheckAttack();
            yield break;
        }

         GameObject projectile=  Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
        projectile.GetComponent<ProjectileTower>()?.ShootTo(currentTarget);

        yield return new WaitForSeconds(attackCooldown);

        if (currentState == ETowerState.Attacking)
            StartCoroutine(Attack());

    }
    void CheckAttack()
    {
        if (currentTarget == null)
            currentState = ETowerState.Waiting;
    }

}
