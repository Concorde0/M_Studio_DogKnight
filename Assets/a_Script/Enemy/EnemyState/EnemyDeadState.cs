using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : IState
{
    private EnemyController enemy;

    public EnemyDeadState(EnemyController enemy)
    {
        this.enemy = enemy;
    }
    public void OnEnter()
    {
        Debug.Log("enemy dead");   
        enemy.isDead = true;
        enemy.coli.enabled = false;
        enemy.agent.enabled = false;
    }
    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void OnExit()
    {
           
    }
}
