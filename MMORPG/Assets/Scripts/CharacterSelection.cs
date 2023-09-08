using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class CharacterSelection : MonoBehaviour
{
    public GameObject characterButtonPrefab;
    public Transform buttonPanel;
    public CharacterDataHandler characterDataHandler;

    private void OnEnable()
    {
        // When the character selection UI is enabled, update the character list
        characterDataHandler.UpdateCharacterList();
        print("Updating characters");
    }

    // This method should be called to populate the character selection screen
    public void PopulateCharacterList(List<CharacterData> characters)
    {
        print(characters.Count);
        foreach (var character in characters)
        {
            
            // Instantiate the character button prefab
            GameObject characterButton = Instantiate(characterButtonPrefab, buttonPanel);

            // Access the Text components of the button and set their text
            characterButton.transform.Find("NameText").GetComponent<TMP_Text>().text = character.name;
            characterButton.transform.Find("RaceText").GetComponent<TMP_Text>().text = character.race;
            characterButton.transform.Find("LevelText").GetComponent<TMP_Text>().text = character.level.ToString();
            characterButton.transform.Find("ClassText").GetComponent<TMP_Text>().text = character.characterClass;

            // Attach a script to the button to handle click events
            CharacterButton characterButtonScript = characterButton.GetComponent<CharacterButton>();
            characterButtonScript.SetCharacterData(character);
        }
    }
}
