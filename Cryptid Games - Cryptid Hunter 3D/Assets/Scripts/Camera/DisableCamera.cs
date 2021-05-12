using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCamera : MonoBehaviour
{
    private GameObject playerCameraClone; 
    public CameraController cm;
    public Animator playerAnimator;

    private PlayerController player;

    void Start() 
    {
        playerCameraClone= GameObject.Find("Camera_Player(Clone)");
        //player= GameObject.Find("Test_Player(Clone)").GetComponent<PlayerController>();
        cm = playerCameraClone.GetComponent<CameraController>();
        
    }

    public void Disable()
    {
        cm.disableCameraController = true;
        playerAnimator.SetFloat("Speed", 0);    
      // player.audioSource.Stop();
        //playerCameraClone.SetActive(false);
    }

    public void Enable()
    {
        cm.disableCameraController = false;
       //playerCameraClone.SetActive(true);
    }
}
