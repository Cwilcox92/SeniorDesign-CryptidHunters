using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    public Camera myCamera;
    
    // Makes whatever sprite this sits on face the camera
    void Update()
    {
        transform.LookAt(myCamera.transform);
    }
}
