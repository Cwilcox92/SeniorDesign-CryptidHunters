using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AiNetworkController : NetworkBehaviour
{

    [SyncVar]
    GameObject AiBody = null;
    GameObject aiPrefab = (GameObject)Resources.Load("Prefabs/AiTempPrefab", typeof(GameObject));

    [SyncVar]
    //float[,] playerWeights = new float[3,4];
    int loadedPlayers = 0;
    const int weightLength = 3;

    double[] aiWeights = new double[3];

    // testing variables to be removed when new functionality is added
    bool endGame = false;

    // Start is called before the first frame update
    void Start()
    {        
        if (GetComponent<NetworkIdentity>().isServer)
        {
            // fill state machine

            // create AI object
            CmdSpawnAI();

            // TODO: attach state machine to AI somehow
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NetworkIdentity>().isServer)
        {
            // sync AI sensors

            // simulate state transition

            // update AI object
            AiBody = AiBody; // sync var so this handles disseminating the action to the players

            if (endGame)
            {   
                CmdRegressionParing();
            }
        }
    }

    // a server side only command that is used to create the AI object
    [Command]
    private void CmdSpawnAI()
    {
        if (AiBody == null)
        {
            GameObject ai = Instantiate(aiPrefab, new Vector3(1, 1, 1), new Quaternion(0, 0, 0, 0));
            AiBody = ai;
            NetworkServer.Spawn(ai);
        }
    }

    // takes the passed in weights and creates the weights for use in the state machine.
    [Command]
    void CmdCreateGameWeights()
    {
        // only fires once we have everyone loaded so that we can avoid repeated work
        if (loadedPlayers == 4) 
        {
            for (int y = 0; y <= weightLength; y++)
            {
                float sum = 0f;
                for (int x = 0; x <= loadedPlayers; x++)
                {
                    //sum += playerWeights[x, y];
                }
                aiWeights[y] = (float) (sum / loadedPlayers);
            }
        }
    }

    // call this from the client object to pass in the user's weights into the AI framework
    public void SendWeightsToServer(float[] weights)
    {
        // use this to verify we're receiving a vector of the right size
        if (weights.Length == weightLength)
        {
            CmdSendWeightsToServer(weights);
        }
    }

    // the server side sync/loading code that takes the passed info from the non Cmd version
    [Command]
    public void CmdSendWeightsToServer(float[] weights)
    {
        // error checking to ensure the right amount is shared
        if (weights.Length != weightLength)
        {
            return;
        }

        // copy weights into the global store of all player weights
        for (int x = 0; x <= weightLength; x++)
        {
            //playerWeights[loadedPlayers, x] = weights[x];
        }

        loadedPlayers++;
        CmdCreateGameWeights();
    }
    
    [Command]
    private void CmdRegressionParing()
    {
        // pare down and update the weights to change the outcome that led to player victory
        aiWeights[0] += 4; // dummy op

        // send weights back
    }

    public double[] GetAiWeights()
    {
        double[] testWeights = new double[3] { 1.0, 2.0, 3.0 };
        return testWeights;
    }
}
