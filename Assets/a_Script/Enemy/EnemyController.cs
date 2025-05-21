using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour,IEndGameObserve
{
    public NavMeshAgent agent;
    public Animator anim;
    public Collider coli;
    private IState currentState;
    private CharacterStats characterStats;

    public GameObject attackTarget;
    
    private float lastAttackTime;
    public float sightRadius;
    public float speed;

    public bool isGuard;
    public bool isWalk;
    public bool isChase;
    public bool isFollow;
    public bool isDead;
    
    [Header("Patrol State")]
    public float patrolRange;
    private Vector3 wayPoint;
    private Vector3 guardPos;

    private void Awake()
    {
         agent = GetComponent<NavMeshAgent>();
         anim = GetComponent<Animator>();
         coli = GetComponent<Collider>();
         characterStats = GetComponent<CharacterStats>();
         speed = agent.speed;
         guardPos = transform.position;
    }

    private void Start()
    {
        if (isGuard)
        {
            ChangeState(new EnemyIdleState(this));
        }
        else
        {
            ChangeState(new EnemyPatrolState(this));
            GetNewWayPoint();
        }
        
        GameManager.Instance.AddObserver(this);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RemoveObserver(this);
        }

        if (GetComponent<LootSpawner>() && isDead)
        {
            GetComponent<LootSpawner>().SpawnLoot();
        }

        if (QuestManager.IsInitialized && isDead)
        {
            QuestManager.Instance.UpdateQuestProgress(this.name,1);
        }
    }

    private void Update()
    {
        if (characterStats.CurrentHealth <= 0)
        {
            ChangeState(new EnemyDeadState(this));
        }
        currentState?.Update();
        SwitchAnimation();
        lastAttackTime -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    public void ChangeState(IState newState)
    {
        currentState = newState;
        currentState.OnEnter();
        currentState?.OnExit();
    }



    public bool FoundPlayer()
    {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);

        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                attackTarget = target.gameObject;
                return true;
            }
        }
        attackTarget = null;
        return false;
    }

    public bool TargetInAttackRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= characterStats.attackData.attackRange;
        }
        else
        {
            return false;
        }
    }

    public bool TargetInSkillRange()
    {
        if (attackTarget != null)
        {
            return Vector3.Distance(attackTarget.transform.position, transform.position) <= characterStats.attackData.skillRange;
        }
        else
        {
            return false;
        }
    }

    private void SwitchAnimation()
    {
        anim.SetBool("Walk", isWalk);
        anim.SetBool("Chase", isChase);
        anim.SetBool("Follow", isFollow);
        anim.SetBool("Critical",characterStats.isCritical);
        anim.SetBool("Death", isDead);
    }

    private void GetNewWayPoint()
    {
        float randomX = UnityEngine.Random.Range(-patrolRange, patrolRange);
        float randomZ = UnityEngine.Random.Range(-patrolRange, patrolRange);
        
        Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y,guardPos.z + randomZ);
        wayPoint = randomPoint;
        wayPoint = NavMesh.SamplePosition(randomPoint, out var hit,patrolRange,1)? hit.position : transform.position;

    }

    public void PatrolLogic()
    {
        if (Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
        {
            isWalk = false;
            GetNewWayPoint();
        }
        else
        {
            isWalk = true;
            agent.destination = wayPoint;
        }
    }

    public void ChaseLogic()
    {
        if (!FoundPlayer())
        {
            isFollow = false;
            agent.destination = transform.position;
        }
        else
        {
            isFollow = true;
            agent.isStopped = false;
            agent.destination = attackTarget.transform.position;
        }
        
    }

    public void CriticalJudgement()
    {
        if (lastAttackTime < 0)
        {
            lastAttackTime = characterStats.attackData.cooldown;
                
            characterStats.isCritical = UnityEngine.Random.value < characterStats.attackData.criticalChance;
        }  
    }

    public void AttackLogic()
    {
         transform.LookAt(attackTarget.transform);
         if (TargetInAttackRange())
         {
             anim.SetTrigger("Attack");
         }
         if (TargetInSkillRange())
         {
             anim.SetTrigger("Skill");
         }
    }

    private void Hit()
    {
        if (attackTarget != null)
        {
            var targetStats = attackTarget.GetComponent<CharacterStats>();
            targetStats.TakeDamage(characterStats,targetStats);
        }
    }


    public void EndNotify()
    {
        isChase = false;
        isWalk = false;
        attackTarget = null;
    }
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRadius); 
    }
}
