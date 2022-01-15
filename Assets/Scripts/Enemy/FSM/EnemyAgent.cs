using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.SerializableAttribute]
public class EnemyAgent : LivingEntity
{
    public GameObject player;
    public EnemyFSM enemyFSM;
    public EnemyStateId initialStateId;

    protected override void Start()
    {
        base.Start();
        enemyFSM = new EnemyFSM(this);
    }

    protected override void Update()
    {
        base.Update();
    }


}
