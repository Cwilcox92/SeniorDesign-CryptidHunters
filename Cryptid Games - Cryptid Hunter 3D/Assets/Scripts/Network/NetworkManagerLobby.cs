using System.Collections;
using System.Collections.Generic;
using Mirror;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NetworkManagerLobby : NetworkManager
{
    [SerializeField] private int minPlayers = 2;
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;
    
    [Header("Game")]
    [SerializeField] private NetworkGamePlayerLobby gamePlayerPrefab = null;
    [SerializeField] private GameObject playerSpawnSystem = null;
    [SerializeField] private GameObject AISpawnSystem = null;
    //[SerializeField] private GameObject playerCamera= null;


    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection> OnServerReadied; // this is to ensure that everyone will connect at the same time 

    public List<NetworkRoomPlayerLobby> RoomPlayers {get; } = new List<NetworkRoomPlayerLobby>(); // each player will be able to know all of the other players, this allows us to loop over and find all of their names 
    public List<NetworkGamePlayerLobby> GamePlayers {get; } = new List<NetworkGamePlayerLobby>(); // room players get put into teh gameplayers

    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs"); // we are going to create a resource folder that will contatin all of teh things that we want to spawn in the game 
        foreach(var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab); 
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }

     public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if(numPlayers >= maxConnections) // if we have too many players disconnect them
        {
            conn.Disconnect();
            return;
        }
        if(SceneManager.GetActiveScene().path !=menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if(SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0; // figures out who is the first person in the lobby, their plaer id/count is zero

            NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

            roomPlayerInstance.IsLeader = isLeader; // allows us to tell a client if they are a leader. 


            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if(conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();
            RoomPlayers.Remove(player);

            NotifyPlayerOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer() // this is called only by the leader to stop the server and the game
    {
        RoomPlayers.Clear();
    }

    public void NotifyPlayerOfReadyState()
    {
        foreach( var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        if(numPlayers < minPlayers)
        {
            return false;
        }
        foreach(var player in RoomPlayers)
        {
            if(!player.IsReady)
            {
                return false;
            }
        }
        return true;
    }

    public void StartGame()
    {
        if(SceneManager.GetActiveScene().path == menuScene)
        {
            if(!IsReadyToStart()) {return; }

            ServerChangeScene("Scene_Game_Map"); //the parameter for this method is where we will pass our game scene too, for right now its just a demo           

        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        //From menu to game
        if(SceneManager.GetActiveScene().path == menuScene && newSceneName.StartsWith("Scene_Game"))
        {
            for(int i= RoomPlayers.Count - 1; i >= 0; i--)
            {
                var conn = RoomPlayers[i].connectionToClient;
                var gameplayerInstance = Instantiate(gamePlayerPrefab);
                gameplayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);


                //SpawnLocalCamera(gameObject);// kyle and i added this 

                //NetworkServer.Destroy(conn.identity.gameObject);
                NetworkServer.ReplacePlayerForConnection(conn, gameplayerInstance.gameObject);

            }
        }

        base.ServerChangeScene(newSceneName);
    }

    /*void SpawnLocalCamera(GameObject playerInstance) // this too, Kyle and I 
    {
        GameObject playerCameraInstance= Instantiate(playerCamera, transform.position, transform.rotation);
        playerCameraInstance.GetComponent<CameraController>().target = playerInstance.GetComponent<Rigidbody>();
        playerInstance.GetComponent<PlayerController>().cam = playerCameraInstance.transform;
    }*/

    public override void OnServerSceneChanged(string sceneName) // all client will now have a spawn system owned by the server 
    {
        if(sceneName.StartsWith("Scene_Game"))
        {
            GameObject AISpawnSystemInstance = Instantiate(AISpawnSystem);
            NetworkServer.Spawn(AISpawnSystemInstance);
            GameObject playerSpawnSystemInstance= Instantiate(playerSpawnSystem);
            NetworkServer.Spawn(playerSpawnSystemInstance);
        }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);

    }
}
