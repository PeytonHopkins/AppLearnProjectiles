using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class TeamButton : MonoBehaviour
{
    public string team;
    public TextMeshProUGUI buttonText;
    private RoomManager roomManager;
    private PhotonView photonView;
    public void Start()
    {
        roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
    }

    public void OnClick()
    {
        if(roomManager.selectedTeam == "") // If this player has not selected a team.
        {
            roomManager.SelectTeam(team);
        }
        else
        {
            if(roomManager.selectedTeam != team) // If they select the other team
            {
                roomManager.SwapTeam();
            }
        }
    }
}
