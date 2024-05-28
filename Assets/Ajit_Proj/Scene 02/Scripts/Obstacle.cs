using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Animator camAnim;



    private void Start()
    {
        camAnim = Camera.main.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            
            ContactPoint2D contact = collision.contacts[0];
            Vector3 contactPoint = contact.point;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, contact.normal);
            Instantiate(particleSystem, contactPoint, rotation);
            camAnim.SetTrigger("Shake");
           
        }
    }
      
}
