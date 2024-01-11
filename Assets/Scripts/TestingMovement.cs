using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horzWalk;
    private float vertWalk;

    private Vector2 walkDir;

    public float speed = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horzWalk = Input.GetAxisRaw("Horizontal");
        vertWalk = Input.GetAxisRaw("Vertical");

        walkDir = new Vector2(horzWalk, vertWalk).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = (walkDir * speed);
    }
}
