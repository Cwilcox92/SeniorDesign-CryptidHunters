using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class TestCameraController : MonoBehaviour
{
   
    public GameObject myPlayer;
    public Vector3 offsetPos; // follow on players x/z plane
    public float moveSpeed = 5; //how fast the camera will move to the offset
    public float turnSpeed = 10;
    public float smoothSpeed = .5f; // how fast eh camera will move around the 45 degree increments around the player

    private Quaternion targetRoation;
    Vector3 targetPos;
    bool smoothRotating = false;

    [Header("Camera")]
    [SerializeField]  public Transform target = null;
    //[SerializeField]  private Camera targetCamera = null;

  
     void FixedUpdate() {
                        
        MoveWithTarget();
        LookAtTarget();

        if(Input.GetKeyDown(KeyCode.Q) && !smoothRotating) // rotating around the left
        {
            StartCoroutine("RotateAroundTarget", 90);
        }
        if(Input.GetKeyDown(KeyCode.E) && !smoothRotating) // rotating around the right
        {
            StartCoroutine("RotateAroundTarget", -90);
        }   
       
    }


    //[ClientCallback]
    // Move the camera to the player position + current camera offset
    // Offset is modified by the RotateAroundTarget cortoutine
    void MoveWithTarget()
    {
        targetPos= target.position + offsetPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
    //[ClientCallback]
    // Use the look vector (target - current) to aimt he camera toward the player
    void LookAtTarget()
    {
        targetRoation= Quaternion.LookRotation(target.position - transform.position);
        transform.rotation= Quaternion.Slerp(transform.rotation, targetRoation, turnSpeed* Time.deltaTime);
    }
    //[ClientCallback]
    //This coroutine can only have one instance running at a time
    // determined by smoothRotating
    IEnumerator RotateAroundTarget(float angle)
    {
        Vector3 vel = Vector3.zero;
        Vector3 targetOffsetPos= Quaternion.Euler(0, angle, 0) * offsetPos;
        float dist= Vector3.Distance(offsetPos, targetOffsetPos);
        smoothRotating= true;

        while(dist > .02f)
        {
            offsetPos = Vector3.SmoothDamp(offsetPos, targetOffsetPos, ref vel, smoothSpeed);
            dist= Vector3.Distance(offsetPos, targetOffsetPos);
            yield return null;
        }

        smoothRotating = false;
        offsetPos = targetOffsetPos;
    }
}
