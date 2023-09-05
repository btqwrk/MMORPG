using System.Collections;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public enum GameState
{
    Login,
    Game
}

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    [SyncVar(hook = nameof(OnGameStateChange))]
    private GameState gameState = GameState.Login;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate GameManager found. Destroying the extra one.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the game with the Login state
        SetGameState(GameState.Login);
    }

    // This method changes the game state and synchronizes it across the network
    public void SetGameState(GameState newState)
    {
        if (!isServer)
        {
            Debug.LogWarning("Only the server can change the game state.");
            return;
        }

        gameState = newState;
    }

    // This method is called when the game state changes
    private void OnGameStateChange(GameState oldState, GameState newState)
    {
        Debug.Log($"Game state changed: {oldState} -> {newState}");

        // Handle state-specific logic here
        switch (newState)
        {
            case GameState.Login:
                SceneManager.LoadScene("LoginScreen");
                break;

            case GameState.Game:
                // Handle game state logic (e.g., start gameplay)
                SceneManager.LoadScene("Player_Test_Scene");
                break;
        }
    }

    // Example method to transition from Loading to Game state
    public void StartGame()
    {
        OnGameStateChange(GetCurrentGameState(), GameState.Game);
    }

    public void ReturnToCharacterSelection()
    {
        OnGameStateChange(GetCurrentGameState(), GameState.Login);
    }

    public GameState GetCurrentGameState()
    {
        return gameState;
    }
}
