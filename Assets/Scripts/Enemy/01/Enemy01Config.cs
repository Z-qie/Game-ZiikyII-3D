using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Enemy01Config : ScriptableObject
{
    public float health;
    public float chaseInterval;
    public float chaseDistance;
    public float attackDistance;
    public float attackInterval;
}
