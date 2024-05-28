using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pre_ArrowPlayer : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public static Vector2 preVelocity;
    float velX;
    float velY;
    float velSqaureX;
    float velSqaureY;
    float initialVelocity;

    void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        velX = preVelocity.x;
        velY = preVelocity.y;
        velSqaureX = velX * velX;
        velSqaureY = velY * velY;

        preVelocity = rb.velocity;

        initialVelocity = Mathf.Sqrt(velSqaureX + velSqaureY);

        Destroy(this.gameObject, 0.3f);

    }
}