using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject roomTextObj;
    private TextMeshProUGUI roomName;
    private Room currentRoom;

    public GameObject player;

    private List<int> indexList = new List<int>{1,2,3,4};


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        
    }
    private void Awake()
    {
        currentRoom = PhotonNetwork.CurrentRoom;
        PhotonNetwork.IsMessageQueueRunning = true;
    }
    private void Start()
    {
        roomName = roomTextObj.GetComponent<TextMeshProUGUI>();
        roomName.text = currentRoom.Name;
        GameObject _player = PhotonNetwork.Instantiate(player.name, Vector3.zero, player.transform.rotation);
        _player.GetComponent<TestingSetup>().isLocalPlayer();
    }
 
}
