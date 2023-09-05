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
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.Text;

namespace YourNamespace
{
    public class RegistrationScript : MonoBehaviour
    {
        public TMP_InputField usernameInput;
        public TMP_InputField passwordInput;
        public TMP_Text registrationStatusText;
        public PlayerSessionIDHolder playerIDHolder;
        public UIManager UIManager;

        private int registeredPlayerID;

        public void Register()
        {
            string username = usernameInput.text;
            string password = passwordInput.text;

            // Generate a random salt for this registration
            string salt = GenerateSalt();

            // Combine the password with the salt and hash it
            string hashedPassword = HashPassword(password, salt);

            // Create a RegistrationData object with the hashed password
            RegistrationData registrationData = new RegistrationData
            {
                Username = username,
                Password = hashedPassword,
                Salt = salt // Include the salt in the registration data
            };

            StartCoroutine(RegisterCoroutine(registrationData));
        }

        IEnumerator RegisterCoroutine(RegistrationData registrationData)
        {
            // Define your server URL
            string serverUrl = "http://localhost:8080/api/registration";

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
                    // Update the player ID in the PlayerSessionIDHolder
                    playerIDHolder.playerID = registrationResponse.PlayerID;
                    registrationStatusText.text = "Registration successful for player id: " + playerIDHolder.playerID;
                    UIManager.ChangeMenu(1);
                }
                else if (registrationResponse == null)
                {
                    print("nothing came through at all from the server as a player id response");
                }
                else
                {
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

        private string GenerateSalt()
        {
            // Define the size of the salt (e.g., 16 bytes)
            int saltSize = 16;

            // Create a random number generator
            System.Random rng = new System.Random();
            byte[] saltBytes = new byte[saltSize];

            // Fill the salt array with random bytes
            rng.NextBytes(saltBytes);

            // Convert the random bytes to a hexadecimal string
            string salt = BitConverter.ToString(saltBytes).Replace("-", "").ToLower();

            return salt;
        }

        private string HashPassword(string password, string salt)
        {
            string combinedPasswordAndSalt = CombinePasswordWithSalt(password, salt);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPasswordAndSalt));
                string hashedPassword = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }


        private string CombinePasswordWithSalt(string password, string salt)
        {
            // Combine the password with the salt
            return password + salt;
        }
    }
}
