using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Player_2 : MonoBehaviour
{
    public GameObject player2;

    void Start()
    {
        player2.transform.Rotate(0, 180, 0);
        Debug.Log("rotation done");
    }
}
