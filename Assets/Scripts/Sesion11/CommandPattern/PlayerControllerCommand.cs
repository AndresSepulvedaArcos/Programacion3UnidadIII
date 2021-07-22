using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerCommand : MonoBehaviour,IDamageable
{
    //FIFO LIFO

    public delegate void FNotifyAttributeChange(AttributeBase attributeChanged);
    public static event FNotifyAttributeChange OnHpChange, OnManaChange;

    NavMeshAgent agent;

   
    public Queue<Vector3> waypoints = new Queue<Vector3>();
    public List<Vector3> waypointsToView;

    public ETeams team;

    public GameObject basicAttackPrefab;
    public float attackRange = 15f;
    bool basicIsInCooldown;
    public float basicCooldown = 0.5f;

    [Header("Attributes")]
    public AttributeBase hp;
    public AttributeBase mana;

    public float manaRegen = 1f;

    [Header("Abilities")]
    public PlayerAbility[] abilities=new PlayerAbility[4];

    Coroutine manaRegenCorutine;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeAttributes();
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

    void InitializeAttributes()
    {
        hp = new AttributeBase(1700);
        mana = new AttributeBase(400) ;

    }
    void InitializeAbilities()
    {
        Debug.Log(AbilityDatabase.instance);

        FAbilityData abilityData = AbilityDatabase.instance.abilities[0];
        abilities[abilityData.abilityIndex] =    Instantiate(abilityData.prefabAbility, transform).GetComponent<PlayerAbility>();
        abilities[abilityData.abilityIndex].Initialize(this, abilityData);
         
    }

    void TryToExecuteAbility(int AbilityIndex)
    {
        abilities[AbilityIndex].Activate();
    }
    public ETeams GetTeam()
    {
        return team;
    }
    //consume mana rage etctectec
    public void ConsumeResource(float AmmountToConsume)
    {
        mana.Subtract(AmmountToConsume);
        OnManaChange?.Invoke(mana);
        if (manaRegenCorutine != null) return;
           manaRegenCorutine=StartCoroutine(ManaRecovery());
    }
    public void ApplyDamage(float Damage)
    {
        hp.Subtract(Damage);

        OnHpChange?.Invoke(hp);


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

    IEnumerator ManaRecovery()
    {
        while(mana.currentValue<mana.baseValue)
        {
            mana.Add(1);
            OnManaChange?.Invoke(mana);
            yield return new WaitForSeconds(.5f);

        }
    }
}
