using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using MMOClassLib;
using Newtonsoft.Json;

namespace YourNamespace
{
    public class RegistrationScript : MonoBehaviour
    {
        public TMP_InputField usernameInput;
        public TMP_InputField passwordInput;
        public TMP_Text registrationStatusText;

        public void Register()
        {
            string username = usernameInput.text;
            string password = passwordInput.text;
            StartCoroutine(RegisterCoroutine(username, password));
            Debug.Log(username.ToString());
            Debug.Log(password.ToString());
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

            Debug.Log(registrationData.ToString());

            // Convert the RegistrationData object to JSON
            string json = JsonConvert.SerializeObject(registrationData);
            Debug.Log("JSON to send: " + json);


            // Create a UnityWebRequest to send the data as JSON
            UnityWebRequest request = UnityWebRequest.PostWwwForm(serverUrl, "POST");
            byte[] jsonBytes = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Registration successful
                registrationStatusText.text = "Registration successful";
            }
            else
            {
                // Registration failed
                registrationStatusText.text = "Registration failed: " + request.error;
            }
        }
    }
}
