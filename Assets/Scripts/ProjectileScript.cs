﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProjectileScript : NetworkBehaviour
{
    public float destroyAfter = 5;
    public Rigidbody rigidBody;
    public float force = 1000;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // set velocity for server and client. this way we don't have to sync the
    // position, because both the server and the client simulate it.
    void Start()
    {
        rigidBody.AddForce(transform.forward * force);
    }

    // destroy for everyone on the server- only called on server
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    // ServerCallback makes the function only run on servers and not generate warnings if run on a client
    [ServerCallback]
    void OnTriggerEnter(Collider co)
    {
        NetworkServer.Destroy(gameObject);
    }
}