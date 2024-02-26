using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bomb_Player : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public static Vector2 vel;
    static public bool hasHit;
    //[SerializeField] SpriteRenderer arrowImg;
    [SerializeField] Collider2D polygonCollider;
    private Transform anchor;
    static public bool isArrowHitPoison;

    [SerializeField] SpriteRenderer arrow;

    static public bool fireArrowHit;
    public static bool iceArrowHit;
    public static bool thunderArrowHit;

    [SerializeField] Sprite[] arrowSprite;
    [SerializeField] SpriteRenderer arrowSprite2;
    [SerializeField] GameObject arrowGO;

    public static bool isSnakeHit;

    [SerializeField] GameObject psFire;
 


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hasHit = false;
      
    }

    private void Update()
    {
        vel = rb.velocity;

        if (hasHit == false)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        if(Arrow_Collider.isHITArrow == true)
        {
            polygonCollider.enabled = false;
        }
      else if (Arrow_Collider.isHITArrow == false) 
        { polygonCollider.enabled = true;  }

        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Debug.Log("V:" + vel);

        if (this.anchor != null)
        {
            this.transform.position = anchor.transform.position;
            this.transform.rotation = anchor.transform.rotation;
        }
        //}

        Destroy(this.gameObject, 4.8f);

        //if(GM_C4.generalArrow == true)
        //{
        //    arrow.sprite = arrowSprite[4];
        //    arrowSprite2.color = Color.black;
            
            
        //}


        /*
        if (GM_C4.iceArrow == true)
        {
            arrow.sprite = arrowSprite[2];
            arrowSprite2.color = Color.white;
        }

        if (GM_C4.fireArrow == true)
        {
            arrow.sprite = arrowSprite[0];
            arrowSprite2.color = Color.white;
        }

        if (GM_C4.thunderArrow == true)
        {
            arrow.sprite = arrowSprite[1];
            arrowSprite2.color = Color.white;
        }
  
        if(GM_C4.bossVillianArrow == true)
        {
            arrow.sprite = arrowSprite[3];
            arrowSprite2.color = Color.white;
     
        }

        if (SnakeChallenge_Weapon_Manager.isFireArrowEnabled == true)
        {
            psFire.SetActive(true);
        }

        if (SnakeChallenge_Weapon_Manager.isFireArrowEnabled == false)
        {
            psFire.SetActive(false);
        }
        */
    }

    public void Initialize(float power)
    {
        rb.AddForce(transform.right * (power / 2), ForceMode2D.Impulse);
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            hasHit = true;
            //StartCoroutine(ArrowDestroy());
            StartCoroutine(ArrowDestroy1());

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;
           
            //StartCoroutine(ArrowDestroy());
            // Destroy(this.gameObject);


        }

        if (collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(this.gameObject);
        }

            if (collision.gameObject.CompareTag("TargetGreen"))
        {
            //hasHit = true;
            //StartCoroutine(ArrowDestroy());
            StartCoroutine(ArrowDestroy2());

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;
        }



        if (collision.gameObject.CompareTag("Target"))
        {
            Game_Manger_1.score += 50;
            
        }

        if (collision.gameObject.CompareTag("Poison_Green"))
        {
            
            Game_Manger_1.score -= 250;
           StartCoroutine(BoolFuncPoison());
            StartCoroutine(ArrowDestroy2());

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;


        }

        if (collision.gameObject.CompareTag("Poison_Blue"))
        {
            Game_Manger_1.score -= 100;
            StartCoroutine(BoolFuncPoison());
            StartCoroutine(ArrowDestroy2());

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;


        }

        if (collision.gameObject.CompareTag("Poison_Orange"))
        {
            Game_Manger_1.score -= 50;
            StartCoroutine(BoolFuncPoison());
            StartCoroutine(ArrowDestroy2());

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;


        }

        if (collision.gameObject.CompareTag("Poison_Sky_Blue"))
        {
            Game_Manger_1.score -= 200;
            StartCoroutine(BoolFuncPoison());
            StartCoroutine(ArrowDestroy2());

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;



        }

       

        if (collision.gameObject.CompareTag("Poison_Violet"))
        {
            Game_Manger_1.score -= 300;
            StartCoroutine(BoolFuncPoison());
            StartCoroutine(ArrowDestroy2());

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;


        }

        /*
        if (collision.gameObject.CompareTag("Pyramid_Red") && GM_C4.fireArrow == true)
        {
            //hasHit = true;
            //StartCoroutine(ArrowDestroy());
            GM_C4.playerScoreCount += 1;
            StartCoroutine(ArrowDestroy2());
  

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;

            fireArrowHit= true;
        }

        if (collision.gameObject.CompareTag("Pyramid_Blue") && GM_C4.thunderArrow == true )
        {
            //hasHit = true;
            //StartCoroutine(ArrowDestroy());
            
            StartCoroutine(ArrowDestroy2());
            GM_C4.playerScoreCount += 1;

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;

            thunderArrowHit= true;
        }
        if (collision.gameObject.CompareTag("Pyramid_Pink") && GM_C4.iceArrow == true)
        {
            //hasHit = true;
            //StartCoroutine(ArrowDestroy());
            GM_C4.playerScoreCount += 1;
            StartCoroutine(ArrowDestroy2());
          

            GameObject anchor = new GameObject("ARROW_ANCHOR");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;

            iceArrowHit = true;
        }
        */

        // else if (collision.gameObject.CompareTag("TargetBlue"))
        // {
        //     Game_Manger_1.score += 30;

        // }
        //else if (collision.gameObject.CompareTag("TargetPink"))
        // {
        //     Game_Manger_1.score += 10;
        //     Debug.Log(Game_Manger_1.score);
        // }
    }


    IEnumerator BoolFuncPoison()
    {
        isArrowHitPoison= true;
        yield return new WaitForSeconds(0.00001f);
        isArrowHitPoison = false;

    }

    IEnumerator ArrowDestroy1()
    {
        isSnakeHit = true;
        yield return new WaitForSeconds(0.0001f);
        isSnakeHit = false;

    }

    IEnumerator ArrowDestroy2()
    {
        
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);

    }


}
