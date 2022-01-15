using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateId
{
    EnemySpawn,
    EnemyChase,
    EnemyAttack,
    EnemyDeath
}

public interface EnemyState
{
    EnemyStateId GetId();
    void EnterState(EnemyAgent agent);
    void UpdateState(EnemyAgent agent);
    void ExitState(EnemyAgent agent);
}
