using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 2.0f;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");

        // Rotate the camera around the player
        Quaternion rotation = Quaternion.Euler(0, mouseX * rotationSpeed, 0);
        offset = rotation * offset;

        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
