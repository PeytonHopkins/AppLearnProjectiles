using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button ChatButtonToggle;
    [SerializeField]
    private GameObject ChatContainer;
    private void Start()
    {
        
    }

    public void OnEnableChat()
    {
        Debug.Log("Enable Chat");
        ChatContainer.SetActive(true);
        ChatButtonToggle.gameObject.SetActive(false);
    }

    public void OnExitChat()
    {
        Debug.Log("Disable Chat");
        ChatContainer.SetActive(false);
        ChatButtonToggle.gameObject.SetActive(true);
    }
}
