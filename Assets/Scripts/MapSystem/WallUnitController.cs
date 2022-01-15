using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUnitController : MonoBehaviour
{
    public float distanceThreshold;

    private Transform player;
    private Vector3 originPosition;
    private ParticleSystem particles;
    // Start is called before the first frame update


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originPosition = transform.position;
        particles = GetComponentInChildren<ParticleSystem>(); // Stores the module in a local variable
        var emission = particles.emission;
        emission.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.position - transform.position).sqrMagnitude <= distanceThreshold * distanceThreshold)
        {
            particles.Play();
            var emission = particles.emission;
            emission.enabled = true;
        }
        else
        {
            particles.Stop();
            var emission = particles.emission;
            emission.enabled = false;
        }
    }
}
