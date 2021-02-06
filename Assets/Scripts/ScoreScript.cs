using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
public class ScoreScript : NetworkBehaviour
{
    public List<uint> players;
    public List<TMP_Text> playerScoreTxts;

    //[SyncVar] public TMP_Text scoreTxt;

    void Start()
    {
        players = new List<uint>();
    }

    [Server]
    public void WriteScore(uint playerID, int playerScore)
    {
        int pNum = players.IndexOf(playerID);
        playerScoreTxts[pNum].text = "" + playerScore;
    }

    [Server]
    public void AddIdentity(uint playerID) 
    {
        players.Add(playerID);
    }
}
