
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    [SerializeField]
    public Transform cameraTransform;
    [SerializeField]
    private CinemachineVirtualCamera aimingCamera;
    [SerializeField]
    private CinemachineFreeLook freeLookCamera;
    //[SerializeField]
    private float virCamfollowOffsetYDefault = 9f;
    //[SerializeField]
    private float virCamFollowOffsetYMax = 20f;
    //[SerializeField]
    private float virCamFollowOffsetYMin = 4f;
    public Vector2 aimingMouseSpeed = new Vector2(0.1f, 0.03f);
    [SerializeField]
    //private Vector2 freeLookMouseSpeed = new Vector2(500f, 150f);
    private Vector2 freeLookMouseSpeed = new Vector2(500f, 1f);

    private InputManager inputManager;
    private bool isSwitchingCamera;
    [HideInInspector] public bool lockingAimCamera;

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

    private void Update()
    {

        // control virtual camera when aiming
        if (inputManager.InGame.MouseAiming.ReadValue<float>() != 0 || lockingAimCamera)
        {
            // facing the camera forward immediatly
            if (isSwitchingCamera)
            {
                transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
                isSwitchingCamera = false;
            }
            // switch camera
            aimingCamera.Priority = 11;

            //float mouseDeltaX = inputManager.InGame.MousePosition.ReadValue<Vector2>().x;
            float mouseDeltaY = -inputManager.InGame.MousePosition.ReadValue<Vector2>().y;

            aimingCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y =
                        Mathf.Clamp(mouseDeltaY * aimingMouseSpeed.y + aimingCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y, virCamFollowOffsetYMin, virCamFollowOffsetYMax);

        }
        else // control free look camera when not aiming
        {
            isSwitchingCamera = true;
            aimingCamera.Priority = 9;
            // rotate
            freeLookCamera.m_XAxis.m_MaxSpeed = freeLookMouseSpeed.x;
            // zoom in/out
            freeLookCamera.m_YAxis.m_MaxSpeed = freeLookMouseSpeed.y;
            aimingCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = virCamfollowOffsetYDefault;

        }
    }

}
