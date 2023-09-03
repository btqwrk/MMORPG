using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class SceneChangeAuthorization : NetworkBehaviour
{
    // Reference to the GameStateManager (you'll need to create this)
    private GameManager gameStateManager;

    private void Start()
    {
        gameStateManager = FindObjectOfType<GameManager>();
    }

    // Server-only method to authorize scene changes
    private bool IsClientAuthorizedToChangeScene(NetworkConnection conn)
    {
        // Check the current game state to determine if scene changes are allowed.
        GameState currentGameState = gameStateManager.GetCurrentGameState();

        // Allow scene changes only when the game is in the "Login" state.
        if (currentGameState == GameState.Login)
            return true;

        // Default: Scene changes are not authorized.
        return false;
    }
}
