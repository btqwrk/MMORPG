using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System;
using UnityEngine.Networking;
using TMPro;

public class CharacterCreation : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Dropdown raceDropdown;
    public TMP_Dropdown genderDropdown;
    public TMP_Dropdown classDropdown;
    public PlayerSessionIDHolder playerSessionIDHolder;

    // Server URL where your character creation API is hosted
    private string serverUrl = "http://localhost:8080/api/charactercreation";

    public void CreateCharacter()
    {
        string characterName = nameInputField.text;
        string selectedRace = raceDropdown.options[raceDropdown.value].text;
        string selectedGender = genderDropdown.options[genderDropdown.value].text;
        string selectedClass = classDropdown.options[classDropdown.value].text;
        int playerid = playerSessionIDHolder.playerID;

        // Get the current time as an integer
        int currentTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        // Extract the last four digits from the current time
        int lastFourDigits = currentTime % 10000;

        // Generate the characterID by appending the last four digits to the playerID
        int CharacterID = playerid * 10000 + lastFourDigits;

        // Create a JSON object or string with the character data
        CharacterData characterData = ScriptableObject.CreateInstance<CharacterData>();
        characterData.playerID = playerid;
        characterData.name = characterName;
        characterData.race = selectedRace;
        characterData.gender = selectedGender;
        characterData.characterClass = selectedClass;
        characterData.level = 1;
        characterData.hp = 100;
        characterData.resource = 100;
        characterData.stamina = 0;
        characterData.intelligence = 0;
        characterData.agility = 0;
        characterData.wisdom = 0;
        characterData.strength = 0;
        characterData.will = 0;
        characterData.accuracy = 0;
        characterData.vitality = 0;
        characterData.headArmorID = 0;
        characterData.chestArmorID = 0;
        characterData.shoulderGuardsID = 0;
        characterData.wristGuardsID = 0;
        characterData.glovesID = 0;
        characterData.beltID = 0;
        characterData.legsArmorID = 0;
        characterData.feetArmorID = 0;
        characterData.mainWeaponID = 0;
        characterData.offHandID = 0;


        string characterDataJson = JsonUtility.ToJson(characterData);

        print(characterDataJson);

        StartCoroutine(SendCharacterData(characterDataJson));
    }

        private IEnumerator SendCharacterData(string data)
    {
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(serverUrl, data))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Character creation failed: " + www.error);
                // Handle failure, such as displaying an error message to the player
            }
            else
            {
                Debug.Log("Character creation successful!");
                // Handle success, such as showing a success message to the player
            }
        }
    }
}
