using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    public GameObject playerCharacter;
    // Start is called before the first frame update
    void Start()
    {

        //create new gameobject
        GameObject empty = new GameObject();

        //make a list of all objects in the scene
        GameObject[] go = GameObject.FindGameObjectsWithTag("GRASS");  //returns GameObject[]

        //boolean value that is false until we find a suitable grassblock to spawn on
        bool foundSpawnBlock = false;

        int index = 0;
        while (foundSpawnBlock == false)
        {
            Debug.Log("INDEX: " + index);
            //until we find an object named GrassTemp(Clone) look through the list
            if( (go[index]).name == "GrassTemp(Clone)")
            {
                //sets current tile to oil then recursively calls on any neighboring tiles that are water
                Vector3 pos = new Vector3(0f, 1.9f, 0f);
                pos = pos + (go[index]).transform.position;
                Instantiate (playerCharacter, pos, transform.rotation);
                Debug.Log("Spawn Complete");
                foundSpawnBlock = true;
            }   
            index = index +1;
        }
    }
}
