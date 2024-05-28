using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq.Expressions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private List<RoomInfo> roomItems = new List<RoomInfo>();
    public GameObject joinCodeObj;
    private TMP_InputField joinCode;

    public GameObject joinBtn;
    private void Start()
    {
        PhotonNetwork.JoinLobby();
        joinCode = joinCodeObj.GetComponent<TMP_InputField>();
        joinBtn.GetComponent<Button>().interactable = false;
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Connected to Lobby.");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room. Loading Level");
        PhotonNetwork.IsMessageQueueRunning = false;
        SceneManager.LoadScene("SelectTeam");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log(returnCode);
        Debug.Log(message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) // First iteration is everything, after is just the changes
    {
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("UPDATE FOR " + room.Name);
        }

        if (roomItems.Count <= 0) // if room items is empty, update it to the values.
        {
            roomItems = roomList;
        }
        else
        {
            List<RoomInfo> updatedList = roomItems;
            List<int> indexToRemove = new List<int>();
            List<RoomInfo> roomsToAdd = new List<RoomInfo>();

            for (int i = 0; i < roomList.Count; i++)
            {
                bool isNew = true;
                for (int j = 0; j < roomItems.Count; j++)
                {
                    if (roomList[i].Name == roomItems[j].Name)
                    {
                        isNew = false;
                        if (roomList[i].RemovedFromList)
                        {
                            indexToRemove.Add(j);
                        }
                        else
                        {
                            updatedList[j] = roomList[i];
                        }
                    }
                }

                if (isNew)
                {
                    roomsToAdd.Add(roomList[i]);
                }
            }

            for(int i = 0; i < indexToRemove.Count; i++)
            {
                updatedList.RemoveAt(indexToRemove[i]); 
            }

            for(int i = 0; i < roomsToAdd.Count; i++)
            {
                updatedList.Add(roomsToAdd[i]);
            }

            roomItems = updatedList;
        }
        

        foreach(RoomInfo room in roomItems)
        {
            Debug.Log("JOIN CODE: " + joinCode.text.ToString() + "\tLOBBY: " + room.Name.ToString() + "\tEQUAL: " + (joinCode.text.Equals(room.Name)));
        }
    }

    public void OnCreateLobbyClick()
    {
        bool isUnique = true;
        string lobbyCode;
        do
        {
            lobbyCode = RoomCodeGenerator.GenerateRoomCode();
            Debug.Log(lobbyCode);
            for(int i = 0; i < roomItems.Count; i++)
            {
                if(lobbyCode == roomItems[i].Name) // If it is a duplicate room code
                {
                    isUnique = false;
                }
            }
        }
        while (!isUnique); // Loop until a unique value is found.

        // Once it is unique
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.IsVisible = true;
        PhotonNetwork.CreateRoom(lobbyCode, options, null, null); // Create the room.
    }

    public void OnJoinLobbyClick()
    {
        if(joinCode.text.Length == 4)
        {
            PhotonNetwork.JoinOrCreateRoom(joinCode.text, null, null);
        }
        else
        {
            Debug.Log("Invalid code, please enter a code with a length of 4 characters.");
        }
        
    }

    public void OnJoinLobbyValueChange()
    {
        joinCode.text = joinCode.text.ToUpper(); // ensure capitalization as lobby codes wont have lowercase.

        if(joinCode.text == "")
        {
            joinBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            joinBtn.GetComponent<Button>().interactable = true;
        }

        if(joinCode.text.Length > 4)
        {
            joinCode.text = joinCode.text[..4];
        }
    }

}
