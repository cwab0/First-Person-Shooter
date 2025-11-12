using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Transform mainCamera;
    [SerializeField] float mouseSensitivity;

    float xRotation;


    void Start()
    {
        mainCamera = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleLooking();
    }

    void HandleLooking()
    {
        Vector2 mouseInput = Mouse.current.delta.ReadValue() * Time.deltaTime;

        xRotation -= mouseInput.y * mouseSensitivity;
        mainCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseInput.x * mouseSensitivity);
    }
}
