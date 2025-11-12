using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Transform mainCamera;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float moveSpeed;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;

    float xRotation;

    InputAction moveAction;
    InputAction jumpAction;
    CharacterController characterController;

    Vector3 velocity;


    void Start()
    {
        mainCamera = Camera.main.transform;
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        HandleLooking();
        HandleMovement();
    }

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
            if(velocity.y < 0)
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
}
