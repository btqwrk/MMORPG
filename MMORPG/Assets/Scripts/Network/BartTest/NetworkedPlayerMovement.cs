using UnityEngine;
using Mirror;

public class NetworkedPlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 100.0f;
    public float gravity = 9.81f; // Set your desired gravity value

    private CharacterController characterController;
    private Vector3 velocity; // Used for applying gravity
    public Camera playerCamera; // Reference to the player's camera

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Disable the camera by default for all players
        if (!isLocalPlayer)
        {
            playerCamera.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        // Handle user input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        // Rotate the player based on input
        transform.Rotate(Vector3.up * horizontalInput * rotateSpeed * Time.deltaTime);

        // Move the player
        Vector3 moveVector = transform.forward * verticalInput * moveSpeed * Time.deltaTime;
        characterController.Move(moveVector);

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
        playerCamera.gameObject.SetActive(true); // Enable the camera for the local player
    }
}
