using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkGamePlayerLobby : NetworkBehaviour
{
    [SyncVar] 
    private string displayName = "Loading...";
        

    private NetworkManagerLobby room;
    private NetworkManagerLobby Room
    {
        get
        {
            if (room != null)
            {
                return room;
            } 
            return room = NetworkManager.singleton as NetworkManagerLobby;  // singleton allows us to cast once      


        }
    }

    

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject); // this wil make it wo thwere if we implement a day cycle of some sort the players arent destroyed

        Room.GamePlayers.Add(this);

        
    }

     public override void OnStopClient()
    {        
       Room.GamePlayers.Remove(this);       

    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }




}
