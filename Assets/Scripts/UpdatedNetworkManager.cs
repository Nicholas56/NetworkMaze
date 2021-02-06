using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UpdatedNetworkManager : NetworkManager
{

    public List<Material> playerColors;

    public override void OnServerAddPlayer(NetworkConnection conn, AddPlayerMessage extraMessage)
    {

        if (LogFilter.Debug) Debug.Log("NetworkManager.OnServerAddPlayer");

        if (playerPrefab == null)
        {
            Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object.");
            return;
        }

        if (playerPrefab.GetComponent<NetworkIdentity>() == null)
        {
            Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab.");
            return;
        }

        if (conn.playerController != null)
        {
            Debug.LogError("There is already a player for this connections.");
            return;
        }


        //change the below to pick a prefab to spawn in
        int modelPick = Random.Range(0, playerColors.Count);
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);
        for (int i = 0; i < player.transform.GetChild(0).childCount; i++)
        {
            player.transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>().material = playerColors[modelPick];
        }

        NetworkServer.AddPlayerForConnection(conn, player);
        Debug.Log("player spawned");
        playerColors.RemoveAt(modelPick);

    }

}
