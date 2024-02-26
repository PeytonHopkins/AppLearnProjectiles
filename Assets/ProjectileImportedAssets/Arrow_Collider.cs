using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Collider : MonoBehaviour
{
    static public bool isHITArrow;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target") || collision.gameObject.CompareTag("TargetGreen"))
        {
            if(Bomb_Player.hasHit == false)
            {
                StartCoroutine(Courtine());
            }
            
        }
    }

    IEnumerator Courtine()
    {
        isHITArrow = true;
        yield return new WaitForSeconds(0.5f);
        isHITArrow= false;
    }
}
