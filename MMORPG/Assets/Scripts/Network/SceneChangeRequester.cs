using UnityEngine;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneChangeRequester : MonoBehaviour
{
    public string serverUrl = "http://localhost:8080/api/change-scene/"; // Replace with your server URL.

    [SerializeField]
    private GameManager gameManager; // Reference to the GameManager object in the Inspector.

    public async void RequestSceneChange(string sceneName)
    {
        // Prepare the data to send in the request body.
        var data = new
        {
            SceneName = sceneName
        };

        // Serialize the data to JSON.
        string jsonData = JsonUtility.ToJson(data);

        // Create an HttpClient instance.
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                // Send the POST request to the remote server.
                var response = await httpClient.PostAsync(serverUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    // Scene change request was successful, handle the response.
                    Debug.Log("Scene change request successful");

                    // Ensure the gameManager reference is not null before using it.
                    if (gameManager != null)
                    {
                        gameManager.StartGame(); // Change the game state to Game
                    }
                    else
                    {
                        Debug.LogError("GameManager reference is null.");
                    }
                }
                else
                {
                    // Scene change request failed, handle the error.
                    Debug.LogError("Scene change request failed: " + response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle any web request errors
                Debug.LogError("Web request error: " + ex.Message);
            }
        }
    }
}
