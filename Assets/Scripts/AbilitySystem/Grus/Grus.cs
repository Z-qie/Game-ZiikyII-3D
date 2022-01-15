using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grus : BaseAbility
{
    // indicator
    [SerializeField] private GameObject grusIndicatorPrefab;
    [SerializeField] private GameObject grusStellationPrefab;
    [SerializeField] private float indicatorRotationSpeed;
    [SerializeField] private float indicatorRotationSpeedActive;
    [SerializeField] private float grusImpactRotationSpeed;
    // ability effect
    [SerializeField] private GameObject grusImpactPrefab;

    public GameObject grusPrefab;


    // timing
    private GrusConfig config;
    [SerializeField] private LayerMask layerMask;

    private GameObject grusIndicator;
    private GameObject grusImpact;
    private GameObject grusStellation;
    private bool isIndicating;
    private RaycastHit hit;
    private Vector3 spellCenter;
    private Vector3[] boltPositions;
    private float initializingInterval;
    private float currentInitializeTime;
    private int currentBoltIndex;

    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
        config = (GrusConfig)baseConfig;


        switch (abilityState)
        {
            case AbilityState.OVERRIDED:
                // hand over control
                break;
            case AbilityState.READY:
                // TODO: broadcast to inGame UI

                // active ability indicator
                if (config.inputAction.triggered && playerController.canCastSpell && !isIndicating)
                {
                    playerController.canCastSpell = false;
                    isIndicating = true;

                    grusIndicator = Instantiate(grusIndicatorPrefab, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                    grusIndicator.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
                // active ability
                else if (inputManager.InGame.Lepus.triggered && isIndicating)
                {
                    isIndicating = false;

                    abilityState = AbilityState.PREPARING;
                    config.currentPreparingTime = config.preparingTime;
                    spellCenter = grusIndicator.transform.position;
                    grusImpact = Instantiate(grusImpactPrefab, grusIndicator.transform.position + Vector3.up * 10f, Quaternion.Euler(new Vector3(90, 0, 0)));

                    GenerateBoltsPosition();
                }
                // cancel ability
                else if (config.inputAction.triggered && isIndicating)
                {
                    playerController.canCastSpell = true;
                    isIndicating = false;
                    Destroy(grusIndicator);
                }
                // indicating
                else if (isIndicating)
                {
                    Ray ray = new Ray(cameraBrain.transform.position, cameraBrain.transform.forward);

                    // detect wall, to make sure the indicator wont be out of the boudary
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        //Debug.Log(hit.collider.gameObject.name);

                        // update indicator transform
                        grusIndicator.transform.localScale = Mathf.Lerp(grusIndicator.transform.localScale.x, 0.8f, 0.15f) * Vector3.one;
                        grusIndicator.transform.position = Vector3.Scale(hit.point, new Vector3(1, 0, 1)) + Vector3.up;
                        grusIndicator.transform.Rotate(new Vector3(0, 0, indicatorRotationSpeed * Time.deltaTime));
                    }
                    else
                    {
                        //Debug.Log("Error, no hit detected");
                    }
                }

                break;
            case AbilityState.PREPARING:

                if (config.currentPreparingTime > 0)
                {
                    config.currentPreparingTime -= Time.deltaTime;
                    grusIndicator.transform.Rotate(new Vector3(0, 0, indicatorRotationSpeedActive * Time.deltaTime));
                    grusImpact.transform.Rotate(new Vector3(0, 0, grusImpactRotationSpeed * Time.deltaTime));
                }
                else
                {
                    abilityState = AbilityState.ACTIVE;
                    config.currentActiveTime = config.activeTime;

                    //grusIndicator.transform.DOScale(new Vector3(10, 10, 10), config.activeTime);
                    // start stellation effect
                    grusStellation.transform.DOPath(
                        boltPositions,
                        config.activeTime,
                        pathType: PathType.Linear);

                    initializingInterval = config.activeTime / config.numBolts;
                }
                break;
            case AbilityState.ACTIVE:
                {
                    playerController.canCastSpell = true;
                    if (config.currentActiveTime > 0)
                    {
                        grusIndicator.transform.Rotate(new Vector3(0, 0, indicatorRotationSpeedActive * Time.deltaTime));
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

    }

    protected override void Activate()
    {
        if (currentInitializeTime > 0)
        {
            currentInitializeTime -= Time.deltaTime;
        }
        else
        {
            InitializeOphiuchusBolts();
            currentInitializeTime = initializingInterval;
        }
    }

    private void InitializeOphiuchusBolts()
    {
        var bolt = Instantiate(grusPrefab, boltPositions[currentBoltIndex++], Quaternion.identity);
        bolt.transform.DOShakePosition(3, Vector3.one * 0.3f, 80, 90, false, false);

        DOTween.Sequence()
            .AppendInterval(1f)
            .Append(bolt.transform.DOScale(Vector3.one * Random.Range(10, 20), 3f))
            .AppendInterval(Random.Range(0.2f, 0.5f))
            //.Append(bolt.transform.DOPunchScale(Vector3.one * Random.Range(-2, 2), 1f))
            //.Append(bolt.transform.DOScale(Vector3.one * 50, 0.01f))
            //.Append(bolt.transform.DOScale(Vector3.zero, 0.01f))
            .Append(bolt.transform.DOScale(Vector3.one * 70, 0.015f))
            .Append(bolt.transform.DOScale(Vector3.zero, 0.04f));
        Destroy(bolt, 10f);

        //dealDamage();

    }

    protected override void PostActivate()
    {
        grusImpact.transform.DOScale(Vector3.zero, 2f);
        grusIndicator.transform.DOScale(Vector3.zero, 2f);
        //grusStellation.transform.DOScale(Vector3.one * 100, 1f);

        Destroy(grusImpact, 3f);
        Destroy(grusStellation, 3f);
        Destroy(grusIndicator, config.activeTime + 2);

    }

    private void GenerateBoltsPosition()
    {
        boltPositions = new Vector3[config.numBolts];

        float angle = 360 / config.numBolts;

        for (int i = 0; i < config.numBolts; i++)
        {
            float distance = Random.Range(config.attackRadius / 2, config.attackRadius);
            float height = Random.Range(10, 30);

            float x = Mathf.Sin(angle * i) * distance;
            float z = Mathf.Cos(angle * i) * distance;

            boltPositions[i] = new Vector3(x, height, z) + spellCenter;
        }
        currentBoltIndex = 0;
        grusStellation = Instantiate(grusStellationPrefab, boltPositions[0], Quaternion.identity);
    }




    private void OnDrawGizmos()
    {
        //if (spellCenter == Vector3.zero)
        //    return;

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(spellCenter, config.attackRadius);
    }
}

