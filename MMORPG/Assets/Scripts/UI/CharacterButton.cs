using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    private CharacterData characterData;

    // Call this method to set the character data for this button
    public void SetCharacterData(CharacterData data)
    {
        characterData = data;
    }

    // Call this method when the button is clicked
    public void OnCharacterButtonClick()
    {
        // You can now access characterData to load the corresponding prefab or perform other actions
        // For example, you might send the characterData to a character loading script
        // that loads the character prefab and displays it on the character selection screen
    }
}
