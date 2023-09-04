using UnityEngine;
using Mirror;

public class NetworkedPlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 100.0f;
    public float gravity = 9.81f; // Set your desired gravity value

    private Rigidbody rb;
    private Vector3 velocity; // Used for applying gravity
    private Quaternion targetRotation; // Store the target rotation

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation; // Initialize target rotation

        if (isLocalPlayer)
        {
            // Perform any initialization or setup for the local player here
        }
        else
        {
            // Disable the script and other components that should not run on remote players
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        // Handle user input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Rotate the player based on input
        targetRotation *= Quaternion.Euler(0, horizontalInput * rotateSpeed * Time.deltaTime, 0);

        // Smoothly interpolate between the current rotation and the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f);

        // Move the player
        Vector3 moveVector = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);

        // Apply gravity
        if (velocity.y > 0)
        {
            velocity -= Vector3.up * gravity * Time.deltaTime;
        }
    }

    public override void OnStartLocalPlayer()
    {
        // This code runs only on the local player's client
        // Perform any initialization or setup for the local player here
    }
}
