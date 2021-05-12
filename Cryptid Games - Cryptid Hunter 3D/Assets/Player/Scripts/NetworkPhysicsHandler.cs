using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// https://wirewhiz.com/unity-multiplayer-using-mirror/

public class NetworkPhysicsHandler : NetworkBehaviour
{
    public Rigidbody rb;

    [SyncVar]
    public Vector3 Position;
    [SyncVar]
    public Vector3 Rotation;
    [SyncVar]
    public Vector3 Velocity;
    [SyncVar]
    public Vector3 AngularVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NetworkIdentity>().isServer)
        {
            Position = rb.position;
            // Rotation = rb.rotation;
            Velocity = rb.velocity;
            AngularVelocity = rb.angularVelocity;
            rb.position = Position;
            // rb.rotation = Rotation;
            rb.velocity = Velocity;
            rb.angularVelocity = AngularVelocity;
        }

        if (GetComponent<NetworkIdentity>().isClient)
        {
            rb.position = Position + Velocity * (float)NetworkTime.rtt;
            // rb.rotation = Rotation * Quaternion.Euler(AngularVelocity * (float)NetworkTime.rtt);
            rb.velocity = Velocity;
            rb.angularVelocity = AngularVelocity;
        }
    }

    // emergency fallback for reset
    [Command] // commands are code run on the server when called by a client
    public void CmdResetPose()
    {
        rb.position = new Vector3(0, 1, 0);
        rb.velocity = new Vector3();
    }

    // apply force on client side to ignore network jitter
    public void ApplyForce(Vector3 force, ForceMode fMode)
    {
        rb.AddForce(force, fMode);
        CmdApplyForce(force, fMode);
    }

    public void CmdApplyForce(Vector3 force, ForceMode fMode)
    {
        rb.AddForce(force, fMode); // apply force on server side
    }
}
