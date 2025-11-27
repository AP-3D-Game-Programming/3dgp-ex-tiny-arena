using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;      // Player die je volgt
    public Vector3 offset;        // Extra offset indien nodig

    [Header("Sensitivity")]
    public float sensX = 100f;
    public float sensY = 100f;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Mouse input ---
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // --- Rotatie toepassen ---
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // --- Player alleen Y laten draaien ---
        player.rotation = Quaternion.Euler(0, yRotation, 0);

        // --- Camera positie laten volgen ---
        transform.position = player.position + offset;
    }
}
