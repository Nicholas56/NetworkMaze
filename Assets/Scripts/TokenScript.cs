using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TokenScript : NetworkBehaviour
{
    private void Start()
    {
        CameraTopDown.token = transform;
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        NetworkServer.Destroy(gameObject);
    }
}
