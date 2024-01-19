using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    private Room currentRoom;
    private Player
    private void Awake()
    {
        currentRoom = PhotonNetwork.CurrentRoom;
        Debug.Log(currentRoom.PlayerCount);

    }
}
