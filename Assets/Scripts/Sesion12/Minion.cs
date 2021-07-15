using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EMinionState { Walking,Chase,Attack}
public class Minion : MonoBehaviour, IDamageable
{

    NavMeshAgent agent;
    public EMinionState currentState = EMinionState.Walking;
    public ETeams team;
    public float detectionArea = 4f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;

    public GameObject currentTarget;
    Transform targetCore;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitializeMinion(ETeams myTeam,Transform OtherCore)
    {
        team = myTeam;
        targetCore = OtherCore;

        agent.Warp(transform.position);
        StartCoroutine(BehaviourTree());
    }


    IEnumerator BehaviourTree()
    {
        do
        {
            switch (currentState)
            {
                case EMinionState.Walking:
                    MoveToWaypoint();
                    CheckTarget();
                    break;
                case EMinionState.Chase:

                    agent.SetDestination(currentTarget.transform.position);
                    CheckAttackRange();
                    break;

                case EMinionState.Attack:
                    Attack();
                    break;

            }
            yield return new WaitForSeconds(0.1f);
        } while (gameObject.activeInHierarchy);
       

    }
  
    void MoveToWaypoint()
    {
        
        agent.SetDestination(targetCore.position);
    }

    void CheckTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionArea);
        for (int i = 0; i < colliders.Length; i++)
        {
            
            if (colliders[i].GetComponent<IDamageable>() == null) continue;

            if (colliders[i].GetComponent<IDamageable>().GetTeam()!=team)
            {

                currentTarget = colliders[i].gameObject;
                currentState = EMinionState.Chase;
                return;
            }
        }
    }

    void CheckAttackRange()
    {
        if((currentTarget.transform.position-transform.position).magnitude< attackRange)
        {
            currentState = EMinionState.Attack;
            StartCoroutine(Attack());

        }
    }

    public virtual  IEnumerator Attack()
    {
        if(currentTarget==null)
        {
            CheckAttack();
            yield break;
        }
            
        currentTarget.GetComponent<IDamageable>().ApplyDamage(1);

        yield return new WaitForSeconds(attackCooldown);

        if(currentState==EMinionState.Attack)
            StartCoroutine(Attack());

    }

    void CheckAttack()
    {
        if (currentTarget == null)
            currentState = EMinionState.Walking;
    }
    
    public ETeams GetTeam()
    {
        return team;
    }

    public void ApplyDamage(float Damage)
    {
        
    }
}