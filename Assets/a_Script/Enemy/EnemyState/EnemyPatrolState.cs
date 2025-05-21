using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : IState
{
    private EnemyController enemy;

    public EnemyPatrolState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        Debug.Log("Patrol State Enter");
        enemy.isChase = false;
        enemy.agent.speed = enemy.speed * 0.5f;
    }
    
    public void Update()
    {
        enemy.PatrolLogic();
        if (enemy.FoundPlayer())
        {
            // 这个不太会
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
