using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] GameObject shootPointGO;
    [SerializeField] GameObject target;
    [SerializeField] GameObject bombPrefab; 
    float speeed = 10f;

    float targetX;
    float shootPointGOX;

    float dist;
    float nextX;
    float baseY;
    float height;


   

    void Awake()
    {
        rb.GetComponent<Rigidbody2D>();
        shootPointGO = GameObject.FindGameObjectWithTag("ShootPoint");
        target = GameObject.FindGameObjectWithTag("Target");
        bombPrefab = GameObject.FindGameObjectWithTag("Bomb");


      

        //Vector3 relative = transform.InverseTransformPoint(bombPrefab.transform.position);
        //angleProjectile = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;

        //Debug.Log(angleProjectile);
    }



   

    void Update()
    {
       


        targetX = target.transform.position.x;
        shootPointGOX = shootPointGO.transform.position.x;

        dist = targetX - shootPointGOX;
        nextX = Mathf.MoveTowards(bombPrefab.transform.position.x, targetX, speeed * Time.deltaTime);
        baseY = Mathf.Lerp(shootPointGO.transform.position.y, target.transform.position.y, (nextX - shootPointGOX) / dist);
        height = 2 * (nextX - shootPointGOX) * (nextX - targetX) / (-0.25f * dist * dist);

        Vector3 movePos = new Vector3(nextX, baseY + height, bombPrefab.transform.position.z);
        bombPrefab.transform.rotation = LookAtTarget(movePos - bombPrefab.transform.position);
        bombPrefab.transform.position = movePos;
  

        if (bombPrefab.transform.position == target.transform.position)
        {
            Destroy(bombPrefab);
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(this.gameObject);
        }
    }
    public static Quaternion LookAtTarget(Vector2 rotation)
    {
         
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
        
    }



    public void Initialize(float power)
    {
        rb.AddForce(transform.right * (power/2), ForceMode2D.Impulse);
    }
}
