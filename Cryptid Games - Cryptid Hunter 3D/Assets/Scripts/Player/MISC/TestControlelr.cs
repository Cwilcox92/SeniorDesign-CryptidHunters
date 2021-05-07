using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


//sprite shaking has something to do with our physics based movement
// will be fixed hopefully with teh implementation of the NetworkPhysiscs handler


/// <summary>
/// 1. 8-directional movement
/// 2. stop and face current direction when input is absent
/// </summary>

public class TestControlelr: MonoBehaviour
{

    public Rigidbody m_RigidBody;
    public float velocity = 5;
    public float turnspeed = 10; // how fast can we turn
     public Animator playerAnimator;

    Vector2 input; // stores our .x for horizontal and .y for vertical 
    float angle; // angle will (for this example) be in 45 degree increments and we will need to know how to rotate the player 
    Quaternion targetRotation; // where we are trying to rotate too
     Transform cam; //keep track of camera angles
    // we need to use the rigidbody instead of the transform to avoid possible collision clipping ()
    // if we have to it wont be hard to implement we will just have to change transform.rotation
    // to myPLayer.GetComponent<RigidBody>().rotation
    // in fact now thinking about it we have have to do this regardless for the mirror network shit

    // GameObject myPlayer;

    public Camera mainCam;    


    public void Start() 
    {
            cam= Camera.main.transform;        

    }
     
    

    
    void FixedUpdate()
    {
                     
        GetInputs();
        if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) //if there is no input
        {
            return;
        }
        CalculateDirection();
        Rotate();
        Move();
        Animate();
        
    }

    /// <summary>
    /// Input based on Horizontal(a,d, <, >) and Vertical (w,s,^,v)
    /// </summary>
    void GetInputs()
    {
        input.x= Input.GetAxisRaw("Horizontal");
        input.y= Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// Direction relative to the camera's rotation
    /// </summary>
    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y); // this gives us a radian
        angle = Mathf.Rad2Deg * angle; // now we have the angle form 0-360 degs
        angle += cam.eulerAngles.y; // what is our cameras y angles then add our calculated angle to that

    }

    /// <summary>
    /// Rotate toward the calculated angle
    /// </summary>
    void Rotate()
    {
        targetRotation= Quaternion.Euler(0, angle, 0); // taking our targetRoation and setting it to the angel calcualted above
        m_RigidBody.rotation= Quaternion.Slerp(m_RigidBody.transform.rotation, targetRotation, turnspeed * Time.deltaTime); // Now we create a smooth lerp to our transform and target with our roation speed
    }

    /// <summary>
    /// This player only moves along its own forwad axis 
    /// </summary>
    void Move()
    {
        m_RigidBody.position += m_RigidBody.transform.forward * velocity * Time.deltaTime;
        //GetComponent<NetworkPhysicsHandler>().ApplyForce(input, ForceMode.VelocityChange);
    }   

    /// <summary>
    /// This controls the animation for the player
    /// </summary>
    void Animate()
    {

        if(Input.GetKey("w") && Input.GetKey("a") && Input.GetKey("s") && Input.GetKey("d"))
        {
            playerAnimator.Play("Idle_N");

        }
        else if(Input.GetKey("w") && Input.GetKey("d"))
        {
            if(Input.GetKey("w") && Input.GetKey("d") != true)
            {
                 playerAnimator.Play("Idle_NW");
            }
            else
            {
                playerAnimator.Play("Run_NW");
            }            
        }
        else if(Input.GetKey("w") && Input.GetKey("a"))
        {

            if(Input.GetKey("w") && Input.GetKey("d") != false)
            {
                 playerAnimator.Play("Idle_NE");
            }
            else
            {
                playerAnimator.Play("Run_NE");
            }
        }
        else if (Input.GetKey("s") && Input.GetKey("a"))
        {
            
            if(Input.GetKey("w") && Input.GetKey("d") == false)
            {
                 playerAnimator.Play("Idle_SE");
            }
            else
            {
                playerAnimator.Play("Run_SE");
            }
        }
        else if (Input.GetKey("s") && Input.GetKey("d"))
        {
             
             if(Input.GetKey("w") && Input.GetKey("d") == false)
            {
                 playerAnimator.Play("Idle_SW");
            }
            else
            {
                playerAnimator.Play("Run_SW");
            }
        }
        else
        {
            if(Input.GetKey("w"))
        {
             if (Input.GetKey("s"))
                    {
                        playerAnimator.Play("Idle_N");
                    }
                    else
                    {
                        playerAnimator.Play("Run_N");
                    }
        }
        if(Input.GetKey("a"))
        {
             //Checks to see if opposite key is pressed
                    if (Input.GetKeyUp("d"))
                    {
                        playerAnimator.Play("Idle_E");
                    }
                    else
                    {
                        playerAnimator.Play("Run_E");
                    }
        }
        if(Input.GetKey("s"))
        {
            if (Input.GetKeyUp("w"))
                    {
                        playerAnimator.Play("Idle_N");
                    }
                    else
                    {
                        playerAnimator.Play("Run_S");
                    }
        }
        if(Input.GetKey("d"))
        {
            if (Input.GetKeyUp("a"))
                    {
                        playerAnimator.Play("Idle_E");
                    }
                    else
                    {
                        playerAnimator.Play("Run_W");
                    }
        }
        }
    }
}



