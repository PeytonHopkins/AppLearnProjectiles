using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class Arrow : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public static Vector2 vel;
    static public bool hasHit;

    public TrailRenderer lr;

    public ParticleSystem particleSystemPrefab;
    public GameObject ps;
    public PhotonView photonView;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hasHit = false;
    }

    public void SetVelocity(Vector3 vel)
    {
        photonView.RPC("UpdateVelocity", RpcTarget.All, vel);
    }

    [PunRPC]
    private void UpdateVelocity(Vector3 vel)
    {
        rb.velocity = vel;
    }

    void Update()
    {
        vel = rb.velocity;

        if (hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
           
        }

        Destroy(this.gameObject, 4.8f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            lr.enabled = false;
            ps.SetActive(false);

        }
        else if (collision.gameObject.CompareTag("Bounce"))
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg * -1;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
