using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;

    Transform mainCamera;

    float xRotation;

    InputAction moveAction;
    InputAction jumpAction;
    InputAction shootAction;
    CharacterController characterController;

    Vector3 velocity;

    [Header("Gun")]
    [SerializeField] Gun gunScript;

    void Start()
    {

        mainCamera = Camera.main.transform;
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        HandleLooking();
        HandleMovement();
        OnShoot();
    }

    #region Movement
    void HandleLooking()
    {
        Vector2 mouseInput = Mouse.current.delta.ReadValue() * Time.deltaTime;

        xRotation -= mouseInput.y * mouseSensitivity;
        mainCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensitivity);
    }

    void HandleMovement()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.y;
        Vector3 horisontalMove = move * moveSpeed;

        Vector3 totalMove = (horisontalMove + velocity) * Time.deltaTime;

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

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(totalMove);
    }
    #endregion

    #region Gun
    void OnShoot()
    {
        if (shootAction.WasPerformedThisFrame())
        {
            gunScript.Shoot();
        }
    }
    #endregion
}
