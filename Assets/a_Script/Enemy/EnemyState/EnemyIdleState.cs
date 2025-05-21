using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private EnemyController enemy;
    public EnemyIdleState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    
    public void OnEnter()
    {
        enemy.anim.Play("Idle");
        Debug.Log("idle enter");
    }

    public void Update()
    {
        // 切换状态
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
        Debug.Log("idle Exit");
    }
    
    
    
}
