using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public float gravity = -9.81f;
    public Camera cameraBrain;
    [HideInInspector] public bool canCastSpell; 
    [HideInInspector] public InputManager inputManager;
    private CharacterController characterController;
    private CameraController cameraController;
    
    private void Awake()
    {
        inputManager = new InputManager();
        canCastSpell = true;
    }

    private void OnEnable()
    {
        inputManager.Enable();
    }

    private void OnDisable()
    {
        inputManager.Disable();
    }

    private void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        cameraController = gameObject.GetComponent<CameraController>();
    }

    void Update()
    {
        Vector2 moveInput = inputManager.InGame.Move.ReadValue<Vector2>();
        Vector3 moveVector = new Vector3(moveInput.x, 0, moveInput.y);
        moveVector = cameraBrain.transform.forward * moveVector.z + cameraBrain.transform.right * moveVector.x;
        moveVector.y = 0;

        // movement
        moveVector.y = gravity;
        characterController.Move(moveVector * Time.deltaTime * moveSpeed);

        // rotation
        // aiming
        if (inputManager.InGame.MouseAiming.ReadValue<float>() != 0 || cameraController.lockingAimCamera)
        {
            float mouseDeltaX = inputManager.InGame.MousePosition.ReadValue<Vector2>().x;
            transform.Rotate(new Vector3(0f, mouseDeltaX * cameraController.aimingMouseSpeed.x, 0f));
        }
        // free look
        else
        {
            if (moveInput != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + cameraBrain.transform.eulerAngles.y;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
            }
        }
    }
}
