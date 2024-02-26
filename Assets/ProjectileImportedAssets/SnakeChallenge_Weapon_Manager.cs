using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeChallenge_Weapon_Manager : MonoBehaviour
{
    public static int arrowCount = 3;

    public static bool isFireArrowEnabled;

    private void Awake()
    {
        isFireArrowEnabled= false;
    }
}
