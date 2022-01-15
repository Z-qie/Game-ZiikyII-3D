using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AbilityConfigs/Ophiuchus")]

public class OphiuchusConfig : AbilityConfig<Ophiuchus>
{
    public float attackRadius;
    public int numBolts;
    public float damagePerBolt;

}
