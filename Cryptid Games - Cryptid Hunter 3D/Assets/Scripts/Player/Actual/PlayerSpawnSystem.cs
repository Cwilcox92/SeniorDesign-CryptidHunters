using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

public class PlayerSpawnSystem : NetworkBehaviour
{
    // we want to get a list of teh player prefabs and say that at each index of spawnpoints spawn that prefab with the same index 
    //[SerializeField] public GameObject playerPrefab = null;

    [SerializeField] GameObject playerCamera;

    private static List<Transform> spawnPoints = new List<Transform>(); // this will be our list of where to store the spawn points pos and rot 
     [SerializeField] private List<GameObject> differentPlayers= new List<GameObject>();

    private int nextIndex = 0; // once a player spawns in this will increment in order to know where the player will spawn in 

   private Vector3 pos; // 0.79

// we need these because the spawn system doesnt exist at design time 
// meaning that they are created later 
    public static void AddSpawnPoint(Transform transform)
    {   
        spawnPoints.Add(transform);
        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

    public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnPlayer;
   /* void SpawnLocalCamera(GameObject playerInstance) // this too, Kyle and I 
    {
        GameObject playerCameraInstance= Instantiate(playerCamera, transform.position, transform.rotation);
        playerCameraInstance.GetComponent<CameraController>().target = playerInstance.GetComponent<Rigidbody>();
        playerInstance.GetComponent<PlayerController>().cam = playerCameraInstance.transform;
    }*/
  

    [ServerCallback]
    private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnPlayer;

    [Server]
    public void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

        if(spawnPoint == null)
        {
            Debug.LogError($"Missing spawn point for player {nextIndex}");
            return;
        }
       // SpawnLocalCamera(gameObject);// kyle and i added this 
                    
        GameObject playerInstance = Instantiate(differentPlayers[nextIndex], spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation); // spawn at a spawn point in the same direction the spawn point is facing 
        NetworkServer.Spawn(playerInstance, conn); // spawns them on the other clients, we gain authority here
        NetworkServer.ReplacePlayerForConnection(conn, playerInstance.gameObject);// this will take over the local authority from whatever had it last
        nextIndex++;
    }

}
