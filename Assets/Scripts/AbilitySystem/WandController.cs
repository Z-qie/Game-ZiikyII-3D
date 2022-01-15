using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class WandController : MonoBehaviour
{
    // wand holder
    [SerializeField] private Light pointLight;
    [SerializeField] private float wandNormalRotation;
    [SerializeField] private float wandAimingRotation;
    // orb control
    [SerializeField] private Transform orbTransform;
    [SerializeField] private VisualEffect orbVFX;
    [SerializeField] private float orbNormalRotation;
    [SerializeField] private float orbAimingRotation;
    [SerializeField] private float orbNormalIntensity;
    [SerializeField] private float orbAimingIntensity;


    private float currentWandHolderRotationSpeed;
    private InputManager inputManager;

    private void Awake()
    {
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        inputManager.Enable();
    }

    private void OnDisable()
    {
        inputManager.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        // if aiming
        if (inputManager.InGame.MouseAiming.ReadValue<float>() != 0)
        {
            // render orb
            orbTransform.Rotate(new Vector3(0, orbAimingRotation * Time.deltaTime, 0));
            orbVFX.transform.position = orbTransform.position;
            orbVFX.SetFloat("Intensity", orbAimingIntensity);
            pointLight.enabled = true;
            pointLight.intensity = Mathf.Lerp(pointLight.intensity, 100, 0.001f);

            // rotate wand holder
            currentWandHolderRotationSpeed = Mathf.Lerp(currentWandHolderRotationSpeed, wandAimingRotation, 0.01f);
            transform.Rotate(new Vector3(0, currentWandHolderRotationSpeed * Time.deltaTime, 0));
        }
        else
        { // render orb
            orbTransform.Rotate(new Vector3(0, orbNormalRotation * Time.deltaTime, 0));
            orbVFX.SetFloat("Intensity", orbNormalIntensity);
            pointLight.enabled = false;
            pointLight.intensity = 0;

            // rotate wand holder
            currentWandHolderRotationSpeed = wandNormalRotation;
            transform.Rotate(new Vector3(0, currentWandHolderRotationSpeed * Time.deltaTime, 0));
        }
    }


}
