using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldPlayerController : MonoBehaviour
{

    [Space]
    [Header("Character Attributes:")]
    public float MOVEMENT_BASE_SPEED = 3.5f;// change this value to change the speed at which the characters move 
    private float movementSpeed;
    private float lockPos = 0;
    

    public Vector3 movementDirection;// Because we are moving on a 3D plane we need to use z intead of y

    public Rigidbody playersRigidBody;
    public Animator TheAnimator;
    public BoxCollider playersBoxCollider;
    public GameObject playerObject;

    // Update is called once per frame
    void Update()
    {
        playerObject.transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);
        ProcessInputs();
        Move();
        Animate();
              
    }

    void ProcessInputs()
    {

        movementDirection = new Vector3(Mathf.Round(Input.GetAxis("Horizontal")), lockPos, Mathf.Round(Input.GetAxis("Vertical")));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, lockPos, 1.0f); //clamp makes it to where the player (if using a controller) cannot move faster than everyone else
        movementDirection.Normalize();
    }

    void Move()
    {
        playersRigidBody.velocity =  movementDirection * movementSpeed * MOVEMENT_BASE_SPEED;
        
    }

    void Animate() { 
        // Animations are controlled from here
        // This area might be the cause for the animation mix up
        if(movementDirection != Vector3.zero){
        TheAnimator.SetFloat("Horizontal", -movementDirection.x);
        TheAnimator.SetFloat("Vertical", movementDirection.z);
        }
        TheAnimator.SetFloat("Speed", movementSpeed);
    }




}
