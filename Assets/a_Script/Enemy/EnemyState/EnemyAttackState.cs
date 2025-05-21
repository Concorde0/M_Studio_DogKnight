using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState :IState
{
    
    private EnemyController enemy;
    public EnemyAttackState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        enemy.isFollow = false;
        enemy.agent.isStopped = true;
        
    }

    public void Update()
    {
        Debug.Log("Attack State ");
        enemy.CriticalJudgement();
        enemy.AttackLogic();
        if (!enemy.TargetInAttackRange() || !enemy.TargetInSkillRange())
        {
            enemy.ChangeState(new EnemyChaseState(enemy));
        }
    }

    public void FixedUpdate()
    {
        
    }
    

    public void OnExit()
    {
        
    }
}
