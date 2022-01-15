using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrusIndicatorController : MonoBehaviour
{

    [SerializeField] private Transform VFX01;
    [SerializeField] private Transform VFX02;
    [SerializeField] private float VFX01RotationSpeed;
    [SerializeField] private float VFX02RotationSpeed;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        VFX01.Rotate(new Vector3(0,0, VFX01RotationSpeed*Time.deltaTime));
        VFX02.Rotate(new Vector3(0, 0, VFX02RotationSpeed * Time.deltaTime));

    }
}
