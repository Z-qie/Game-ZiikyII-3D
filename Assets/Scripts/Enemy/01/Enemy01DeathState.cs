using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01DeathState : EnemyState
{

    private Enemy01Config config;

    public EnemyStateId GetId()
    {
        return EnemyStateId.EnemyDeath;
    }

    public void EnterState(EnemyAgent agent)
    {
        config = ((Enemy01Agent)agent).config;

        Debug.Log("die");
        agent.enemyFSM.ChangeState(EnemyStateId.EnemyChase);
        agent.currentHealth = config.health;
}

    public void ExitState(EnemyAgent agent)
    {
    }



    public void UpdateState(EnemyAgent agent)
    {
    }
}
