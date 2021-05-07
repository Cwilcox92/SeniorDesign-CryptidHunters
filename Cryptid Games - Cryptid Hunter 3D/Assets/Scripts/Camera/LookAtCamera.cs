using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

private GameObject player;
[SerializeField] Transform myTransform;
//private Camera playerCam;


    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.Find("Test_Player(Clone)");
        ////playerCam= player.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.LookAt(player.transform);
    }
}
