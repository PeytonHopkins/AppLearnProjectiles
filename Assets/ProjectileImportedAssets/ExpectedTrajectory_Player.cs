using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ExpectedTrajectory_Player : MonoBehaviour
{

    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] SpriteRenderer cannon;
    [SerializeField] GameObject target;

    Vector3 currentPos;
    GameObject aimPoint;

    [SerializeField] GameObject shootPointGO;

    float speeed;
    public static bool isET;

    public static float angleProjectile;
    public Transform bombPos;

    [SerializeField] TextMeshProUGUI angleProjectileText;
    [SerializeField] TextMeshProUGUI forceProjectileText;

    [SerializeField] GameObject aPT;
    [SerializeField] GameObject fPT;

    #region DistanceTransformi_Button

    Vector3 transformiButton;

   

    float transformX;
    float transformY;

    [SerializeField] TextMeshProUGUI transformXTextiButton;
    [SerializeField] TextMeshProUGUI transformYTextiButton;

    #endregion

    void Start()
    {

        currentPos = new Vector3(Random.Range(11, 29), Random.Range(1, -17), 0);
        aimPoint = Instantiate(target, currentPos, Quaternion.identity);
        target = GameObject.FindGameObjectWithTag("Target");

        InvokeRepeating("Randomization", 5f, 15f);

 
    }

   
    void Update()
    {
        angleProjectileText.text = "ExpectedAngle:" + angleProjectile.ToString("F0") + " deg";
        forceProjectileText.text = "ExpectedForce:" + speeed.ToString("F0") + " m/s";

    }

    public void ExpectedTrajectory()
    {
        StartCoroutine(BoolFuncET());

        speeed = Random.RandomRange(10, 30);

        GameObject bombIntantiate = Instantiate(bombPrefab, shootPoint.position, shootPoint.rotation);
        bombIntantiate.GetComponent<Projectile>().Initialize(speeed);
        
        Vector3 direction = target.transform.position - shootPoint.position;
        angleProjectile = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //angleProjectile = Mathf.Abs(angleProjectile);
        Debug.Log(angleProjectile);

        cannon.transform.rotation = Quaternion.Euler(0f, 0f, angleProjectile);
    }

    IEnumerator BoolFuncET()
    {
        isET = true;
        aPT.SetActive(true);
        fPT.SetActive(true);
        yield return new WaitForSeconds(5f);
        aPT.SetActive(false);
        fPT.SetActive(false);
        isET = false;
    }

    void Randomization()
    {
        currentPos = new Vector3(Random.Range(11, 29), Random.Range(1, -17), 0);
        aimPoint.transform.position = currentPos;

       

        transformiButton = target.transform.position - shootPoint.position;
        transformX = transformiButton.x;
        transformY = transformiButton.y;

        //Debug.Log("transformX: " + transformX+ ", " + "transformY: "+ transformY);

        transformXTextiButton.text = "XDistance:" + transformX.ToString("F0");
        transformYTextiButton.text = "YDistance:" + transformY.ToString("F0");

    }

}
