using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Lepus : BaseAbility
{
    public GameObject lepusBoltPrefab;
    //public GameObject prevVFXPrefab;

    // timing
    private LepusConfig config;
    [SerializeField] private LayerMask layerMask;
    //private GameObject preVFX;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {

        base.Update();
        config = (LepusConfig)baseConfig;


        switch (abilityState)
        {
            case AbilityState.OVERRIDED:
                // hand over control
                break;
            case AbilityState.READY:
                // TODO: broadcast to inGame UI
                if (inputManager.InGame.Lepus.ReadValue<float>() != 0 && playerController.canCastSpell)
                {
                    //preColumbaVFX = Instantiate(preColumbaVFXPrefab, orbTransform.position, Quaternion.identity, orbTransform);

                    //abilityState = AbilityState.PREPARING;
                    abilityState = AbilityState.ACTIVE;
                    //currentActiveTime = config.activeTime;
                }
                break;
            //case AbilityState.PREPARING:
            //    if (currentPreparingTime > 0)
            //    {
            //        PreActivate();
            //        currentPreparingTime -= Time.deltaTime;
            //    }
            //    else
            //    {
            //        Destroy(preColumbaVFX);
            //        abilityState = AbilityState.ACTIVE;
            //        currentActiveTime = config.activeTime;
            //    }
            //    break;
            case AbilityState.ACTIVE:
                Activate();
                abilityState = AbilityState.COOLDOWN;
                config.currentCoolDownTime = config.coolDownTime;
                break;
            //case AbilityState.POSTACTIVE:
            //    PostActivate();
            //    abilityState = AbilityState.COOLDOWN;
            //    break;
            case AbilityState.COOLDOWN:
                if (config.currentCoolDownTime > 0)
                {
                    config.currentCoolDownTime -= Time.deltaTime;
                }
                else
                {
                    abilityState = AbilityState.READY;
                }
                break;
        }
    }



    protected override void PreActivate()
    {
    }

    protected override void Activate()
    {
        castLepus();
    }

    protected override void PostActivate()
    {
    }



    private void castLepus()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraBrain.transform.position, cameraBrain.transform.forward);

        // initiate bolt
        GameObject bolt = Instantiate(lepusBoltPrefab, orbTransform.position, Quaternion.identity);
        LepusBoltContorller lepusBoltContorller = bolt.GetComponent<LepusBoltContorller>();
        lepusBoltContorller.damage = config.damage;

        // detect wall
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log(hit.collider.gameObject.name);
            lepusBoltContorller.target = hit.point;
        }
        else
        {
            //Debug.Log("Error, no hit detected");
            lepusBoltContorller.target = ray.GetPoint(500f);
        }

        lepusBoltContorller.flyingSpeed = config.flyingSpeed;
    }



    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawRay(Orb.position, (target - Orb.position).normalized * 100);
    }
}
