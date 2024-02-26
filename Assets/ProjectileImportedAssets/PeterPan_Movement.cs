using UnityEngine;
using UnityEngine.Events;

public class PeterPan_Movement : MonoBehaviour
{
    public PeterPan_Controller_Movement controller;

   
    public static bool isGEMCollected;


    public float runSpeed = 40f;
    float horizontalMove = 0;

    bool jump = false;
    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump= true;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump= false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            isGEMCollected = true;
            
        }


    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Fruit"))
    //    {
    //        isGEMCollected = true;
          
    //    }
    //}

}