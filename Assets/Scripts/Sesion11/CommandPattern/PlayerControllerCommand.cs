using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerCommand : MonoBehaviour,IDamageable
{
    //FIFO LIFO

    NavMeshAgent agent;

   
    public Queue<Vector3> waypoints = new Queue<Vector3>();
    public List<Vector3> waypointsToView;

    public ETeams team;

    public GameObject basicAttackPrefab;
    public float attackRange = 15f;
    bool basicIsInCooldown;
    public float basicCooldown = 0.5f;

    public PlayerAbility[] abilities;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeAbilities();
    }

    void OnDisable()
    {
        SkillInput.OnSkillInputPress -= SkillInput_OnSkillInputPress;
    }

    void OnEnable()
    {
        SkillInput.OnSkillInputPress += SkillInput_OnSkillInputPress;
    }

    private void SkillInput_OnSkillInputPress(int SkillIndexInput)
    {
        TryToExecuteAbility(SkillIndexInput);
    }

    void AddActionToQueue()
    {
        Ray ray=   Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {

            if(hit.transform.GetComponent<IDamageable>()==null)
            {
                agent.SetDestination(hit.point);
                agent.isStopped = false;
            }
            else
            {
                if(hit.transform.GetComponent<IDamageable>().GetTeam()!=team)
                {
                    if((hit.transform.position-transform.position).magnitude>attackRange)
                    {
                        agent.isStopped = false;
                        agent.SetDestination(hit.point);
                        
                    }
                    else
                    {
                        agent.isStopped = true;
                        MakeBasicAttack(hit.transform);
                    }
                   
                }
            }
             
            

        }


    }

    void DisplayQueue()
    {
        waypointsToView.Clear();
        waypointsToView.AddRange(waypoints.ToArray()); 
    }
    void MoveToNextWaypoint()
    {
        waypoints.Dequeue();
        DisplayQueue();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AddActionToQueue();
        }
         
         
    }

    void InitializeAbilities()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if(abilities[i]!=null)
            {
                abilities[i].Initialize(this);
            }
        }
    }

    void TryToExecuteAbility(int AbilityIndex)
    {
        abilities[AbilityIndex].Activate();
    }
    public ETeams GetTeam()
    {
        return team;
    }

    public void ApplyDamage(float Damage)
    {
         
    }

    public void MakeBasicAttack(Transform attackTarget)
    {
        if (basicIsInCooldown) return;

        GameObject projectile= Instantiate(basicAttackPrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<PlayerBasicAttack>().ShootTo(attackTarget);
        basicIsInCooldown = true;
        StartCoroutine(ResetBasicAttackCooldown());
    }

    IEnumerator ResetBasicAttackCooldown()
    {
        yield return new WaitForSeconds(basicCooldown);
        basicIsInCooldown = false;
    }
}
