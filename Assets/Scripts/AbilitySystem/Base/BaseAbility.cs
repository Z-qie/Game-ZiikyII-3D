using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState
{
    READY,
    PREPARING,
    ACTIVE,
    POSTACTIVE,
    COOLDOWN,
    OVERRIDED, // by enhanced abilities
}

public class BaseAbility : MonoBehaviour
{
    [HideInInspector]
    public AbilityConfigBase baseConfig;
    public AbilityState abilityState;

    protected PlayerController playerController;
    protected InputManager inputManager;
    protected Camera cameraBrain;
    public Transform orbTransform;

    protected virtual void Awake()
    {

    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        // ask for inputmanager from player controller
        playerController = gameObject.GetComponent<PlayerController>();
        inputManager = playerController.inputManager;
        cameraBrain = playerController.cameraBrain;

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!baseConfig.hasLearned || !baseConfig.equipped)
            return;
    }

    protected virtual void PreActivate()
    {

    }
    protected virtual void PostActivate()
    {

    }
    protected virtual void Activate()
    {

    }
}
