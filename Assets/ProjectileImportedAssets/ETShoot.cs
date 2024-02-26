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

public class ETShoot : MonoBehaviour
{
    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject cannon;
    //[SerializeField] Transform botPlat;
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
   // [SerializeField] TextMeshProUGUI ExpectedTrajectetoryVY;

    [SerializeField] LineRenderer line;

    GameObject pos1;
    GameObject pos2;
    GameObject pos3;

  //  GameObject refETVY;

    GameObject target;

  //  [SerializeField] GameObject ETVX;
  //  [SerializeField] GameObject ETVY;

    static public float preciseTimeInAir;
    static public float preciseVelocityMag;

    static public float preciseTimeInAir1;
    static public float preciseVelocityMag1;

    static public float etVY_Value;

    static public bool isETValuesActive;
    static public bool isETVYValuesActive;

    //[SerializeField] GameObject etVX;
    //[SerializeField] GameObject etVY;

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

      

        pos1 = GameObject.FindGameObjectWithTag("Position1");
        pos2 = GameObject.FindGameObjectWithTag("Position2");
        pos3 = GameObject.FindGameObjectWithTag("Position3");
        target = GameObject.FindGameObjectWithTag("Target");

       // refETVY = GameObject.FindGameObjectWithTag("ETVY");

        spawnrotation = new Quaternion();
        //aimPoint = Instantiate(indicatorPoint, currentPos, spawnrotation);
        //currentPos = new Vector3(-100, aimPoint.transform.position.y, transform.position.z);
        line.positionCount = 3;

        ExpectedTrajectory2();

        Debug.Log(time_ET_val + velocity_ET_val);
    }

    private void Update()
    {

        //ETVY1 = GameObject.FindGameObjectWithTag("ETVY");


        if (Game_Manger_1.isDFWActive == true)
        {
            ExpectedTrajectory1();
            line.enabled = true;
            line.SetPosition(0, shootPoint.transform.position);
            line.SetPosition(1, pos1.transform.position);
            line.SetPosition(2, pos3.transform.position);
            //ETVX.SetActive(true);
            //ETVY1.SetActive(true);


        }


        if (Game_Manger_1.isDFWActive == false)
        {

            line.enabled = false;
            /*
            if(ETVX.activeInHierarchy == true)
            {
                ETVX.SetActive(false);
            }

            if (ETVX.activeInHierarchy == true)
            {
                //ETVY1.SetActive(false);
            }
            */


        }

        if (isETVYValuesActive == true)
        {
            //ETVX.SetActive(true);
            //ETVY1.SetActive(false);
        }
        if (isETVYValuesActive == false && Game_Manger_1.isDFWActive == false)
        {
            //ETVX.SetActive(false);
            //ETVY1.SetActive(false);
        }

    }

    // Update is called once per frame
    public void ExpectedTrajectory()
    {
       

        StartCoroutine(isETVYValuesActiveMethod());

        StartCoroutine(ET());

        StartCoroutine(BGBLUR());

        pos1 = GameObject.FindGameObjectWithTag("Position1");
        pos2 = GameObject.FindGameObjectWithTag("Position2");
        pos3 = GameObject.FindGameObjectWithTag("Position3");

     

        target = GameObject.FindGameObjectWithTag("Target");

      

   
        rand_x_pos = target.transform.position.x;
        currentPos.x = rand_x_pos;
        aimPoint = target;
        aimPoint.transform.position = target.transform.position;

        //Find the precise shot
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

        ExpectedTrajectetoryVX.text =   xDistance.ToString("F2");
    
        AngleAdjustmentFront(45);

    

        StartCoroutine(lineRenderer());




     
        time_ET_val = Mathf.Sqrt(2 * (xDistance - yDistance) * 1 / 9.81f);
       
        velocity_ET_val = xDistance / (Mathf.Cos(Mathf.PI / 4) * time_ET_val);


    }

    public void ExpectedTrajectory2()
    {
        //pos1 = GameObject.FindGameObjectWithTag("Position1");
        //pos2 = GameObject.FindGameObjectWithTag("Position2");
        //pos3 = GameObject.FindGameObjectWithTag("Position3");

        target = GameObject.FindGameObjectWithTag("Target");

       // refETVY = GameObject.FindGameObjectWithTag("ETVY");

        //pauseShotDuration -= Time.deltaTime;
        //if (pauseShotDuration <= 0)
        //{
        //    if (projectile)
        //    {
        //        Destroy(projectile);
        //    }
        //    pauseShotDuration = 10;
        rand_x_pos = target.transform.position.x;
        currentPos.x = rand_x_pos;
        aimPoint = target;
        aimPoint.transform.position = target.transform.position;

        //Find the precise shot
        float xDistance = Mathf.Abs(rand_x_pos - shootPoint.position.x);
        float yDistance = (aimPoint.transform.position.y) - shootPoint.position.y;

        etVY_Value = yDistance;

        preciseTimeInAir = Mathf.Sqrt((2 * (yDistance - xDistance * Mathf.Tan(optimalAngle))) / -9.81f);

        preciseVelocityMag = xDistance / (preciseTimeInAir * Mathf.Cos(optimalAngle));

        float angleVal = UnityEngine.Random.Range(minAngleRange, maxAngleRange);
        float velx = preciseVelocityMag * Mathf.Cos(MathF.PI / 4);
        float vely = preciseVelocityMag * Mathf.Sin(MathF.PI / 4);
        //print("IntialVelocityET:" + preciseVelocityMag);
        //print("ET_dy:" + vely);

        //Vector3 velocity = new Vector3(velx, vely, 0f);
        //projectile = Instantiate(bombPrefab, shootPoint.position, spawnrotation);
        //projectile.GetComponent<Rigidbody2D>().velocity = velocity;

        //ExpectedTrajectetoryVX.text = "ETX:" + xDistance.ToString("F0");
        //ExpectedTrajectetoryVY.text = "ETY:" + yDistance.ToString("F0");

        //StartCoroutine(BoolFuncET());
        //int randomValue = Random.Range(40, 70);
        AngleAdjustmentFront(45);

        //StartCoroutine(lineRenderer());

        //ETVY.transform.position = new Vector3(refETVY.transform.position.x, transform.position.y, transform.position.z);
        //ETVX.transform.position = pos3.transform.position;

       // subractionOfRangeandHeight = 2 * (xDistance - yDistance) * Mathf.Tan(45);
        time_ET_val = Mathf.Sqrt(2 * (xDistance - yDistance) *1 / 9.81f);
       // DivValue = Mathf.Cos(45) * time_ET_val;
        velocity_ET_val = xDistance / (Mathf.Cos(Mathf.PI/4) * time_ET_val);

        timeET.text = "<#F08EED>" + "<b>" + time_ET_val.ToString("F2") + "</b>" + "</color> " + " =  √( 2* ( " + "<color=white>" + xDistance.ToString("F2") + "</color> " + " - " + "<color=white>" +
           yDistance.ToString("F2") + "</color> " + " ) * " + "<color=white>" + MathF.Tan(MathF.PI / 4).ToString("F2") + "</color> " + " / " + " 9.81 )";

        velocityET.text = "<#F08EED>" + "<b>" + velocity_ET_val.ToString("F2") + "</b>" + "</color> " + " = " + "<color=white>" + xDistance.ToString("F2") + "</color> " + " / ( " + "<color=white>" +
  MathF.Cos(MathF.PI / 4).ToString("F2") + "</color> " + " * " + "<color=white>" + time_ET_val.ToString("F2") + "</color> " + " )";

    }

    public void ExpectedTrajectory1()
    {
        pos1 = GameObject.FindGameObjectWithTag("Position1");
        pos2 = GameObject.FindGameObjectWithTag("Position2");
        pos3 = GameObject.FindGameObjectWithTag("Position3");

        target = GameObject.FindGameObjectWithTag("Target");
        
        rand_x_pos = target.transform.position.x;
        currentPos.x = rand_x_pos;
        aimPoint = target;
        aimPoint.transform.position = target.transform.position;

        //Find the precise shot
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
        //ExpectedTrajectetoryVY.text = yDistance.ToString("F2");

        // AngleAdjustmentFront(45);
        // subractionOfRangeandHeight = 2 * (xDistance - yDistance) * Mathf.Tan(45);
        time_ET_val = Mathf.Sqrt(2 * (xDistance - yDistance) * 1 / 9.81f);
        // DivValue = Mathf.Cos(45) * time_ET_val;
        velocity_ET_val = xDistance / (Mathf.Cos(Mathf.PI / 4) * time_ET_val);

        timeET.text = "<#F08EED>" + "<b>" + time_ET_val.ToString("F2") + "</b>" + "</color> " + " =  √ 2* ( " + "<color=white>" + xDistance.ToString("F2") + "</color> " + " - " + "<color=white>" +
           yDistance.ToString("F2") + "</color> " + " ) * " + "<color=white>" + MathF.Tan(MathF.PI / 4).ToString("F2") + "</color> " + " / " + " 9.81 ";

        velocityET.text = "<#F08EED>" + "<b>" + velocity_ET_val.ToString("F2") + "</b>" + "</color> " + " = " + "<color=white>" + xDistance.ToString("F2") + "</color> " + " / ( " + "<color=white>" +
  MathF.Cos(MathF.PI / 4).ToString("F2") + "</color> " + " * " + "<color=white>" + time_ET_val.ToString("F2") + "</color> " + " )";

    }

 
    IEnumerator lineRenderer()
    {
        line.enabled = true;
        line.SetPosition(0, cannon.transform.position);
        line.SetPosition(1, pos1.transform.position);
        line.SetPosition(2, target.transform.position);
     
     
        
        yield return new WaitForSeconds(5f);
    
            line.enabled = false;
          
    
    }

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
        bgBlur.enabled= true;
       
        
        yield return new WaitForSeconds(5f);

        bgBlur.enabled = false;



    }


    void AngleAdjustmentFront(float value)
    {
        //_angle = value;
        //cannon.transform.rotation = Quaternion.AngleAxis(angleFront, Vector3.forward);
        
        cannon.transform.rotation = Quaternion.Euler(0f, 0f, value);
    }
}


