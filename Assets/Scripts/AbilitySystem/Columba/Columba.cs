using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Columba : BaseAbility
{
    public GameObject columbaBoltPrefab;
    public GameObject preColumbaVFXPrefab;
    //public MeshRenderer orbMesh;

    // timing
    private ColumbaConfig config;
    private float currentFireTime;

    private GameObject preColumbaVFX;
    [SerializeField] private LayerMask layerMask;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
        config = (ColumbaConfig)baseConfig;


        switch (abilityState)
        {
            case AbilityState.OVERRIDED:
                // hand over control
                break;
            case AbilityState.READY:
                // TODO: broadcast to inGame UI
                //if (inputManager.InGame.MouseAiming.ReadValue<float>() != 0 &&
                if(inputManager.InGame.Columba.triggered && playerController.canCastSpell)
                {
                    playerController.canCastSpell = false;
                    playerController.gameObject.GetComponent<CameraController>().lockingAimCamera = true;
                    preColumbaVFX = Instantiate(preColumbaVFXPrefab, orbTransform.position, Quaternion.identity, orbTransform);
                    
                    abilityState = AbilityState.PREPARING;
                    config.currentPreparingTime = config.preparingTime;
                }
                break;
            case AbilityState.PREPARING:
                if (config.currentPreparingTime > 0)
                {
                    PreActivate();
                    config.currentPreparingTime -= Time.deltaTime;
                }
                else
                {
                    Destroy(preColumbaVFX);
                    abilityState = AbilityState.ACTIVE;
                    config.currentActiveTime = config.activeTime;
                }
                break;
            case AbilityState.ACTIVE:
                {
                    if (config.currentActiveTime > 0)
                    {
                        config.currentActiveTime -= Time.deltaTime;
                        Activate();
                    }
                    else
                    {
                        abilityState = AbilityState.POSTACTIVE;
                        config.currentCoolDownTime = config.coolDownTime;
                    }
                }
                break;
            case AbilityState.POSTACTIVE:
                PostActivate();
                abilityState = AbilityState.COOLDOWN;
                break;
            case AbilityState.COOLDOWN:
                {
                    playerController.canCastSpell = true;
                    if (config.currentCoolDownTime > 0)
                    {
                        config.currentCoolDownTime -= Time.deltaTime;
                    }
                    else
                    {
                        abilityState = AbilityState.READY;
                        config.currentActiveTime = config.activeTime;
                    }
                }
                break;
        }
    }



    protected override void PreActivate()
    {
        preColumbaVFX.transform.localScale = Vector3.one * Mathf.Lerp(preColumbaVFX.transform.localScale.x, 1f, 0.02f);
        //preColumbaVFX.transform.position = orbTransform.position;
        //if (currentPreparingTime > 0.5f)
        //{
        //    preColumbaVFX.transform.localScale = Vector3.one * Mathf.Lerp(preColumbaVFX.transform.localScale.x, 0.6f, 0.1f);
        //    preColumbaVFX.transform.position = orbTransform.position;
        //}
        //else
        //{
        //    preColumbaVFX.transform.localScale = Vector3.one * Mathf.Lerp(preColumbaVFX.transform.localScale.x, 1f, 0.3f);
        //    preColumbaVFX.transform.position = orbTransform.position;
        //}
    }

    protected override void Activate()
    {

        if (currentFireTime > 0)
        {
            currentFireTime -= Time.deltaTime;
        }
        else
        {
            castColumba();
            currentFireTime = config.fireRate;
        }
    }

    protected override void PostActivate()
    {
        playerController.gameObject.GetComponent<CameraController>().lockingAimCamera = false;

    }




    private void castColumba()
    {
        RaycastHit hit;
        Ray ray = new Ray(cameraBrain.transform.position, cameraBrain.transform.forward);

        // initiate bolt
        GameObject bolt = Instantiate(columbaBoltPrefab, orbTransform.position, Quaternion.identity);
        ColumbaBoltContorller columbaBoltContorller = bolt.GetComponent<ColumbaBoltContorller>();

        // detect wall
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log(hit.collider.gameObject.name);
            columbaBoltContorller.target = hit.point;
        }
        else
        {
            columbaBoltContorller.target = ray.GetPoint(500f);
        }
        columbaBoltContorller.flyingSpeed = config.flyingSpeed;
    }



    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawRay(Orb.position, (target - Orb.position).normalized * 100);
    }
}
