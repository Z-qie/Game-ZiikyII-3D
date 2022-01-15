using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01ChaseState : EnemyState
{
    private Enemy01Config config;
    private float chaseTimer;
    private NavMeshAgent navMeshAgent;

    public EnemyStateId GetId()
    {
        return EnemyStateId.EnemyChase;
    }

    public void EnterState(EnemyAgent agent)
    {
        navMeshAgent = agent.GetComponent<NavMeshAgent>();
        config = ((Enemy01Agent)agent).config;

    }
    public void UpdateState(EnemyAgent agent)
    {
        if (agent.currentHealth <= 0)
            agent.enemyFSM.ChangeState(EnemyStateId.EnemyDeath);

        if ((agent.transform.position - agent.player.transform.position).sqrMagnitude <= config.attackDistance * config.attackDistance)
            agent.enemyFSM.ChangeState(EnemyStateId.EnemyAttack);

        // chase (because we dont have idle state)
        bool canChase = (agent.player.transform.position - agent.transform.position).sqrMagnitude <= config.chaseDistance * config.chaseDistance;

        if (chaseTimer > 0)
        {
            chaseTimer -= Time.deltaTime;
        }
        else if (canChase)
        {
            navMeshAgent.SetDestination(agent.player.transform.position);
            chaseTimer = config.chaseInterval;
            //Debug.Log("Chasing");
        }
        else
        {
            chaseTimer = config.chaseInterval;
        }
    }

    public void ExitState(EnemyAgent agent)
    {

    }
}


