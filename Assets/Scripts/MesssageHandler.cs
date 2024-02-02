using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MesssageHandler : MonoBehaviourPunCallbacks
{
    public TMP_InputField textInput;
    public TMP_InputField messageLogBox;

    private PhotonView photonView;

    public void Start()
    {
        photonView = PhotonView.Get(this);
    }

    public void OnTextMessageSend()
    { 
        Debug.Log("Sent" + " " + textInput.text);
        photonView.RPC("SendMessageToAll", RpcTarget.All, textInput.text);
        textInput.text = "";
    }

    [PunRPC]
    private void SendMessageToAll(string message)
    {
        //string fullMessage = "\n" + PhotonNetwork.LocalPlayer.NickName + ":" + message; // Still need to set up logic for Nicknames, once web backend is up we can use that name
        string fullMessage = message + "\n";
        messageLogBox.text += fullMessage;
    }
}
