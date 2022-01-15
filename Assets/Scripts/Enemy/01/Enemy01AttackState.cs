using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01AttackState : EnemyState
{
    private Enemy01Config config;
    private float attackTimer;


    public EnemyStateId GetId()
    {
        return EnemyStateId.EnemyAttack;
    }

    public void EnterState(EnemyAgent agent)
    {
        config = ((Enemy01Agent)agent).config;

    }
    public void UpdateState(EnemyAgent agent)
    {
        if (agent.currentHealth <= 0)
            agent.enemyFSM.ChangeState(EnemyStateId.EnemyDeath);

        // check chase
        if ((agent.transform.position - agent.player.transform.position).sqrMagnitude > config.attackDistance * config.attackDistance)
            agent.enemyFSM.ChangeState(EnemyStateId.EnemyChase);


        // attack
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else 
        {
            attackTimer = config.chaseInterval;
            //Debug.Log("Attacking");
        }

      

    }

    public void ExitState(EnemyAgent agent)
    {

    }
}
