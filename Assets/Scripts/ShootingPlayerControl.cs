using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingPlayerControl : MonoBehaviour
{
    Vector3 mousePos = Vector3.zero;
    Vector3 releasePos = Vector3.zero;

    GameObject bow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            Debug.Log(0);
        }

        if (Input.GetMouseButtonUp(0))
        {
            releasePos = Input.mousePosition;
            float releaseAngle = Vector3.Angle(mousePos, releasePos);
            Debug.Log($"{releaseAngle}");
        }
    }
}
