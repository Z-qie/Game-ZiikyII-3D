using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01SpawnState : EnemyState
{

    public EnemyStateId GetId()
    {
        return EnemyStateId.EnemySpawn;
    }

    public void EnterState(EnemyAgent agent)
    {
        agent.currentHealth = ((Enemy01Agent)agent).config.health;
        agent.originalHealth = agent.currentHealth;
        //agent.OnDeath += agent.enemyFSM.ChangeState(EnemyStateId.EnemyDeath);

    }


    public void UpdateState(EnemyAgent agent)
    {
        agent.enemyFSM.ChangeState(EnemyStateId.EnemyChase);
    }


    public void ExitState(EnemyAgent agent)
    {
        Debug.Log("Exit SpawnState");
    }


}
