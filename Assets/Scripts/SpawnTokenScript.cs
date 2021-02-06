using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnTokenScript : NetworkBehaviour
{
    public GameObject tokenPrefab;
    public Transform tokenPoints;
    GameObject currentToken;
    NetworkIdentity tokenID;

    public override void OnStartServer()
    {
        base.OnStartServer();
        SpawnToken();
    }

    [Server]
    public void SpawnToken()
    {
        int randPoint = Random.Range(0, tokenPoints.childCount);
        currentToken = Instantiate(tokenPrefab, tokenPoints.GetChild(randPoint).position, Quaternion.identity);
        tokenID = currentToken.GetComponent<NetworkIdentity>();
    }
}
