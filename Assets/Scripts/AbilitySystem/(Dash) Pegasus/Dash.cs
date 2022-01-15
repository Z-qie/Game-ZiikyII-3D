using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class Dash : BaseAbility
{
    // visual effect
    //public Material dashMat;
    public Volume dashVolume;
    public float volumeLerp;
    public TrailRenderer dashTrail;
    public ParticleSystem dashStella;
    public SkinnedMeshRenderer cloakMesh; // to be a shade switcher!!!1
    public SkinnedMeshRenderer hatMesh; // to be a shade switcher!!!1
    //private Material originalMat;


    // timing
    private DashConfig config;
    private float originalSpeed;


    protected override void Start()
    {
        base.Start();
        RenderDashEffect(false);
        originalSpeed = playerController.moveSpeed;
        //originalMat = cloakMesh.material;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        config = (DashConfig)baseConfig;

        switch (abilityState)
        {
            case AbilityState.OVERRIDED:
                // hand over control
                break;
            case AbilityState.READY:
                // TODO: broadcast to inGame UI
                if (inputManager.InGame.Dash.triggered)
                {
                    PreActivate();
                    config.currentActiveTime = config.activeTime;
                    abilityState = AbilityState.ACTIVE;
                }

                break;
            case AbilityState.ACTIVE:
                {
                    Activate();
                    if (config.currentActiveTime > 0)
                    {
                        config.currentActiveTime -= Time.deltaTime;
                    }
                    else
                    {
                        abilityState = AbilityState.COOLDOWN;
                        config.currentCoolDownTime = config.coolDownTime;
                    }
                }

                break;
            case AbilityState.COOLDOWN:
                {
                    PostActivate();

                    if (config.currentCoolDownTime > 0)
                    {
                        config.currentCoolDownTime -= Time.deltaTime;
                    }
                    else
                    {
                        abilityState = AbilityState.READY;
                        config.currentActiveTime = config.activeTime;
                        RenderDashEffect(false);
                    }
                }
                break;
        }
    }

    protected override void PreActivate()
    {
        dashVolume.enabled = true;
        RenderDashEffect(true);

        // transform
        playerController.moveSpeed = config.dashSpeed;
        // try dotween
        //playerController.moveSpeed = 0;
        //playerController.transform.DOMove(
        //    playerController.gameObject.transform.position + playerController.gameObject.transform.forward * 40f,
        //    config.activeTime) ;


    }

    protected override void Activate()
    {
        dashVolume.weight = Mathf.Lerp(dashVolume.weight, 1, volumeLerp);

    }

    protected override void PostActivate()
    {
        dashVolume.enabled = false;
        playerController.moveSpeed = originalSpeed;
        dashVolume.weight = Mathf.Lerp(dashVolume.weight, 0, volumeLerp);

    }

    private void RenderDashEffect(bool canRender)
    {
        if (canRender)
        {
            //cloakMesh.material = dashMat;
            //hatMesh.material = dashMat;

            dashTrail.enabled = true;
            dashTrail.emitting = true;
            var emission = dashStella.emission;
            emission.enabled = true;
        }
        else
        {
            //cloakMesh.material = originalMat;
            //hatMesh.material = originalMat;

            //dashTrail.enabled = false;
            dashTrail.emitting = false;

            var emission = dashStella.emission;
            emission.enabled = false;
        }
    }

}
