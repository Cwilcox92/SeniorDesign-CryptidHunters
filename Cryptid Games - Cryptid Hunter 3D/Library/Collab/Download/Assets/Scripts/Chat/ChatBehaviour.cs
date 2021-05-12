using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using TMPro;


public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private GameObject chatUI = null;
    [SerializeField] private TMP_Text chatText = null; // set the text on the screen 
    [SerializeField] private TMP_InputField inputField = null; 
    [SerializeField] private GameObject playerName;

    private string playerPrefsNameKey;

    private static event Action<string> OnMessage;

    public override void OnStartAuthority() // this will only be for my chat
    {
        chatUI.SetActive(true);

        OnMessage += HandleNewMessage; // subscribe to the event 
    }

    [ClientCallback]    
    private void OnDestroy()
    {
        if (!hasAuthority) {return;}
        
        OnMessage -= HandleNewMessage; // unsubscribe to the event
    }

    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    [Client]
    public void Send(string message)
    {

        if(!Input.GetKeyDown(KeyCode.Return)) {return;} // only if we have pressed enter

        if(string.IsNullOrWhiteSpace(message)) {return;} // check to see if the message is null or whitespace 

        CmdSendMessage(message);

        inputField.text = string.Empty;

    }
    [Command]
     private void CmdSendMessage(string message)
    {
        
        //this is where we validate things
        // so if we dont want profanity and things or that nature
        RpcHandleMessage($"[{PlayerPrefs.GetString("PlayerName")}]: {message}");// okay so there error here is that because PLayerPrefs.GetString() is saved the reason it takes the hosts name is 
                                                                                // because its saved from last time. connectionToClient.connectionId, PlayerPrefs.GetString("PlayerName")
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        // this is called on all clients 
        
        OnMessage?.Invoke($"\n{message}");
    }

    public void HandleCameraController()
    {
        GameObject temp= GameObject.Find("Camera_Player(Clone)");
    }
   

   



    


}
