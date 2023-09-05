using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Net;
using System.IO;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public UIManager loginUI;
    public PlayerSessionIDHolder playerSessionIDHolder;

    private string serverUrl = "http://localhost:8080/api/check-authorization"; // Replace with your server URL.

    public void OnLoginButtonClicked()
    {
        string enteredUsername = usernameInput.text;
        string enteredPassword = passwordInput.text;

        StartCoroutine(CheckCredentials(enteredUsername, enteredPassword));
    }

    private IEnumerator CheckCredentials(string username, string password)
    {
        // Create a web request to the server URL
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);
        request.Method = "POST";

        // Set the content type and length headers (even though the body is empty)
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = 0;

        // Set the credentials in the request headers
        request.Headers.Add("Username", username);
        request.Headers.Add("Password", password);

        try
        {
            // Send the request and get the response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseText = reader.ReadToEnd();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Parse the playerID from the response
                if (int.TryParse(responseText, out int parsedPlayerID))
                {
                    // Credentials are correct, you can call your function here
                    ChangeScene(4, parsedPlayerID);
                }
                else
                {
                    // Handle the case where parsing the playerID failed
                    Debug.LogError("Failed to parse playerID from response: " + responseText);
                }
            }

            else
            {
                // Credentials are incorrect, handle the error (e.g., display an error message).
                Debug.LogError("Incorrect username or password");
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
        Debug.Log("Login successful for : " + playerID + " !");
        loginUI.ChangeMenu(sceneNr);
        
        playerSessionIDHolder.playerID = playerID;
        // Add your code here to perform actions upon successful login.
    }
}
