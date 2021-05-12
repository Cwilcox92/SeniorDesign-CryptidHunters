//THIS IS NOT IN USE ANYWHERE AT THE MOMENT 
//DO NOT MAKE EDITS TO THIS SCRIPT
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public string[] staticDirections={"Static_N", "Static_NW", "Static_W", "Static_SW", "Static_S", "Static_SE", "Static_E", "Static_NE"};
    public string[] runDirections= {"Run_N", "Run_NW", "Run_W", "Run_SW", "Run_S", "Run_SE", "Run_E", "Run_NE"};
    int lastDirection;
    private Animator animator;
    private void Awake()
    {
        animator= GetComponent<Animator>();
        float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
        Debug.Log("R1 "+ result1);
        float result2 = Vector2.SignedAngle(Vector2.up, Vector2.left);
        Debug.Log("R2 "+ result1);
        float result3 = Vector2.SignedAngle(Vector2.up, Vector2.down);
        Debug.Log("R3 "+ result1);
    }

    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray= null;

        if(_direction.magnitude < 0.01) // Character is static and velocity is approx 0
        {
            directionArray= staticDirections;            
        }
        else
        {
            directionArray= runDirections;
            lastDirection= DirectionToIdex(_direction); //Get the index of the slice from the vector

        }
        animator.Play(directionArray[lastDirection]);
    }

    //Converts a Vector2 direction from an index to a slice around a circle in a counter-clockwise direction
    private int DirectionToIdex(Vector2 _direction)
    {
        Vector2 normalizeDirection = _direction.normalized;
        float step= 360/8;
        float offset = step/2;

        float angle = Vector2.SignedAngle(Vector2.up,normalizeDirection);
        angle += offset;
        if(angle < 0)
        {
            angle += 360;
        }
        float stepCount = angle/step;
        return Mathf.FloorToInt(stepCount);
    }
}
