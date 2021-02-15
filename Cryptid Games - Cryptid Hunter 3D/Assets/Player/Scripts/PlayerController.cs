using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //@James Schuchardt
    //This is a custom made 8 directional movement character controller. It as able to function with four different camera angles.

    //Current Bug:
    //Player Box Collider is going slightly into other box collider
    //allowing for player to no clip and applying a constant force after collision


    public GameObject myPlayer;
    public Camera myCamera;
    public Animator myAnimator;

    private Vector3 offset1 = new Vector3(0f, 0.36f, -1.5f); //Camera 1
    private Vector3 offset2 = new Vector3(1.5f, 0.36f, 0);   //Camera 2
    private Vector3 offset3 = new Vector3(-1.5f, 0.36f, 0);  //Camera 3
    private Vector3 offset4 = new Vector3(0f, 0.36f, 1.5f);  //Camera 4

    // Update is called once per frame
    void Update()
    {
        //Checks and makes sure we idle once we let go
        if (Input.GetKeyUp("w") && Input.GetKeyUp("d"))
        {
            myAnimator.Play("Idle_NW");
        }
        else if (Input.GetKeyUp("w") && Input.GetKeyUp("a"))
        {
            myAnimator.Play("Idle_NE");
        }
        else if (Input.GetKeyUp("s") && Input.GetKeyUp("a"))
        {
            myAnimator.Play("Idle_SW");
        }
        else if (Input.GetKeyUp("s") && Input.GetKeyUp("d"))
        {
            myAnimator.Play("Idle_SE");
        }
        else
        {

        if(Input.GetKeyUp("w"))
        {
            myAnimator.Play("Idle_N");
        }
        if(Input.GetKeyUp("a"))
        {
            myAnimator.Play("Idle_E");
        }
        if(Input.GetKeyUp("s"))
        {
            myAnimator.Play("Idle_S");
        }
        if(Input.GetKeyUp("d"))
        {
            myAnimator.Play("Idle_W");
        }

        }

        //Camera 1
        //Checks if all four are being pressed and freezes character
        if ( Input.GetKey("w") && Input.GetKey("a") && Input.GetKey("s") && Input.GetKey("d") && (myCamera.transform.position == (myPlayer.transform.position + offset1)))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,0f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            myAnimator.Play("Idle_N");
        }
        //Diagonal Movements
        else if (Input.GetKey("w") && Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset1))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            myAnimator.Play("Run_NW");
            
        }
        else if (Input.GetKey("w") && Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset1))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_NE");
            
        }
        else if (Input.GetKey("s") && Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset1))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,-1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_SE");
            
        }
        else if (Input.GetKey("s") && Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset1))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,-1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_SW");
            
        } //end of diagonal movements
        else
        {
            if(Input.GetKey("w") && (myCamera.transform.position == (myPlayer.transform.position + offset1)))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,1f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("s"))
                {
                    myAnimator.Play("Idle_N");
                }
                else
                {
                    myAnimator.Play("Run_N");
                }
                
            }
            if(Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset1))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,0f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("d"))
                {
                    myAnimator.Play("Idle_E");
                }
                else
                {
                    myAnimator.Play("Run_E");
                }
            }
            if(Input.GetKey("s") && (myCamera.transform.position == myPlayer.transform.position + offset1))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,-1f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("w"))
                {
                    myAnimator.Play("Idle_N");
                }
                else
                {
                    myAnimator.Play("Run_S");
                }
            }
            if(Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset1))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,0f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("a"))
                {
                    myAnimator.Play("Idle_E");
                }
                else
                {
                    myAnimator.Play("Run_W");
                }
            } //end of four directional movements
        }//end of Camera 1

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Camera 2
        //Checks if all four are being pressed and freezes character
        if ( Input.GetKey("w") && Input.GetKey("a") && Input.GetKey("s") && Input.GetKey("d") && (myCamera.transform.position == (myPlayer.transform.position + offset2)))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,0f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            myAnimator.Play("Idle_N");
        }
        //Diagonal Movements
        else if (Input.GetKey("w") && Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset2))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            myAnimator.Play("Run_NW");
            
        }
        else if (Input.GetKey("w") && Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset2))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,-1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_NE");
            
        }
        else if (Input.GetKey("s") && Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset2))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,-1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_SE");
            
        }
        else if (Input.GetKey("s") && Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset2))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_SW");
            
        } //end of diagonal movements
        else
        {
            if(Input.GetKey("w") && (myCamera.transform.position == (myPlayer.transform.position + offset2)))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,0f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("s"))
                {
                    myAnimator.Play("Idle_N");
                }
                else
                {
                    myAnimator.Play("Run_N");
                }
                
            }
            if(Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset2))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,-1f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("d"))
                {
                    myAnimator.Play("Idle_E");
                }
                else
                {
                    myAnimator.Play("Run_E");
                }
            }
            if(Input.GetKey("s") && (myCamera.transform.position == myPlayer.transform.position + offset2))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,0f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("w"))
                {
                    myAnimator.Play("Idle_N");
                }
                else
                {
                    myAnimator.Play("Run_S");
                }
            }
            if(Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset2))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,1f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("a"))
                {
                    myAnimator.Play("Idle_E");
                }
                else
                {
                    myAnimator.Play("Run_W");
                }
            } //end of four directional movements
        }//end of Camera 2

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Camera 3
        //Checks if all four are being pressed and freezes character
        if ( Input.GetKey("w") && Input.GetKey("a") && Input.GetKey("s") && Input.GetKey("d") && (myCamera.transform.position == (myPlayer.transform.position + offset3)))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,0f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            myAnimator.Play("Idle_N");
        }
        //Diagonal Movements
        else if (Input.GetKey("w") && Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset3))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,-1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            myAnimator.Play("Run_NW");
            
        }
        else if (Input.GetKey("w") && Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset3))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_NE");
            
        }
        else if (Input.GetKey("s") && Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset3))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_SE");
            
        }
        else if (Input.GetKey("s") && Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset3))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,-1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_SW");
            
        } //end of diagonal movements
        else
        {
            if(Input.GetKey("w") && (myCamera.transform.position == (myPlayer.transform.position + offset3)))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,0f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("s"))
                {
                    myAnimator.Play("Idle_N");
                }
                else
                {
                    myAnimator.Play("Run_N");
                }
                
            }
            if(Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset3))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,1f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("d"))
                {
                    myAnimator.Play("Idle_E");
                }
                else
                {
                    myAnimator.Play("Run_E");
                }
            }
            if(Input.GetKey("s") && (myCamera.transform.position == myPlayer.transform.position + offset3))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,0f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("w"))
                {
                    myAnimator.Play("Idle_N");
                }
                else
                {
                    myAnimator.Play("Run_S");
                }
            }
            if(Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset3))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,-1f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("a"))
                {
                    myAnimator.Play("Idle_E");
                }
                else
                {
                    myAnimator.Play("Run_W");
                }
            } //end of four directional movements
        }//end of Camera 2

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Camera 4
        //Checks if all four are being pressed and freezes character
        if ( Input.GetKey("w") && Input.GetKey("a") && Input.GetKey("s") && Input.GetKey("d") && (myCamera.transform.position == (myPlayer.transform.position + offset4)))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,0f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            myAnimator.Play("Idle_N");
        }
        //Diagonal Movements
        else if (Input.GetKey("w") && Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset4))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,-1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            myAnimator.Play("Run_NW");
            
        }
        else if (Input.GetKey("w") && Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset4))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,-1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_NE");
            
        }
        else if (Input.GetKey("s") && Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset4))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_SE");
            
        }
        else if (Input.GetKey("s") && Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset4))
        {
            Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,1f);
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

            
            myAnimator.Play("Run_SW");
            
        } //end of diagonal movements
        else
        {
            if(Input.GetKey("w") && (myCamera.transform.position == (myPlayer.transform.position + offset4)))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,-1f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("s"))
                {
                    myAnimator.Play("Idle_N");
                }
                else
                {
                    myAnimator.Play("Run_N");
                }
                
            }
            if(Input.GetKey("a") && (myCamera.transform.position == myPlayer.transform.position + offset4))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(1f,0f,0f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("d"))
                {
                    myAnimator.Play("Idle_E");
                }
                else
                {
                    myAnimator.Play("Run_E");
                }
            }
            if(Input.GetKey("s") && (myCamera.transform.position == myPlayer.transform.position + offset4))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(0f,0f,1f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("w"))
                {
                    myAnimator.Play("Idle_N");
                }
                else
                {
                    myAnimator.Play("Run_S");
                }
            }
            if(Input.GetKey("d") && (myCamera.transform.position == myPlayer.transform.position + offset4))
            {
                Vector3 target = (myPlayer.transform.position) + new Vector3(-1f,0f,0f);
                myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, target, 0.1f);

                //Checks to see if opposite key is pressed
                if (Input.GetKey("a"))
                {
                    myAnimator.Play("Idle_E");
                }
                else
                {
                    myAnimator.Play("Run_W");
                }
            } //end of four directional movements
        }//end of Camera 4
    }
}
