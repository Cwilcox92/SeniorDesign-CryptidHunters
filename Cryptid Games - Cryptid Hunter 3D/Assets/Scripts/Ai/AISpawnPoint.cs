using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        AISpawnSystem.AddSpawnPoint(transform); // can be called like this becuase it is static 
    }

    private void OnDestroy()
    {
        AISpawnSystem.RemoveSpawnPoint(transform);

    }

    private void OnDrawGizmos()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1f); // blue shpere at this position witha radi of 1
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2); // green line from our position to the foward postion, this will help you see where they are facing
            // also only seen in the scene view not game view
              
        
    }

}
