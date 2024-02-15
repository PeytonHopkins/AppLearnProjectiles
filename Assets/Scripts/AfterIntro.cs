using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterIntro : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("MultiplayerStart");
    }
}
