using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy01Agent : EnemyAgent
{
    public Enemy01Config config;

    private void OnEnable()
    {
       
    }

    protected override void Start()
    {
        base.Start();
        enemyFSM.RegisterState(new Enemy01SpawnState());
        enemyFSM.RegisterState(new Enemy01ChaseState());
        enemyFSM.RegisterState(new Enemy01DeathState());
        enemyFSM.RegisterState(new Enemy01AttackState());

        enemyFSM.ChangeState(initialStateId);
        currentHealth = config.health;
    }

    protected override void Update()
    {
        base.Update();
        enemyFSM.UpdateState();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.chaseDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, config.attackDistance);
    }
}
