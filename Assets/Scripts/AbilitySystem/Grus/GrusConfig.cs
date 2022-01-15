using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AbilityConfigs/Grus")]

public class GrusConfig : AbilityConfig<Grus>
{
    public float attackRadius;
    public int numBolts;
    public float damagePerBolt;
}
