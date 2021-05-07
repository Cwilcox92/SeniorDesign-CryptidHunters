using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Billboarding : MonoBehaviour
{
    private GameObject myCamera;
    public bool useStaticBillboard= true;

  /*void Start() {
       myCamera= GameObject.Find("Camera_Player(Clone)");
    }*/
    
    // Makes whatever sprite this sits on face the camera


    private IEnumerator Start() {
        yield return new WaitForSeconds(3.0f);// make the loading screen wait for 7-8 seconds.
        myCamera= GameObject.Find("Camera_Player(Clone)");
        Update();
    }
 
    void Update()
    {
        if(myCamera != null)
            { if(!useStaticBillboard)
            {
                transform.LookAt(myCamera.transform);
            }
            else
            {
                transform.rotation = myCamera.transform.rotation;
            }

            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            Debug.Log("Waiting to receive Camera_Player");
            Debug.Log("I hope nothing else is broken...");
        }
       
       
    }



}

/*private GameObject myCamera;

  void Start() {
      myCamera= GameObject.Find("Camera_Player(Clone)");
    }
    
    // Makes whatever sprite this sits on face the camera
    void Update()
    {
        transform.LookAt(myCamera.transform);
    }*/


/*


    public Camera m_Camera;
	public bool amActive =false;
	public bool autoInit =true;
	GameObject myContainer;	
 
	void Awake(){
		if (autoInit == true){
			m_Camera = GameObject.Find("Camera_Player(Clone)").GetComponent<Camera>();
			amActive = true;
		}
 
		myContainer = new GameObject();
		myContainer.name = "GRP_"+transform.gameObject.name;
		myContainer.transform.position = transform.position;
		transform.parent = myContainer.transform;
	}
 
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate(){
        if(amActive==true){
        	myContainer.transform.LookAt(myContainer.transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up);
	    }
    }

*/


/*private Camera playerCam;

    public bool useStaticBillboard;

    void Start()
    {
        playerCam= GameObject.Find("Test_Player(Clone)").GetComponent<Camera>();
        
    }

    void LateUpdate() {
        if(!useStaticBillboard)
        {
            transform.LookAt(playerCam.transform);
        }
        else
        {
            transform.rotation = playerCam.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        
    }*/