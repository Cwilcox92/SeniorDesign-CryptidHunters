using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")] 
    [SerializeField] private GameObject landingPagePanel = null;
    private GameObject roomPlayer = null;

    public void HostLobby()
    {
        networkManager.StartHost();
        
        landingPagePanel.SetActive(false);
    }

    public void ReturnToLobby()
     {  
        //networkManager.StopHost();
        landingPagePanel.SetActive(true);  
        //roomPlayer = GameObject.Find("RoomPlayer(Clone)");
        //Destroy(roomPlayer);                 
    }

}
