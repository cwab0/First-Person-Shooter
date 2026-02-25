using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] float sprintSmoothTime;
    float sprintRefFloat;
    float FOVRefFloat;
    float kickbackRefFloat;

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

    float cameraKick;
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

        //xRotation -= cameraKick;

        //cameraKick = Mathf.MoveTowards(cameraKick, 0, Time.deltaTime * 20);
        
        //xRotation = Mathf.SmoothDamp(xRotation, xRotation - kickbackStrength, ref kickbackRefFloat, 0.3f);
    }

    #region Camera
    void HandleLooking()
    {
        Vector2 mouseInput = Mouse.current.delta.ReadValue() * Time.deltaTime;

        xRotation -= mouseInput.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        mainCam.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensitivity);
    }

    public void ApplyKickback(float kickbackStrength)
    {
        //cameraKick = 2;
    }

    #endregion

    #region Movement

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
            Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, sprintFOV, ref FOVRefFloat, FOVSmoothTime);
        }
        else
        {
            //newMoveSpeed = Mathf.SmoothDamp(moveVector.y, moveSpeed, ref currentSprintVelo, sprintSmoothTime);
            newMoveSpeed = moveSpeed;
            Camera.main.fieldOfView = Mathf.SmoothDamp(Camera.main.fieldOfView, normalFOV, ref FOVRefFloat, FOVSmoothTime);
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
