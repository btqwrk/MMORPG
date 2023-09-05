using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.IO;
using TMPro;
using Newtonsoft.Json;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public UIManager loginUI;
    public PlayerSessionIDHolder playerSessionIDHolder;

    private string serverUrl = "http://localhost:8080/api/check-authorization"; // Replace with your server URL.
    private string saltUrl = "http://localhost:8080/api/get-salt";

    public void OnLoginButtonClicked()
    {
        string enteredUsername = usernameInput.text;
        string enteredPassword = passwordInput.text;

        StartCoroutine(CheckCredentials(enteredUsername, enteredPassword));
    }

    private IEnumerator CheckCredentials(string username, string password)
    {
        print(username);
        print(password);
        print("creating json from username : " + username);
        // Create a JSON object to hold the username
        var usernameObject = new
        {
            Username = username
        };

        // Convert the username to JSON
        string usernameJson = JsonConvert.SerializeObject(usernameObject);
        print("username : " + username + " converted to json file : " + usernameJson);

        // Create a web request to the server URL to request the salt
        HttpWebRequest saltRequest = (HttpWebRequest)WebRequest.Create(saltUrl);
        saltRequest.Method = "POST";
        saltRequest.ContentType = "application/json";
        saltRequest.ContentLength = usernameJson.Length;

        try
        {
            // Write the JSON username data to the request stream
            using (var streamWriter = new StreamWriter(saltRequest.GetRequestStream()))
            {
                streamWriter.Write(usernameJson);
            }

            // Send the salt request and get the response
            HttpWebResponse saltResponse = (HttpWebResponse)saltRequest.GetResponse();
            StreamReader saltReader = new StreamReader(saltResponse.GetResponseStream());
            string saltResponseText = saltReader.ReadToEnd();

            if (saltResponse.StatusCode == HttpStatusCode.OK)
            {
                print("Salt recieved in good condition");
                // Parse the response text to get the salt
                string salt = saltResponseText;

                // Hash the password on the client side using the retrieved salt
                string hashedPassword = HashPassword(password, salt);
                print("Hashing password to create jason with hashed password and username for authentication");

                // Create a JSON object to hold the credentials (username and hashed password)
                var credentials = new
                {
                    Username = username,
                    Password = hashedPassword
                };

                // Convert the credentials to JSON
                string json = JsonConvert.SerializeObject(credentials);

                // Create a new web request to send the credentials for authentication
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = json.Length;

                // Write the JSON credentials data to the request stream
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                // Send the authentication request and get the response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseText = reader.ReadToEnd();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Parse the response text as an integer (PlayerID)
                    if (int.TryParse(responseText, out int parsedPlayerID) && parsedPlayerID > 0)
                    {
                        // Credentials are correct, you can call your function here
                        ChangeScene(4, parsedPlayerID);
                    }
                    else
                    {
                        // Handle the case where parsing the playerID failed or is not valid
                        Debug.LogError("Invalid PlayerID received: " + responseText);
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    // Credentials are incorrect or unauthorized, handle the error
                    Debug.LogError("Unauthorized or incorrect username or password");
                }
                else
                {
                    // Handle other HTTP status codes as needed
                    Debug.LogError("HTTP Error: " + response.StatusCode);
                }
            }
            else
            {
                // Handle the case where salt retrieval failed
                Debug.LogError("Failed to retrieve salt from the server");
            }
        }
        catch (WebException ex)
        {
            // Handle any web request errors
            Debug.LogError("Web request error: " + ex.Message);
        }

        yield return null;
    }

    private void ChangeScene(int sceneNr, int playerID)
    {
        // Implement the logic to be executed when the user successfully logs in
        Debug.Log("Login successful for: " + playerID + "!");
        loginUI.ChangeMenu(sceneNr);

        // Store the playerID in the session holder
        playerSessionIDHolder.playerID = playerID; // Assign the correct playerID
    }


    private string HashPassword(string password, string salt)
    {
        // Implement your password hashing logic here.
        // You can use a secure hashing algorithm such as SHA256.
        // Concatenate the password and salt, and then hash them.

        // Example using SHA256 (you can choose a more secure algorithm):
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] saltedPasswordBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
