using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public enum AbilityCode
{
    Lepus,
    Dash,
    Columba,
    Grus,
    Ophiuchus

}

public class AbilityManager : MonoBehaviour
{
    [SerializeField]
    private AbilityConfigBase[] allAbilityConfigs = { };

    GameObject character;
    protected InputManager inputManager;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        inputManager = character.gameObject.GetComponent<PlayerController>().inputManager;

        AddAbility(AbilityCode.Lepus);
        AddAbility(AbilityCode.Dash);
        AddAbility(AbilityCode.Columba);
        AddAbility(AbilityCode.Grus);
        AddAbility(AbilityCode.Ophiuchus);

        BindInputAction(AbilityCode.Grus, inputManager.InGame.FirstSpell);
        BindInputAction(AbilityCode.Ophiuchus, inputManager.InGame.SecondSpell);

    }

    private void Update()
    {

    }

    public void AddAbility(AbilityCode abilityCode)
    {
        AbilityConfigBase abilityConfig = Array.Find(allAbilityConfigs, s => s.abilityCode == abilityCode);
        abilityConfig.AddAbility(character);
    }

    public void EquipAbility(AbilityCode abilityCode)
    {
        AbilityConfigBase abilityConfig = Array.Find(allAbilityConfigs, s => s.abilityCode == abilityCode);
        abilityConfig.equipped = true;
    }

    public void BindInputAction(AbilityCode abilityCode, InputAction inputAction)
    {
        AbilityConfigBase abilityConfig = Array.Find(allAbilityConfigs, s => s.abilityCode == abilityCode);
        abilityConfig.inputAction = inputAction;
    }
}