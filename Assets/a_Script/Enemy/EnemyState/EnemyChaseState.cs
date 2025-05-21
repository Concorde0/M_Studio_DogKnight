using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChaseState :IState
{
    private EnemyController enemy;
    public EnemyChaseState(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
        
        enemy.isChase = true;
        enemy.isWalk = false;
        enemy.agent.isStopped = false;
    }

    public void Update()
    {
        
        enemy.ChaseLogic();
        if (enemy.TargetInAttackRange() || enemy.TargetInSkillRange())
        {
            enemy.ChangeState(new EnemyAttackState(enemy));
        }
    }

    public void FixedUpdate()
    {
       
    }

    public void OnExit()
    {
        
    }
    
}
