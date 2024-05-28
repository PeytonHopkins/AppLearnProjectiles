using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public GameObject player;

    Vector3 pos1 = new Vector3(-13f, 8f, -0.03f);
    Vector3 pos2 = new Vector3(10.5f, 7.431278f, -0.03f);

    private PlayerInfo obj;
    private GameObject _player;
    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("PlayerInfo").GetComponent<PlayerInfo>();

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(InstantiateMaster());
        }
        else
        {
            StartCoroutine(InstantiateOther());
        }
    }

    public void DisconnectPlayer()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("JoinLobby");
    }

    IEnumerator InstantiateMaster()
    {
        yield return new WaitForSeconds(1);
        if (obj.GetTeam() == "A")
        {

            _player = PhotonNetwork.Instantiate(player.name, pos1, player.transform.rotation);
        }
        else
        {
            _player = PhotonNetwork.Instantiate(player.name, pos2, new Quaternion(0, 180, 0, 0));
        }

        _player.GetComponent<TestingSetup>().isLocalPlayer();
    }
    IEnumerator InstantiateOther()
    {
        yield return new WaitForSeconds(1);
        if (obj.GetTeam() == "A")
        {

            _player = PhotonNetwork.Instantiate(player.name, pos1, player.transform.rotation);
        }
        else
        {
            _player = PhotonNetwork.Instantiate(player.name, pos2, new Quaternion(0, 180, 0, 0));
        }

        _player.GetComponent<TestingSetup>().isLocalPlayer();
    }
}
