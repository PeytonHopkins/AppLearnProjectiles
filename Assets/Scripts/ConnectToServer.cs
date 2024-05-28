using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject button;
    public GameObject ConnectingText;
    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        Screen.SetResolution(1280, 720, false);
    }
    public void OnClickStart()
    {
        PhotonNetwork.ConnectUsingSettings();
        button.GetComponent<Button>().interactable = false;
        ConnectingText.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        SceneManager.LoadScene("JoinLobby");
    }
}
