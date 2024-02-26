using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GM_C4 : MonoBehaviour
{
    [SerializeField] SpriteRenderer eggColorInArrow;
    [SerializeField] Sprite[] arrowSprite;

    [SerializeField] GameObject[] pyramidPlatforms;

    static public bool fireArrow;
    public static bool iceArrow;
    public static bool thunderArrow;
    public static bool bossVillianArrow;
    public static bool generalArrow;

    static public bool fireArrowActive;
    public static bool iceArrowActive;
    public static bool thunderArrowActive;


    static public int currentBulletCountred = 5;


    static public int currentBulletCountredblue = 5;


    static public int currentBulletCountyellow = 5;

    [SerializeField] TextMeshProUGUI[] eggCountTMPRO;

    [SerializeField] TextMeshProUGUI playerScoreText;

    [SerializeField] GameObject eggPanel;
    
    static public int playerScoreCount;

    private void Start()
    {
        //generalArrow = true;
        fireArrow = true;
        thunderArrow = false;
        iceArrow = false;
        eggColorInArrow.sprite = arrowSprite[0];
        playerScoreCount = 0;
        bossVillianArrow = false;

    }
    private void Update()
    {
        playerScoreText.text = "Score: " + playerScoreCount;
        //if(Bomb_Player.fireArrowHit == true)
        //{
        //    StartCoroutine(PyramidPlatformDestroyRed());
        //}

        //if (Bomb_Player.thunderArrowHit == true)
        //{
        //   StartCoroutine(PyramidPlatformDestroyBlue());
        //}

        //if (Bomb_Player.iceArrowHit == true)
        //{
        //    StartCoroutine(PyramidPlatformDestroyYellow());


        //}

        if(bossVillianArrow == true)
        {
           // Debug.Log("TRUE");
            eggColorInArrow.sprite = arrowSprite[3];
            currentBulletCountred += 1000;
            currentBulletCountredblue += 1000;
            currentBulletCountyellow+= 1000;
            eggPanel.SetActive(false);
        }

        
        if (pyramidPlatforms[0].activeInHierarchy == true)
        {
            fireArrowActive = true;
            iceArrowActive = false;
            thunderArrowActive= false;
        }

        if (pyramidPlatforms[1].activeInHierarchy == true)
        {
            thunderArrowActive = true;
            fireArrowActive = false;
            iceArrowActive = false;
         
        }

        if (pyramidPlatforms[2].activeInHierarchy == true)
        {
            iceArrowActive = true;
            fireArrowActive = false;
            thunderArrowActive = false;
        }

        eggCountTMPRO[0].text = currentBulletCountred.ToString("F0");
        eggCountTMPRO[1].text = currentBulletCountredblue.ToString("F0");
        eggCountTMPRO[2].text = currentBulletCountyellow.ToString("F0");
    }
    public void RedEgg()
    {
        fireArrow = true;
        thunderArrow = false;
        iceArrow= false;
        eggColorInArrow.sprite = arrowSprite[0];
    }

    public void BlueEgg()
    {
        thunderArrow = true;
        fireArrow = false;
        iceArrow= false;
        eggColorInArrow.sprite = arrowSprite[1];
    }

    public void YellowEgg()
    {
        iceArrow = true;
        thunderArrow = false;
        fireArrow = false;

        eggColorInArrow.sprite = arrowSprite[2];
    }

    //IEnumerator PyramidPlatformDestroyRed()
    //{

    //    yield return new WaitForSeconds(1.5f);
    //    pyramidPlatforms[0].SetActive(false);
    //    pyramidPlatforms[1].SetActive(true);
    //}

    //IEnumerator PyramidPlatformDestroyBlue()
    //{

    //    yield return new WaitForSeconds(1.5f);
    //    pyramidPlatforms[1].SetActive(false);
    //    pyramidPlatforms[2].SetActive(true);

    //}

    //IEnumerator PyramidPlatformDestroyYellow()
    //{

    //    yield return new WaitForSeconds(1.5f);
    //   pyramidPlatforms[2].SetActive(false) ;

    //}
}
