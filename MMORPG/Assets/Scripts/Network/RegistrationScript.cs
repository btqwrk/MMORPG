using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using MMOClassLib;
using Newtonsoft.Json;
using System;
using Unity.VisualScripting;
using YourNamespace;

namespace YourNamespace
{
    public class RegistrationScript : MonoBehaviour
    {
        public TMP_InputField usernameInput;
        public TMP_InputField passwordInput;
        public TMP_Text registrationStatusText;
        public PlayerSessionIDHolder playerIDHolder;

        private int registeredPlayerID;

        public void Register()
        {
            string username = usernameInput.text;
            string password = passwordInput.text;

            StartCoroutine(RegisterCoroutine(username, password));

        }

        IEnumerator RegisterCoroutine(string username, string password)
        {
            // Define your server URL
            string serverUrl = "http://localhost:8080/api/registration";

            // Create a RegistrationData object to hold the registration data
            RegistrationData registrationData = new RegistrationData
            {
                Username = username,
                Password = password
            };

            // Convert the RegistrationData object to JSON
            string json = JsonConvert.SerializeObject(registrationData);

            // Create a UnityWebRequest to send the data as JSON
            UnityWebRequest request = UnityWebRequest.PostWwwForm(serverUrl, "POST");
            byte[] jsonBytes = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            try
            {
                // Registration successful, parse the response JSON to get the player ID
                string responseJson = request.downloadHandler.text;
                RegistrationResponse registrationResponse = JsonConvert.DeserializeObject<RegistrationResponse>(responseJson);

                if (registrationResponse != null && registrationResponse.PlayerID > 0)
                {
                    print(registrationResponse);
                    print(registrationResponse.PlayerID);
                    // Update the player ID in the PlayerSessionIDHolder
                    playerIDHolder.playerID = registrationResponse.PlayerID;
                    registrationStatusText.text = "Registration successful";
                }
                else if (registrationResponse == null)
                {
                    print("nothing came through at all from the server as a player id response");
                }
                else
                {
                    print(registrationResponse);
                    print(registrationResponse.PlayerID);
                    registrationStatusText.text = "Registration failed: Invalid player ID received";
                }

            }
            catch (JsonException ex)
            {
                // Handle JSON parsing error
                registrationStatusText.text = "JSON parsing error: " + ex.Message;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                registrationStatusText.text = "Error: " + ex.Message;
            }
        }

    }
}
