using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

public class ServerCommunication : MonoBehaviour
{
    private const string serverURL = "https://yourserver.com/api/character"; // Replace with your server URL

    public IEnumerator RegisterCharacter(CharacterData characterData)
    {
        // Convert characterData to JSON
        string characterDataJson = JsonUtility.ToJson(characterData);

        // Create a request object
        UnityWebRequest request = new UnityWebRequest(serverURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(characterDataJson);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Character registration successful
            Debug.Log("Character registered successfully!");
        }
    }
}
