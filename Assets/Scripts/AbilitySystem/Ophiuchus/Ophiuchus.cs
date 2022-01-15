using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ophiuchus : BaseAbility
{
    // indicator
    public GameObject ophiuchusIndicatorPrefab;
    public GameObject ophiuchusStellationPrefab;

    // ability effect
    public GameObject ophiuchusPrefab;


    // timing
    private OphiuchusConfig config;
    [SerializeField] private LayerMask layerMask;

    private GameObject ophiuchusIndicator;
    private GameObject ophiuchusStellation;
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
        config = (OphiuchusConfig)baseConfig;


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

                    ophiuchusIndicator = Instantiate(ophiuchusIndicatorPrefab, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                    ophiuchusIndicator.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }
                // active ability
                else if (inputManager.InGame.Lepus.triggered && isIndicating)
                {
                    isIndicating = false;

                    abilityState = AbilityState.PREPARING;
                    config.currentPreparingTime = config.preparingTime;
                    spellCenter = ophiuchusIndicator.transform.position;

                    //Destroy(ophiuchusIndicator, config.currentPreparingTime);

                    GenerateBoltsPosition();
                }
                // cancel ability
                else if (config.inputAction.triggered && isIndicating)
                {
                    playerController.canCastSpell = true;
                    isIndicating = false;
                    Destroy(ophiuchusIndicator);
                }
                // indicating
                else if (isIndicating)
                {
                    Ray ray = new Ray(cameraBrain.transform.position, cameraBrain.transform.forward);

                    // detect wall, to make sure the indicator wont be out of the boudary
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        //Debug.Log(hit.collider.gameObject.name);

                        ophiuchusIndicator.transform.localScale = Mathf.Lerp(ophiuchusIndicator.transform.localScale.x, 1, 0.2f) * Vector3.one;
                        ophiuchusIndicator.transform.position = Vector3.Scale(hit.point, new Vector3(1, 0, 1)) + Vector3.up;
                        ophiuchusIndicator.transform.Rotate(new Vector3(0, 0, 1));
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
                    if (ophiuchusIndicator != null)
                        ophiuchusIndicator.transform.localScale = Mathf.Lerp(ophiuchusIndicator.transform.localScale.x, 30, 0.008f) * Vector3.one;
                    config.currentPreparingTime -= Time.deltaTime;

                }
                else
                {
                    abilityState = AbilityState.ACTIVE;
                    config.currentActiveTime = config.activeTime;

                    // start stellation effect
                    ophiuchusStellation.transform.DOPath(
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
        var bolt = Instantiate(ophiuchusPrefab, boltPositions[currentBoltIndex++], Quaternion.identity);
        // self destory;
        DOTween.Sequence()
            .AppendInterval(1f)
            .Append(bolt.transform.DOScale(Vector3.one * Random.Range(15, 30), 2f))
            .Append(bolt.transform.DOPunchScale(Vector3.one * Random.Range(-2, 2), 1f))
            .Append(bolt.transform.DOScale(Vector3.one * 0, 0.2f));
        Destroy(bolt, 5f);

        //dealDamage();

    }

    protected override void PostActivate()
    {
        Destroy(ophiuchusStellation);
        Destroy(ophiuchusIndicator);

    }

    private void GenerateBoltsPosition()
    {
        boltPositions = new Vector3[config.numBolts];

        float angle = 360 / config.numBolts;

        for (int i = 0; i < config.numBolts; i++)
        {
            float distance = Random.Range(config.attackRadius / 2, config.attackRadius);
            float height = Random.Range(0, 30);

            float x = Mathf.Sin(angle * i) * distance;
            float z = Mathf.Cos(angle * i) * distance;

            boltPositions[i] = new Vector3(x, height, z) + spellCenter;
        }
        currentBoltIndex = 0;
        ophiuchusStellation = Instantiate(ophiuchusStellationPrefab, boltPositions[0], Quaternion.identity);
    }




    private void OnDrawGizmos()
    {
        //if (spellCenter == Vector3.zero)
        //    return;

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(spellCenter, config.attackRadius);
    }
}

