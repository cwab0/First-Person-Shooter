using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    Transform mainCam;

    [Header("Movement")]
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintMultiplier;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;
    [SerializeField] int normalFOV;
    [SerializeField] int sprintFOV;
    [SerializeField] float FOVSmoothTime;
    float FOVCurrentVelo;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction sprintAction;

    float xRotation;

    Vector3 velocity;

    [Header("Interact")]
    [SerializeField] int interactRange;
    [SerializeField] LayerMask interactLayerMask;
    CharacterController characterController;
    InputAction interactAction;


    void Start()
    {
        mainCam = Camera.main.transform;
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        interactAction = InputSystem.actions.FindAction("Interact");
        sprintAction = InputSystem.actions.FindAction("Sprint");
    }

    void Update()
    {
        HandleLooking();
        HandleMovement();
        Interact();
    }

    #region Movement
    void HandleLooking()
    {
        Vector2 mouseInput = Mouse.current.delta.ReadValue() * Time.deltaTime;

        xRotation -= mouseInput.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        mainCam.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensitivity);
    }

    void HandleMovement()
    {
        // Sets movement
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;

        // Sprinting
        float newMoveSpeed;
        if (sprintAction.IsPressed() && moveVector.y > 0)
        {
            newMoveSpeed = moveSpeed * sprintMultiplier;
            Camera.main.fieldOfView = Mathf.SmoothDamp(normalFOV, sprintFOV, ref FOVCurrentVelo, FOVSmoothTime);
        }
        else
        {
            newMoveSpeed = moveSpeed;
            Camera.main.fieldOfView = Mathf.SmoothDamp(sprintFOV, normalFOV, ref FOVCurrentVelo, FOVSmoothTime);
        }

        // Sets more movement
        Vector3 horisontalMove = move * newMoveSpeed;
        Vector3 totalMove = (horisontalMove + velocity) * Time.deltaTime;

        // Jumping
        if (characterController.isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2;
            }

            if (jumpAction.WasPerformedThisFrame())
            {
                velocity.y = jumpForce;
            }
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;

        // Move player
        characterController.Move(totalMove);
    }
    #endregion

    #region Interact

    void Interact()
    {
        if (interactAction.WasPressedThisFrame())
        {
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, interactRange, interactLayerMask))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    #endregion
}
