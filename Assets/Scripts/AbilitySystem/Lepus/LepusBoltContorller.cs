using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
//using System;

public class LepusBoltContorller : MonoBehaviour
{
    //[SerializeField] private GameObject impactVFXPrefab;
    [SerializeField] private float impactTime;
    [SerializeField] private float maxLifeTime;
    [SerializeField] private GameObject impactVFXPrefab;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public float flyingSpeed;

    public float damage;
    void Start()
    {
        SetProjectileRoute();
    }

    void Update()
    {
        //float scale = Mathf.PingPong(Time.time * 2, 1) + 1f;
        //transform.localScale = Vector3.one * scale / 4f;

        // for missed projectile
        if (maxLifeTime > 0)
        {
            maxLifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetProjectileRoute()
    {
        //float divergeStrength = 30f;
        iTween.MoveTo(gameObject, iTween.Hash("position", target, "speed", flyingSpeed));//, "easetype", iTween.EaseType.easeInQuad

        //iTween.PunchPosition(
        //    gameObject,
        //    iTween.Hash(
        //                "x", Random.Range(-divergeStrength, divergeStrength),
        //                "y", Random.Range(-divergeStrength, divergeStrength),
        //                "z", Random.Range(-divergeStrength, divergeStrength),
        //                "delay", 0.5,
        //                "looptype", "loop",
        //                "time", 3f));            //new Vector3(Random.Range(-divergeStrength, divergeStrength), Random.Range(-divergeStrength, divergeStrength), Random.Range(-divergeStrength, divergeStrength)),

    }

    private void OnCollisionEnter(Collision collision)
    {
        // ignore player
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
        }


        if (collision.gameObject.tag == "Enemy" ||
            collision.gameObject.tag == "Wall" ||
            collision.gameObject.tag == "Water"
            )
        {
            // impact effect
            //VisualEffect vfx = GetComponent<VisualEffect>();
            //vfx.SetFloat("impactFloat", 500f);
         

            ContactPoint contactPoint = collision.GetContact(0);

            var impact = Instantiate(impactVFXPrefab, contactPoint.point + contactPoint.normal * 0.001f, Quaternion.LookRotation(contactPoint.normal)) as GameObject;

            // water ripple
            if (collision.gameObject.tag == "Water")
            {
                GetComponentInChildren<SpriteRenderer>().enabled = true;
            }

            Destroy(impact, impactTime);
            Destroy(gameObject, 0.01f);
        }



        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<LivingEntity>().TakeDamage(damage);
        }
    }
}
