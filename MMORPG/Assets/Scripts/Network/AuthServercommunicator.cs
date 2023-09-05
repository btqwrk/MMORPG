using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class AuthServerCommunicator : MonoBehaviour
{
    private string serverUrl = "http://localhost:8080/"; // Replace with your server URL

    public IEnumerator GetSaltFromDatabase(string username, Action<string> onSaltReceived, Action<string> onError)
    {
        // Define the API endpoint for getting salt
        string apiUrl = serverUrl + "api/get-salt";

        // Create a request data object
        Dictionary<string, string> requestData = new Dictionary<string, string>
        {
            { "Username", username }
        };

        // Convert the request data to JSON
        string jsonRequest = JsonConvert.SerializeObject(requestData);

        // Create a UnityWebRequest to send the request
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequest);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ProtocolError)
        {
            // Handle the error
            Debug.LogError("Error: " + request.error);
            onError?.Invoke("Error: " + request.error);
        }
        else
        {
            // Parse the response JSON
            string jsonResponse = request.downloadHandler.text;
            Dictionary<string, string> responseData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

            if (responseData.ContainsKey("Salt"))
            {
                // Salt received successfully
                string salt = responseData["Salt"];
                onSaltReceived?.Invoke(salt);
            }
            else
            {
                // Salt not found in the response
                Debug.LogError("Salt not found in the response");
                onError?.Invoke("Salt not found in the response");
            }
        }
    }
}
