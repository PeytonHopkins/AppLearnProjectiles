using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestingMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horzWalk;
    private float vertWalk;

    private Vector2 walkDir;

    public GameObject chatContainer;

    public float speed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        chatContainer = GameObject.Find("ChatContainer");
    }

    void Update()
    {
        horzWalk = Input.GetAxisRaw("Horizontal");
        vertWalk = Input.GetAxisRaw("Vertical");

        walkDir = new Vector2(horzWalk, vertWalk).normalized;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Quit Game
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (chatContainer.activeSelf)
            {
                chatContainer.SetActive(false);
            }
            else
            {
                chatContainer.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = (walkDir * speed);
    }
}
