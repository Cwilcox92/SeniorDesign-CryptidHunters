using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class SynchronizeSeed : NetworkBehaviour
{ 
    [SyncVar(hook= nameof(RandomNumberSyncCallback))]
        int seed;

     public mapGenerator Generator;
     

     void Start() {
         //CmdGetSeed(); 
         if (isServer)
         {
            seed = UnityEngine.Random.Range(1, 10000000); 
         }
         Generator.GenerateWorld(seed);        
     }

     void RandomNumberSyncCallback(int oldNumber, int newNumber)
     {
         //if (isServer) return;
         seed = newNumber;
     }


  /*  public int GetSeed()
    {
        if(seed > 0)
        {
            return seed;
        }
        else
        {
            seed = UnityEngine.Random.Range(1, 10000000);   
            return seed;
        }
    }
    [Command]
    void CmdGetSeed()
    {
        seed= GetSeed();
        
    }*/

}
