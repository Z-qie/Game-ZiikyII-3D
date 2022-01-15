using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class AbilityConfigBase : ScriptableObject
{
    [Header("Ability State")]
    public bool hasLearned = false;
    public bool equipped = true; // tbc

    [Header("Ability Info")]
    public AbilityCode abilityCode;
    [HideInInspector] public InputAction inputAction;

    [TextArea(1, 2)]
    public string description;
    public Sprite icon;

    // time for casting and effect
    public float preparingTime;
    public float activeTime;
    public float postActiveTime;
    public float coolDownTime;

    [HideInInspector] public float currentPreparingTime;
    [HideInInspector] public float currentActiveTime;
    [HideInInspector] public float currentPostActiveTime;
    [HideInInspector] public float currentCoolDownTime;

    public abstract BaseAbility AddAbility(GameObject gameObject);
    public abstract void RemoveAbility(GameObject gameObject);
}

public abstract class AbilityConfig<T> : AbilityConfigBase where T : BaseAbility
{
    public override BaseAbility AddAbility(GameObject gameObject)
    {
        if (gameObject == null)
        {
            Debug.Log("Null player");
            return null;
        }
        // attach ability onto player/enemy
        T ability = gameObject.GetComponent<T>();
        ability.baseConfig = this;
        hasLearned = true;
        return ability;
    }

    public override void RemoveAbility(GameObject gameObject)
    {
        T ability = gameObject.GetComponent<T>();
        hasLearned = false;
    }
}
