using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveReflective : MonoBehaviour
{
    // We are going to want this to be rendered on the serverside and then given to the other player so that they synced.
    
    public float timeframe = 3f;
    public float moveSpeed = 4f;

    private float currentTime;
    private bool moveRight;

    private Vector2 moveVector = new Vector2();
    void Start()
    {
        currentTime = timeframe;
        moveVector.x = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if(currentTime <= 0)
        {
            currentTime = timeframe;

            moveRight = !moveRight;
            moveVector *= -1;
        }

        transform.Translate(moveVector * Time.deltaTime);
    }
}
