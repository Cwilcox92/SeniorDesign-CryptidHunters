//SCRIPT CURRENTLY NOT IN USE
//WILL BE USED TO PROCESS USER INPUT FOR CAMERA MOVEMENTs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject playerCharacter;

    private Vector3 offset1 = new Vector3(0f, 0.36f, -1.5f); //first camera in middle

    private Vector3 offset2 = new Vector3(1.5f, 0.36f, 0); //camera to the right      

    private Vector3 offset3 = new Vector3(-1.5f, 0.36f, 0);  //camera to the left 

    private Vector3 offset4 = new Vector3(0f, 0.36f, 1.5f); //opposite camera 


    private void Awake()
    {
        playerCamera.transform.position = playerCharacter.transform.position + offset1;
        playerCamera.transform.rotation = Quaternion.Euler(20f, 0f, 0f);
    }
    
    private void Update()
    {
        if ( (playerCamera.transform.position == playerCharacter.transform.position + offset1) && (Input.GetKeyUp("e"))   )
        {
            //go to offset 3
            playerCamera.transform.position = playerCharacter.transform.position + offset3;
            playerCamera.transform.rotation = Quaternion.Euler(20f, 90f, 0);
            print ("Changed Camera to: offset3 from 1");
        }
        else if ( (playerCamera.transform.position == playerCharacter.transform.position + offset1) && (Input.GetKeyUp("q"))   )
        {
            //go to offset 2
            Debug.Log("Hey you pressed e the first time");
            playerCamera.transform.position = playerCharacter.transform.position + offset2;
            playerCamera.transform.rotation = Quaternion.Euler(20f, -90f, 0);
            print ("Changed Camera to: offset2 from 1");
        }
        else if ( (playerCamera.transform.position == playerCharacter.transform.position + offset2) && (Input.GetKeyUp("e"))   )
        {
            //go to offset 1
            playerCamera.transform.position = playerCharacter.transform.position + offset1;
            playerCamera.transform.rotation = Quaternion.Euler(20f, 0f, 0f);
            print ("Changed Camera to: offset1 from 2");
        }
        else if ( (playerCamera.transform.position == playerCharacter.transform.position + offset2) && (Input.GetKeyUp("q"))   )
        {
            //go to offset 4
            playerCamera.transform.position = playerCharacter.transform.position + offset4;
            playerCamera.transform.rotation = Quaternion.Euler(20f, 180f, 0f);
            print ("Changed Camera to: offset4 from 2");
        }
        else if ( (playerCamera.transform.position == playerCharacter.transform.position + offset3) && (Input.GetKeyUp("e"))   )
        {
            //go to offset 4
            playerCamera.transform.position = playerCharacter.transform.position + offset4;
            playerCamera.transform.rotation = Quaternion.Euler(20f, 180f, 0f);
            print ("Changed Camera to: offset4 from 3");
        }
        else if ( (playerCamera.transform.position == playerCharacter.transform.position + offset3) && (Input.GetKeyUp("q"))   )
        {
            //go to offset 1
            playerCamera.transform.position = playerCharacter.transform.position + offset1;
            playerCamera.transform.rotation = Quaternion.Euler(20f, 0f, 0f);;
            print ("Changed Camera to: offset1 from 3");
        }
        else if ( (playerCamera.transform.position == playerCharacter.transform.position + offset4) && (Input.GetKeyUp("e"))   )
        {
            //go to offset 2
            playerCamera.transform.position = playerCharacter.transform.position + offset2;
            playerCamera.transform.rotation = Quaternion.Euler(20f, -90f, 0);
            print ("Changed Camera to: offset2 from 4");
        }
        else if ( (playerCamera.transform.position == playerCharacter.transform.position + offset4) && (Input.GetKeyUp("q"))   )
        {
            //go to offset 3
            playerCamera.transform.position = playerCharacter.transform.position + offset3;
            playerCamera.transform.rotation = Quaternion.Euler(20f, 90f, 0);
            print ("Changed Camera to: offset3 from 4");
        }
    } 
}
