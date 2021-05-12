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

public class PlayerController: NetworkBehaviour
{

    [Header("Movement")]
    public Rigidbody m_RigidBody;
    public float velocity = 5;
    public float turnspeed = 5; // how fast can we turn
    public  Vector2 movementDirection;
    public  bool isMoving = false;
    //public bool moving;
    [Header("Animation")]
    public Animator playerAnimator;

    Vector2 input; // stores our .x for horizontal and .y for vertical 
    float angle; // angle will (for this example) be in 90 degree increments and we will need to know how to rotate the player 
    Quaternion targetRotation; // where we are trying to rotate too
    [Header("Camera's")]
    public Transform cam; //keep track of camera angles
    [SerializeField] GameObject playerCamera;
    public AudioSource audioSource;

    // we need to use the rigidbody instead of the transform to avoid possible collision clipping ()
    // if we have to it wont be hard to implement we will just have to change transform.rotation
    // to myPLayer.GetComponent<RigidBody>().rotation
    // in fact now thinking about it we have have to do this regardless for the mirror network shit

    // GameObject myPlayer;
    [Header("Networking")]
    public NetworkIdentity myNetworkID;
    public bool localReadout;
    [Header("Inventory")]
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory playerInventory;
    public GameObject panelMenu; // for opening the inventory
    public Canvas canvasUI;
    //public int counter = 0;
    GameObject slot01= null;
         GameObject slot11= null;
         GameObject slot02= null;
         GameObject slot22= null;
         GameObject craftedItem= null;
    //public bool pressedWoodFirst, pressedOreFirst = false;

    //public AudioSource audio;
    int counter= 0;
    
   


    //public Camera mainCam;    


    public void Start() 
    {

        //cam= mainCam.transform;
       //cam= mainCam.transform;
            playerInventory = new Inventory(UseItem);
                
           myNetworkID = GetComponent<NetworkIdentity>();
           SpawnLocalCamera(gameObject);// kyle and i added this 
           playerCamera= GameObject.Find("Camera_Player(Clone)");
           //playerInventory = new Inventory();
           uiInventory.SetInventory(playerInventory); 
           audioSource= GetComponent<AudioSource>();
           canvasUI.worldCamera = playerCamera.GetComponent<Camera>();
           CraftingSystem craftingSystem = new CraftingSystem();
           // for testing 
           Item item = new Item {itemType = Item.ItemType.Wood, amount= 1};
           craftingSystem.SetItem(item, 0, 0);
          // Debug.Log(craftingSystem.GetItem(0,0));
          /*slot01= playerInventory.GetComponent<GameObject>();
       slot11= playerInventory.Find("slot01.1");
          slot02= playerInventory.Find("slot02");
         slot22= playerInventory.Find("slot02.1");
         craftedItem= playerInventory.Find("createdItemTemplate");*/
            
    }

    void SpawnLocalCamera(GameObject playerInstance) // this too, Kyle and I 
    {
        GameObject playerCameraInstance= Instantiate(playerCamera, transform.position, transform.rotation);
        playerCameraInstance.GetComponent<CameraController>().target = playerInstance.GetComponent<Rigidbody>();
        playerInstance.GetComponent<PlayerController>().cam = playerCameraInstance.transform;
    }
    
    void FixedUpdate()
    {       
        localReadout = myNetworkID.isLocalPlayer;

        if (cam == null)
        {
            return;
        }
       
        if(!localReadout)
        {
            if(cam != null)
            {
                Destroy(cam.gameObject);// we added this in too (Kyle and I)
            } 
            if(canvasUI != null)
            {
                 Destroy(canvasUI.gameObject);
            }
            return;
        }
                   
        GetInputs();
        if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) //if there is no input
        {
           // FindObjectOfType<AudioManager>().Stop("Player_Movement");
            isMoving= false;
            return;
        }
        CalculateDirection();
        Rotate();
        Move();
        //PlaySounds();
  
        Animate();
            
    }
    public void PlaySounds()
    {
        if(Input.GetButtonDown("w"))
        {
            //FindObjectOfType<AudioManager>().Play("Player_Movement");
        }
        
    }



    private void OnTriggerEnter(Collider collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            //Touching item
            playerInventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
        if(collider.tag == "TrapBody")
        {
             CaughtInTrap();
        }
        
    }

    public List<float> CmdGetMyWeights()
    {
        var personalWeightsText = Resources.Load<TextAsset>("weights/playerWeights").ToString();
        string[] weightsRows = personalWeightsText.Split('\n');
        var w = new List<float>();
        foreach (string weightRow in weightsRows)
        {
            var i = float.Parse(weightRow);
            w.Add(i);
        }
        return w;
    }

    /// <summary>
    /// Input based on Horizontal(a,d, <, >) and Vertical (w,s,^,v)
    /// </summary>
    void GetInputs()
    {
       
        input.x= Input.GetAxisRaw("Horizontal");
        input.y= Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(input.x, input.y);
        if(input == Vector2.zero)
        {
            velocity = 0;
            isMoving = false;         
        }
        else
        {
            velocity= 1.7f;
            isMoving= true;
        }
        movementDirection.Normalize(); 
       // Debug.Log("Movement Direction is: " + movementDirection);
       //  Debug.Log("Velocity is: " + velocity);
         if(Input.GetKey("f"))
        
        {
            if(panelMenu != null)
        {
            Animator animator = panelMenu.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("Display");
                animator.SetBool("Display", !isOpen);

            }
        }
           
        }
        playerAnimator.SetFloat("Speed", velocity);  
        if(isMoving)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
                // Debug.Log("IsMoving: " +isMoving);
            }

        }
        else
        {
             if(audioSource.isPlaying)
            {
                audioSource.Stop();
               // Debug.Log("IsMoving: " +isMoving);
            }
            
           // Debug.Log("IsMoving: " +isMoving);
        }
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
        m_RigidBody.rotation= targetRotation;
        //OLD ROTATION CODE;
        //Quaternion.Slerp(m_RigidBody.transform.rotation, targetRotation, turnspeed * Time.deltaTime); // Now we create a smooth lerp to our transform and target with our roation speed
    }

    /// <summary>
    /// This player only moves along its own forwad axis 
    /// </summary>
    void Move()
    {
        
        m_RigidBody.position += m_RigidBody.transform.forward * velocity * Time.deltaTime;
        
        //GetComponent<NetworkPhysicsHandler>().ApplyForce(m_RigidBody.position, ForceMode.VelocityChange);
    }   

    /// <summary>
    /// This controls the animation for the player
    /// Currently the animation are backwards and will need to be fixed, E and W, NW and NE are messed up 4/18/21 
    /// </summary>
    void Animate()
    {
        
        playerAnimator.SetFloat("Horizontal", input.x); 
        playerAnimator.SetFloat("Vertical", input.y);
       
    
    }

    void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Wood:
                Debug.Log("used item conter @: " + counter);
                playerInventory.RemoveItem(new Item {itemType = Item.ItemType.Wood, amount = 2});
                counter++;
                break;
            case Item.ItemType.Ore:
                Debug.Log("used item conter @: " + counter);
                playerInventory.RemoveItem(new Item {itemType = Item.ItemType.Ore, amount = 2});
                //slot01.SetActive(true);
                counter++;
                break;
        }

    }


    private IEnumerator CaughtInTrap() {
        velocity= 0;
        yield return new WaitForSeconds(3.0f);// make the loading screen wait for 7-8 seconds.
       
    }
}



