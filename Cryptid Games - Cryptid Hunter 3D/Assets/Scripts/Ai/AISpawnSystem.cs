using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

public class AISpawnSystem : NetworkBehaviour
{
    [SerializeField] public GameObject aiPrefab = null;

    private static List<Transform> aispawnPoints = new List<Transform>(); // this will be our list of where to store the spawn points pos and rot 

    private int nextIndex = 0; // once a player spawns in this will increment in order to know where the player will spawn in 

// we need these because the spawn system doesnt exist at design time 
// meaning that they are created later 
    public static void AddSpawnPoint(Transform transform)
    {   
        aispawnPoints.Add(transform);
        aispawnPoints = aispawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    public static void RemoveSpawnPoint(Transform transform) => aispawnPoints.Remove(transform);

    public override void OnStartServer() => NetworkManagerLobby.OnServerReadied += SpawnAI;

    [ServerCallback]
    private void OnDestroy() => NetworkManagerLobby.OnServerReadied -= SpawnAI;

    [Server]
    public void SpawnAI(NetworkConnection conn)
    {
        Transform spawnPoint = aispawnPoints.ElementAtOrDefault(nextIndex);

        if(spawnPoint == null)
        {
            Debug.LogError($"Missing spawn point for AI: {nextIndex}");// index represent the player or AI, in our case this will always be zeroo until more are added
            return;
        }
        GameObject AIInstance = Instantiate(aiPrefab, aispawnPoints[nextIndex].position, aispawnPoints[nextIndex].rotation); // spawn at a spawn point in the same direction the spawn point is facing 
        NetworkServer.Spawn(AIInstance, conn);
        nextIndex++;
    }

}
