using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.EventSystems;

public class InGameUIComponent : MonoBehaviour
{
    public int maxMessages = 25;

    public GameObject chatPanel, textObject;
    public TMP_InputField chatBox;

    [SerializeField]
    private List<Message> messageList = new List<Message>();

    private bool isTyping = false; // Add this flag

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (isTyping)
            {
                SendMessageToChat(chatBox.text);
                chatBox.text = "";
                isTyping = false;
                chatBox.DeactivateInputField(); // Remove focus from the chat box
            }
            else
            {
                isTyping = true;
                EventSystem.current.SetSelectedGameObject(chatBox.gameObject);
                chatBox.ActivateInputField(); // Activate the chat box for typing
            }
        }
    }

    public void SendMessageToChat(string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<TMP_Text>();

        newMessage.textObject.text = newMessage.text;

        messageList.Add(newMessage);
    }
}



[System.Serializable]
public class Message
{
    public string text;
    public TMP_Text textObject;
}
