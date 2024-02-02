using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView photonView;
    public GameObject player;
    // Start is called before the first frame update

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    void Start()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, Vector3.zero, player.transform.rotation);
        _player.GetComponent<TestingSetup>().isLocalPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
