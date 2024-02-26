#region LibClass
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#endregion

public class Game_Manger_1 : MonoBehaviour
{
    [SerializeField] GameObject modularWindow;
    [SerializeField] GameObject DFW;
    [SerializeField] Image dfW_sprite;
    [SerializeField] GameObject[] mw;

    [SerializeField] GameObject[] targets;
    [SerializeField] GameObject[] poisonSpells;


    Vector3 currentPos;
    GameObject aimPoint;
    Vector3 currentPos1;
    GameObject aimPoint1;


    Vector3 transformiButton;

    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject[] swapButtons;
    [SerializeField] GameObject[] Formulas_Front;
    [SerializeField] GameObject[] Formulas_Back;


    static public int score;
    [SerializeField] TextMeshProUGUI playerScore;

    static bool isModeularWindowActive;
    static public bool isDFWActive;

    [SerializeField] GameObject BG1;

    [SerializeField] GameObject ETVX;
    GameObject ETVY;
    [SerializeField] GameObject GameOverText;
    [SerializeField] GameObject GameWonText;
    [SerializeField] GameObject resetButton;


    [SerializeField] GameObject psFire;



    //public float timer, interval = 10f;

    private void Start()
    {
        InvokeRepeating("SBGColorChange", 120 , 130);
        score = 10;
    }


    void Update()
    {
        Color color1;

        ColorUtility.TryParseHtmlString("00B7FF", out color1);
       

        ETVY = GameObject.FindGameObjectWithTag("ETVY");

        if (Formulas_Back[0].activeInHierarchy == true && Formulas_Front[0].activeInHierarchy == false)
        {
            dfW_sprite.color = color1;
        }
        if (Formulas_Front[0].activeInHierarchy == true && Formulas_Back[0].activeInHierarchy == false)
        {
            dfW_sprite.color = Color.black;
        }


        if (modularWindow.activeInHierarchy == true)
        {
            isModeularWindowActive = true;
        }

        if (modularWindow.activeInHierarchy == false)
        {

            isModeularWindowActive = false;
        }

        if (DFW.activeInHierarchy == true)
        {
            isDFWActive = true;
        }

        if (DFW.activeInHierarchy == false)
        {

            isDFWActive = false;
        }


        playerScore.text = "Score: " + score;

       // timer += Time.deltaTime;

        //if (timer >= interval)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int randomValue = Random.Range(0, 2);
        //        if (randomValue == 0)
        //        {
        //            BG1.SetActive(false);
        //        }
        //        else BG1.SetActive(true);
        //    }
        //    timer = 0;
        //}

        if(score <= 0)
        {
            score = 0;
            GameOverText.SetActive(true);
            resetButton.SetActive(true);
          // Time.timeScale = 0;
           
        }

        if (score >= 2000)
        {
            score = 2000;
            GameWonText.SetActive(true);
            resetButton.SetActive(true);
            //Time.timeScale = 0;

        }

    

    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextMW1()
    {
        mw[0].SetActive(false);
        mw[1].SetActive(true);
    }

    public void NextMW2()
    {
        mw[1].SetActive(false);
        mw[2].SetActive(true);
    }

    public void BackMW2()
    {
        mw[1].SetActive(false);
        mw[0].SetActive(true);
    }

    public void PlayMW3()
    {
        mw[2].SetActive(false);
        modularWindow.SetActive(false);
    }

    public void Randomization()
    {
             //5.79f

        int i = Random.Range(0, 6);
            currentPos = new Vector3(Random.Range(11, 29), Random.Range(10, -10), 0);
            aimPoint = Instantiate(targets[i], currentPos, Quaternion.identity);
         
    }

    public void PoisonSpell()
    {



        int i = Random.Range(0, 6);
        //float k = Random.Range(-2, 11);
        //float j = Random.Range(0, 8.5f);

        //if(k == j)
        //{
        //    k = 10;
        //}

        currentPos1 = new Vector3(Random.Range(11, 29), 10, 0);
        aimPoint1 = Instantiate(poisonSpells[i], currentPos1, Quaternion.identity);

    }

    public void SBGColorChange()
    {
        StartCoroutine(BGColorChange());
    }

    public void CloseButton()
    {
        if (DFW.activeInHierarchy == true)
        {
            DFW.SetActive(false);
       
        }

        if (swapButtons[0].activeInHierarchy == true)
        {
            swapButtons[0].SetActive(false);
        }
        if (swapButtons[1].activeInHierarchy == true)
        {
            swapButtons[1].SetActive(false);
        }

        
        for (int i = 0; i < Formulas_Front.Length; i++)
        {
            if (Formulas_Front[i].activeInHierarchy == true)
            {
                Formulas_Front[i].SetActive(false);

            }

        }

        for (int i = 0; i < Formulas_Back.Length; i++)
        {
            if (Formulas_Back[i].activeInHierarchy == true)
            {
                Formulas_Back[i].SetActive(false);

            }

        }

        //if (ETVX.activeInHierarchy == true)
        //{
        //    ETVX.SetActive(false);
        //}
        //if (ETVY.activeInHierarchy == true)
        //{
        //    ETVY.SetActive(false);
        //}

    }



    IEnumerator BGColorChange()
    {
        BG1.SetActive(true);

        yield return new WaitForSeconds(120f);

        BG1.SetActive(false);
    }
   
}
