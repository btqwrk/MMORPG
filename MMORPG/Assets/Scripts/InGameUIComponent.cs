using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InGameUIComponent : MonoBehaviour
{
    public int maxMessages = 25;

    public GameObject chatPanel, textObject;

    [SerializeField]
    List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SendMessageToChat("Space bar pressed - jump");
        }
    }

    public void SendMessageToChat(string text)
    {
        if (messageList.Count > maxMessages)
            messageList.Remove(messageList[0]);

        Message newMessage = new Message();

        newMessage.text = text;
        messageList.Add(newMessage);
    }

}

[System.Serializable]
public class Message
{
    public string text;
}
