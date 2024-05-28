#region libClass

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using static System.Net.WebRequestMethods;
using Unity.VisualScripting;
using static Unity.VisualScripting.Member;
using UnityEngine.UIElements;

#endregion

public class ExTr_Shoot : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject cannon;
  
    [SerializeField] GameObject indicatorPoint;
    float pauseShotDuration = 10;
    //38 degrees
    float minAngleRange = (19f * Mathf.PI) / 90f;
    //52 degrees
    float maxAngleRange = (13f * Mathf.PI) / 45f;
    //45 degrees gives x and y velocities the same value
    float optimalAngle = Mathf.PI / 4;
    float rand_x_pos;
    Vector3 currentPos;
    GameObject aimPoint;
    GameObject projectile;
    Quaternion spawnrotation;

    [SerializeField] TextMeshProUGUI ExpectedTrajectetoryVX;
    [SerializeField] TextMeshProUGUI ExpectedTrajectetoryVY;

    // [SerializeField] LineRenderer line;

    //GameObject pos1;
    //GameObject pos2;
    //GameObject pos3;

    GameObject target;

    [SerializeField] GameObject ETVX;


    static public float preciseTimeInAir;
    static public float preciseVelocityMag;

    static public float preciseTimeInAir1;
    static public float preciseVelocityMag1;

    static public float etVY_Value;

    static public bool isETValuesActive;
    static public bool isETVYValuesActive;


    public float time_ET_val;
    public float subractionOfRangeandHeight;
    public double DivValue;
    public float velocity_ET_val;


    [SerializeField] TextMeshProUGUI timeET;
    [SerializeField] TextMeshProUGUI velocityET;

    [SerializeField] TextMeshProUGUI velocityText;
    [SerializeField] TextMeshProUGUI angleText;

    [SerializeField] TextMeshProUGUI anglePlayer;
    [SerializeField] TextMeshProUGUI forcePlayer;

    [SerializeField] GameObject anglePlayer2;
    [SerializeField] GameObject forcePlayer2;
    [SerializeField] GameObject anglePlayer3;
    [SerializeField] GameObject forcePlayer3;

    GameObject ETVY1;

    float angle = 45;

    static public bool isETtrue;

    [SerializeField] SpriteRenderer bgBlur;

    void Start()

    {
        //pos1 = GameObject.FindGameObjectWithTag("Position1");
        //pos2 = GameObject.FindGameObjectWithTag("Position2");
        //pos3 = GameObject.FindGameObjectWithTag("Position3");
        target = GameObject.FindGameObjectWithTag("Target");

        spawnrotation = new Quaternion();
 
       // line.positionCount = 3;

        ExpectedTrajectory2();

        ExpectedTrajectetoryVX.gameObject.SetActive(true); 

        Debug.Log(time_ET_val + velocity_ET_val);
    }

    private void Update()
    {

        ETVY1 = GameObject.FindGameObjectWithTag("ETVY");

        if (MainPlayer_S2.isDRAG == true)
        {
            ExpectedTrajectory2();
        }

        if (GM.isDFWActive == true)
        {
            ExpectedTrajectory1();
            //line.enabled = true;
            //line.SetPosition(0, shootPoint.transform.position);
            //line.SetPosition(1, pos1.transform.position);
            //line.SetPosition(2, pos3.transform.position);
            ETVX.SetActive(true);
            ETVY1.SetActive(true);


        }


        //if (GM.isDFWActive == false)
        //{

        //    //line.enabled = false;
        //    if (ETVX.activeInHierarchy == true)
        //    {
        //        ETVX.SetActive(false);
        //    }

        //    if (ETVX.activeInHierarchy == true)
        //    {
        //        ETVY1.SetActive(false);
        //    }



        //}

        //if (isETVYValuesActive == true)
        //{
        //    ETVX.SetActive(true);
            
        //}
        //if (isETVYValuesActive == false && GM.isDFWActive == false)
        //{
        //    ETVX.SetActive(false);
           
        //}

    }

    public void ExpectedTrajectory()
    {
        StartCoroutine(isETVYValuesActiveMethod());

        StartCoroutine(ET());

        StartCoroutine(BGBLUR());

        //pos1 = GameObject.FindGameObjectWithTag("Position1");
        //pos2 = GameObject.FindGameObjectWithTag("Position2");
        //pos3 = GameObject.FindGameObjectWithTag("Position3");

    
        target = GameObject.FindGameObjectWithTag("Target");

        rand_x_pos = target.transform.position.x;
        currentPos.x = rand_x_pos;
        aimPoint = target;
        aimPoint.transform.position = target.transform.position;
        
        float xDistance = Mathf.Abs(rand_x_pos - shootPoint.position.x);
        float yDistance = (aimPoint.transform.position.y) - shootPoint.position.y;

        etVY_Value = yDistance;

        preciseTimeInAir = Mathf.Sqrt((2 * (yDistance - xDistance * Mathf.Tan(optimalAngle))) / -9.81f);

        preciseVelocityMag = xDistance / (preciseTimeInAir * Mathf.Cos(optimalAngle));

        float angleVal = UnityEngine.Random.Range(minAngleRange, maxAngleRange);
        float velx = preciseVelocityMag * Mathf.Cos(MathF.PI / 4);
        float vely = preciseVelocityMag * Mathf.Sin(MathF.PI / 4);
        print("ET_Velocity:" + preciseVelocityMag);
        print("ET_Time:" + preciseTimeInAir);

        Vector3 velocity = new Vector3(velx, vely, 0f);
        projectile = Instantiate(bombPrefab, shootPoint.position, spawnrotation);
        projectile.GetComponent<Rigidbody2D>().velocity = velocity;

        ExpectedTrajectetoryVX.text = "Range: " + xDistance.ToString("F2");
        ExpectedTrajectetoryVY.text = "Height:" + yDistance.ToString("F2");

        AngleAdjustmentFront(45);
        //StartCoroutine(lineRenderer());

        time_ET_val = Mathf.Sqrt(2 * (xDistance - yDistance) * 1 / 9.81f);

        velocity_ET_val = xDistance / (Mathf.Cos(Mathf.PI / 4) * time_ET_val);
    }

    public void ExpectedTrajectory2()
    {

        target = GameObject.FindGameObjectWithTag("Target");

        rand_x_pos = target.transform.position.x;
        currentPos.x = rand_x_pos;
        aimPoint = target;
        aimPoint.transform.position = target.transform.position;

        float xDistance = Mathf.Abs(rand_x_pos - shootPoint.position.x);
        float yDistance = (aimPoint.transform.position.y) - shootPoint.position.y;

        etVY_Value = yDistance;

        preciseTimeInAir = Mathf.Sqrt((2 * (yDistance - xDistance * Mathf.Tan(optimalAngle))) / -9.81f);

        preciseVelocityMag = xDistance / (preciseTimeInAir * Mathf.Cos(optimalAngle));

        float angleVal = UnityEngine.Random.Range(minAngleRange, maxAngleRange);
        float velx = preciseVelocityMag * Mathf.Cos(MathF.PI / 4);
        float vely = preciseVelocityMag * Mathf.Sin(MathF.PI / 4);
       
        AngleAdjustmentFront(45);

        time_ET_val = Mathf.Sqrt(2 * (xDistance - yDistance) * 1 / 9.81f);
       
        velocity_ET_val = xDistance / (Mathf.Cos(Mathf.PI / 4) * time_ET_val);

        timeET.text = "<#F08EED>" + "<b>" + time_ET_val.ToString("F2") + "</b>" + "</color> " + " =  √( 2* ( " + "<color=white>" + xDistance.ToString("F2") + "</color> " + " - " + "<color=white>" +
           yDistance.ToString("F2") + "</color> " + " ) * " + "<color=white>" + MathF.Tan(MathF.PI / 4).ToString("F2") + "</color> " + " / " + " 9.81 )";

        velocityET.text = "<#F08EED>" + "<b>" + velocity_ET_val.ToString("F2") + "</b>" + "</color> " + " = " + "<color=white>" + xDistance.ToString("F2") + "</color> " + " / ( " + "<color=white>" +
  MathF.Cos(MathF.PI / 4).ToString("F2") + "</color> " + " * " + "<color=white>" + time_ET_val.ToString("F2") + "</color> " + " )";

    }

    public void ExpectedTrajectory1()
    {
        //pos1 = GameObject.FindGameObjectWithTag("Position1");
        //pos2 = GameObject.FindGameObjectWithTag("Position2");
        //pos3 = GameObject.FindGameObjectWithTag("Position3");

        target = GameObject.FindGameObjectWithTag("Target");

        rand_x_pos = target.transform.position.x;
        currentPos.x = rand_x_pos;
        aimPoint = target;
        aimPoint.transform.position = target.transform.position;

        float xDistance = Mathf.Abs(rand_x_pos - shootPoint.position.x);
        float yDistance = (aimPoint.transform.position.y) - shootPoint.position.y;

        etVY_Value = yDistance;

        preciseTimeInAir1 = Mathf.Sqrt((2 * (yDistance - xDistance * Mathf.Tan(optimalAngle))) / -9.8f);

        preciseVelocityMag1 = xDistance / (preciseTimeInAir * Mathf.Cos(optimalAngle));

        float angleVal = UnityEngine.Random.Range(minAngleRange, maxAngleRange);
        float velx = preciseVelocityMag1 * Mathf.Cos(MathF.PI / 4);
        float vely = preciseVelocityMag1 * Mathf.Sin(MathF.PI / 4);

        Vector3 velocity = new Vector3(velx, vely, 0f);


        ExpectedTrajectetoryVX.text = xDistance.ToString("F2");

        time_ET_val = Mathf.Sqrt(2 * (xDistance - yDistance) * 1 / 9.81f);
     
        velocity_ET_val = xDistance / (Mathf.Cos(Mathf.PI / 4) * time_ET_val);

        timeET.text = "<#F08EED>" + "<b>" + time_ET_val.ToString("F2") + "</b>" + "</color> " + " =  √ 2* ( " + "<color=white>" + xDistance.ToString("F2") + "</color> " + " - " + "<color=white>" +
           yDistance.ToString("F2") + "</color> " + " ) * " + "<color=white>" + MathF.Tan(MathF.PI / 4).ToString("F2") + "</color> " + " / " + " 9.81 ";

        velocityET.text = "<#F08EED>" + "<b>" + velocity_ET_val.ToString("F2") + "</b>" + "</color> " + " = " + "<color=white>" + xDistance.ToString("F2") + "</color> " + " / ( " + "<color=white>" +
        MathF.Cos(MathF.PI / 4).ToString("F2") + "</color> " + " * " + "<color=white>" + time_ET_val.ToString("F2") + "</color> " + " )";

    }


    //IEnumerator lineRenderer()
    //{
        //line.enabled = true;
        //line.SetPosition(0, cannon.transform.position);
        //line.SetPosition(1, pos1.transform.position);
        //line.SetPosition(2, target.transform.position);
        //yield return new WaitForSeconds(5f);
        //line.enabled = false;
    //}

    IEnumerator isETVYValuesActiveMethod()
    {
        isETVYValuesActive = true;
        yield return new WaitForSeconds(5f);
        isETVYValuesActive = false;
    }

    IEnumerator ET()
    {
        isETtrue = true;
        yield return new WaitForSeconds(6f);
        isETtrue = false;
    }
    IEnumerator BGBLUR()
    {
        bgBlur.enabled = true;
        yield return new WaitForSeconds(5f);
        bgBlur.enabled = false;
    }


    void AngleAdjustmentFront(float value)
    {
        cannon.transform.rotation = Quaternion.Euler(0f, 0f, value);
    }
}
