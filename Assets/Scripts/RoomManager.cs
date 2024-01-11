using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;
using UnityEditor;
using static Photon.Pun.UtilityScripts.PunTeams;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject roomTextObj;
    private TextMeshProUGUI roomName;
    private Room currentRoom;

    public GameObject player;

    private PhotonView photonView;

    [SerializeField]
    private int[] teamAmount; // A, B
    [SerializeField]
    private TextMeshProUGUI[] teamTexts;
    [SerializeField]
    private Button[] teamButtons;

    [SerializeField]
    private int teamMax = 2;

    public string selectedTeam = "";


    // Could probably use a dictionary for name/index pairs. Would be able to restructure a lot of this code, it is pretty inefficient currently. I feel as if I could improve a lot upon it. 
    private void Awake()
    {
        currentRoom = PhotonNetwork.CurrentRoom;
        PhotonNetwork.IsMessageQueueRunning = true;
    }
    private void Start()
    {
        roomName = roomTextObj.GetComponent<TextMeshProUGUI>();
        roomName.text = currentRoom.Name;
        photonView = PhotonView.Get(this);

        //GameObject _player = PhotonNetwork.Instantiate(player.name, Vector3.zero, player.transform.rotation);
        //_player.GetComponent<TestingSetup>().isLocalPlayer();

        if (PhotonNetwork.IsMasterClient)
        {
            teamAmount = new int[] { 0, 0 };
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SetTeamAmount", RpcTarget.All, teamAmount);
        }
    }

    [PunRPC]
    public void AddToTeam(string team)
    {
        int index = TeamToInt(team);
        IterateAmount(index);
        teamTexts[index].text = $"Team {team}\n {teamAmount[index]}/2";
        Debug.Log("ADDING!");
        Debug.Log(teamTexts[index].text);
    }

    [PunRPC]
    public void RemoveFromTeam(string team)
    {
        int index = TeamToInt(team);
        teamAmount[index]--;
        teamTexts[index].text = $"Team {team}\n {teamAmount[index]}/2";
    }

    [PunRPC]
    private void SetTeamAmount(int[] teamAmounts)
    {
        teamAmount = teamAmounts;
        teamTexts[0].text = $"Team A\n {teamAmount[0]}/2";
        if (teamAmount[0] == 2)
        {
            teamButtons[0].interactable = false;
        }
        teamTexts[1].text = $"Team B\n {teamAmount[1]}/2";
        if (teamAmount[1] == 2)
        {
            teamButtons[1].interactable = false;
        }
    }

    private void IterateAmount(int index)
    {
        if(teamAmount[index] == teamMax)
        {
            Debug.Log($"Max amount reached for team {index}");
        }
        else
        {
            teamAmount[index]++;
            if (teamAmount[index] == 2)
            {
                teamButtons[index].interactable = false;
            }
        }
    }

    public void SelectTeam(string team)
    {
        selectedTeam = team;
        photonView.RPC("AddToTeam", RpcTarget.All, team);
        teamButtons[TeamToInt(team)].interactable = false;
    }
    public void SwapTeam()
    {
        switch (selectedTeam)
        {
            case "A":
                SelectTeam("B");
                teamButtons[0].interactable = true;
                photonView.RPC("RemoveFromTeam", RpcTarget.All, "A");
                break;
            case "B":
                SelectTeam("A");
                teamButtons[1].interactable = true;
                photonView.RPC("RemoveFromTeam", RpcTarget.All, "B");
                break;
            default:
                break;
        }

    }

    private int TeamToInt(string team)
    {
        switch (team)
        {
            case "A":
                return 0;
            case "B":
                return 1;
            default:
                return -1;
        }
    }
}