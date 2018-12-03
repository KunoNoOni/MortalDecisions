using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScreen : MonoBehaviour {

    public Text logScreenMessages;

    private List<string> messages;

	private void Start () 
	{
        messages = new List<string>();
	}

    public void AddMessage(string newMessage)
    {
        messages.Add(newMessage);
        DisplayMessages();
    }

    private void CullMessages()
    {
        if (messages.Count == 13)
            messages.RemoveAt(0);
    }

    private void DisplayMessages()
    {
        CullMessages();
        logScreenMessages.text = "";
        foreach (string message in messages)
        {
            logScreenMessages.text += message + "\r\n";
        }
    }
}
