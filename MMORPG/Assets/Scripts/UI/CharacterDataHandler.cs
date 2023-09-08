using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections;

public class CharacterDataHandler : MonoBehaviour
{
    public CharacterSelection characterSelection;
    public PlayerSessionIDHolder playerSessionIDHolder;

    // Replace with your server's API URL
    private string serverUrl = "http://localhost:8080/api/get-character-data";

    private void Start()
    {
        // Make a request to the server to fetch character data
        StartCoroutine(FetchCharacterData());
    }

    public void UpdateCharacterList()
    {
        // Make a request to the server to fetch character data
        StartCoroutine(FetchCharacterData());
    }

    private IEnumerator FetchCharacterData()
    {
        // Get the player ID from the PlayerSessionIDHolder
        int playerID = playerSessionIDHolder.playerID;

        // Append the player ID to the server URL
        string requestUrl = serverUrl + "?playerID=" + playerID;

        using (UnityWebRequest www = UnityWebRequest.Get(requestUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response into a list of character data
                List<CharacterData> characterList = ParseCharacterData(www.downloadHandler.text);

                // Populate the character selection UI with the received data
                characterSelection.PopulateCharacterList(characterList);
            }
            else
            {
                Debug.LogError("Failed to fetch character data: " + www.error);
            }
        }
    }


    private List<CharacterData> ParseCharacterData(string json)
    {
        // Implement your JSON parsing logic here to convert the JSON string to a list of CharacterData objects.
        // You can use JsonConvert.DeserializeObject or other JSON libraries.
        List<CharacterData> characterList = JsonConvert.DeserializeObject<List<CharacterData>>(json);

        return characterList;
    }
}
