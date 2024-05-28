using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    [SerializeField] GameObject DFW;
    [SerializeField] Image dfW_sprite;
 
    [SerializeField] GameObject[] swapButtons;
    [SerializeField] GameObject[] Formulas_Front;
    [SerializeField] GameObject[] Formulas_Back;

    static public bool isDFWActive;

    [SerializeField] GameObject dynamicFormulaMainWindow_gameobject;
    [SerializeField] GameObject _rotateButton1;
    [SerializeField] GameObject _rotateButton2;

    [SerializeField] GameObject _closeButton1;
    [SerializeField] GameObject _closeButton2;

    [SerializeField] Animator anim;
    [SerializeField] Animator anim1;

    void Update()
    {
        Color color1;

        ColorUtility.TryParseHtmlString("00B7FF", out color1);

        if (Formulas_Back[0].activeInHierarchy == true && Formulas_Front[0].activeInHierarchy == false)
        {
           
        }
        if (Formulas_Front[0].activeInHierarchy == true && Formulas_Back[0].activeInHierarchy == false)
        {
            
        }

       

        if (DFW.activeInHierarchy == true)
        {
            isDFWActive = true;
        }

        if (DFW.activeInHierarchy == false)
        {

            isDFWActive = false;
        }


    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    #region CloseButton
    public void CloseButton()
    {

        anim1.SetBool("ScrollClose", true);

        if (DFW.activeInHierarchy == true)
        {
            StartCoroutine(DFWDelay());

        }

        _closeButton1.SetActive(false);

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


       


    }

    #endregion


    #region MainIButton
    public void IButtonMDW()
    {
        if (dynamicFormulaMainWindow_gameobject.activeInHierarchy == false)
        {
            dynamicFormulaMainWindow_gameobject.SetActive(true);
        }

        for (int i = 0; i < Formulas_Back.Length; i++)
        {
            if (Formulas_Back[i].activeInHierarchy == true)
            {
                Formulas_Back[i].SetActive(false);
            }


        }

        StartCoroutine(FormulasFrontDelay());

        if (_rotateButton1.activeInHierarchy == false )
        {
            StartCoroutine(RotateButtonFront());
        }

        if (_closeButton1.activeInHierarchy == false)
        {
            StartCoroutine(CloseButtonFront());
        }

        if (_rotateButton2.activeInHierarchy == false && Formulas_Back[0].activeInHierarchy == true)
        {
            _rotateButton2.SetActive(true);
        }
    }
    #endregion

    #region RotateButton

    public void rotateButton1()
    {


        anim.SetTrigger("FW1");
        _rotateButton1.SetActive(false);
        StartCoroutine(ButtonDelay1());


    }

    public void rotateButton2()
    {

        anim.SetTrigger("FW2");
        _rotateButton2.SetActive(false);
        StartCoroutine(ButtonDelay2());

    }

    IEnumerator ButtonDelay1()
    {
        yield return new WaitForSeconds(1.2f);
        _rotateButton2.SetActive(true);
       
        for (int i = 0; i < Formulas_Front.Length; i++)
        {
            Formulas_Front[i].SetActive(false);
            Formulas_Back[i].SetActive(true);
        }
    }

    IEnumerator ButtonDelay2()
    {
        yield return new WaitForSeconds(1.2f);
        _rotateButton1.SetActive(true);
        for (int i = 0; i < Formulas_Front.Length; i++)
        {
            Formulas_Front[i].SetActive(true);
            Formulas_Back[i].SetActive(false);
        }

    }

    IEnumerator FormulasFrontDelay()
    {
        yield return new WaitForSeconds(5.5f);

        for (int i = 0; i < Formulas_Front.Length; i++)
        {
            if (Formulas_Front[i].activeInHierarchy == false)
            {
                Formulas_Front[i].SetActive(true);
            }


        }
    }

    IEnumerator RotateButtonFront()
    {
        yield return new WaitForSeconds(5.5f);
        _rotateButton1.SetActive(true);

    }

    IEnumerator CloseButtonFront()
    {
        yield return new WaitForSeconds(5.5f);
        _closeButton1.SetActive(true);
    }

    IEnumerator DFWDelay()
    {
        yield return new WaitForSeconds(6.5f);
        DFW.SetActive(false);
    }

    #endregion

}
