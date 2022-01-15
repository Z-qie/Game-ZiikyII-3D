using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumbaBoltContorller : MonoBehaviour
{
    [SerializeField] private GameObject impactVFXPrefab;
    [SerializeField] private float impactTime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private Vector2 randomImapctScale;
    [SerializeField] private Vector2 randomImpactRotation;
     


    [HideInInspector] public Vector3 target;
    [HideInInspector] public float flyingSpeed;

    private float rippleDetectHeight = 4f;

    void Start()
    {
        SetProjectileRoute();
    }


    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // for missed projectile
        if (maxLifeTime > 0)
        {
            maxLifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        //// water detect
        //if(transform.position.y < rippleDetectHeight)
        //{
        //    GetComponentInChildren<SpriteRenderer>().enabled = true;
        //}
    }

    private void SetProjectileRoute()
    {
        float divergeStrength;
        int numNodes = 6;
        Vector3[] path = new Vector3[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            divergeStrength = (target - transform.position).magnitude / numNodes / 7 * Mathf.Clamp((numNodes - i - 3), 0, numNodes);
            //divergeStrength = (target - transform.position).magnitude / numNodes / 8;

            path[i] =
                transform.position +
                (target - transform.position) * i / numNodes / 3 +
                new Vector3(Random.Range(-divergeStrength, divergeStrength), Random.Range(-divergeStrength, divergeStrength), Random.Range(-divergeStrength, divergeStrength));
        }
        path[numNodes - 1] = target;
        iTween.MoveTo(gameObject, iTween.Hash("path", path, "speed", flyingSpeed));//, "easetype", iTween.EaseType.easeInQuad

        //iTween.PunchPosition(
        //    gameObject,
        //    new Vector3(Random.Range(-divergeStrength, divergeStrength), Random.Range(-divergeStrength, divergeStrength), Random.Range(-divergeStrength, divergeStrength)),
        //    2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ignore player
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
        }


        if (collision.gameObject.tag == "Enemy" ||
           collision.gameObject.tag == "Wall")
            //collision.gameObject.tag == "Water")
        {
            // impact effect
            ContactPoint contactPoint = collision.GetContact(0);
            var impact = Instantiate(impactVFXPrefab, contactPoint.point + contactPoint.normal * 0.001f, Quaternion.LookRotation(contactPoint.normal)) as GameObject;

            float impactScale = Random.Range(randomImapctScale.x, randomImapctScale.y);
            float impactRotation = Random.Range(randomImpactRotation.x, randomImpactRotation.y);
            impact.transform.localScale = new Vector3(impactScale, impactScale, impactScale);
            iTween.PunchRotation(impact, new Vector3(impactRotation, impactRotation, impactRotation), 2f);
            iTween.FadeTo(impact, 0, impactTime);
            //Debug.Log("Hit!");
            Destroy(impact, impactTime);
            Destroy(gameObject);
        }


    }
}
