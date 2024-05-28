#region libfuns
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Photon.Pun;
#endregion

public class MainPlayer_S2 : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region Variables

    [Header("Points")]
    Vector3 startPoint;
    Vector3 currentPoint;

    Vector3 _startPos;
    Vector3 _endPos;

    [Header("AngleVleocityTMPRO")]
    [SerializeField] TextMeshProUGUI anglePlayer;
    [SerializeField] TextMeshProUGUI velocityPlayer;


    [Header("AnglesVarForAngleCalc")]
    float angle;
    float force;
    float angleNew;
    float degrees;

    [Header("CannonRef")]
    [SerializeField] GameObject cannon;

    [Header("VelocityVarForForceCalc")]
    float _velocity;
    float velx;
    float vely;
    float velxSq;
    float velySQ;
    float initialVelocity;
    Vector3 velocity;
    float x_Velocity;
    float y_Velocity;

    private float dragStartTime = 0f;
    private float dragDurationThreshold = 3f;

    [Header("PrefabRef")]
    [SerializeField] GameObject bombPrefabPlayer;
    [SerializeField] GameObject prebombPrefabPlayer;
    [SerializeField] Transform shootPoint;


    #region DynamicFormulaWindow Variables
    
    float Vx;
    float Vy;
    float t;
    float tSquare;
    float g = 9.81f;
    float Dx;
    float Dintial;
    float Dfinal = 0;

    [SerializeField] Rigidbody2D rb;

    //float ExTr_Shoot.preciseTimeInAir = 2.92f;
    float time;
    float timeSquare;

    float distanceX;
    double heightY;

    double heightFromPTOTarget;

    double height;
    float forceSquare;
    float angleSquare;


    #endregion

    #region DynamicFormulaWindow TMPRO's

    //[Header("FormulaWindowFront")]
    //[SerializeField] TextMeshProUGUI HV;
    //[SerializeField] TextMeshProUGUI VV;
    //[SerializeField] TextMeshProUGUI FlightTime;
    //[SerializeField] TextMeshProUGUI FHD;
    //[SerializeField] TextMeshProUGUI IVD;

    [Header("FormulaWindowFront")]
    [SerializeField] TextMeshProUGUI distanceXText;
    [SerializeField] TextMeshProUGUI heightXText;
    [SerializeField] TextMeshProUGUI timeText;

    bool isDragged;

    static public bool isDRAG;
    float ETAngle = 45;

    [SerializeField] PhotonView view;

    #endregion


    #endregion

    #region PointerFns

    public void OnPointerDown(PointerEventData eventData)
    {
        force = 0;
        angle = 0;
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPoint.z = 15;
        //Debug.Log("OnPointerDown");

        StartCoroutine(ISDRAGBool());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log("BeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPoint.z = 15;
        AngleCalc();
        ForceCalc();
        PreShoot();
        //Debug.Log("OnDrag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Shoot();
        //Debug.Log("OnEndDrag");
    }

    #endregion

    #region UpdateFunc

    void Update()
    {
        //if (view.IsMine)
        //{

            #region ETDependices

            if (ExTr_Shoot.isETtrue == true)
            {
                velocityPlayer.text = "Velocity: " + ExTr_Shoot.preciseVelocityMag.ToString("F0") + " m/s";
                anglePlayer.text = "Angle: " + ETAngle.ToString("F0") + "deg";
            }

            if (ExTr_Shoot.isETtrue == false)
            {
                velocityPlayer.text = "Velocity: " + initialVelocity.ToString("F0") + " m/s";
                anglePlayer.text = "Angle: " + degrees.ToString("F0") + " deg";
            }

            #endregion


            #region method2
            //if (Input.GetMouseButtonDown(0))
            //{
            //    OnPointerDown();
            //}

            //if (Input.GetMouseButton(0))
            //{
            //    OnDrag();
            //    isDragged = true;
            //}

            //if (Input.GetMouseButtonUp(0))
            //{
            //    OnEndDrag();
            //}

            #endregion

            AngleAdjustment(angle);
            IntialVelocityCalc();
            DynamicFormulaCalculations();

            //anglePlayer.text = "Angle: " + degrees.ToString("F0") + " deg";
            //velocityPlayer.text = "Velocity: " + initialVelocity.ToString("F0") + " m/s"; 
        //}

    }

    #region method2Func
    //private void OnPointerDown()
    //{
    //    force = 0;
    //    angle = 0;
    //    startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    startPoint.z = 15;
    //    //Debug.Log("OnPointerDown");
    //}

    //private void OnDrag()
    //{
    //    _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    currentPoint.z = 15;
    //    AngleCalc();
    //    ForceCalc();
    //    PreShoot();
    //    //Debug.Log("OnDrag");

    //}

    //private void OnEndDrag()
    //{
    //    //if(isDragged && Time.time - dragStartTime > dragDurationThreshold)
    //    //{
    //        Shoot();
    //    //}


    //    //Debug.Log("OnEndDrag");
    //}
    #endregion

 
        #endregion

    #region AngleCalculation
        void AngleCalc()
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 dir = currentPoint - startPoint;
        angle = Mathf.Atan2(dir.y, dir.x);

        if (angle > 0)
        {
            angle -= Mathf.PI;
        }
        else if (angle < 0)
        {
            angle += Mathf.PI;
        }

        angleNew = angle * MathF.PI / 180;

    }

    void AngleAdjustment(float value)
    {
        degrees = value * Mathf.Rad2Deg;
        cannon.transform.rotation = Quaternion.Euler(0f, 0f, degrees);

    }

    #endregion

    #region Force
    void ForceCalc()
    {
        force = (_endPos - _startPos).magnitude;
        x_Velocity = force * Mathf.Cos(angle);
        y_Velocity = force * Mathf.Sin(angle);

    }

    #endregion


    #region Shoot

    void Shoot()
    {

        GameObject bombIntantiate = Instantiate(bombPrefabPlayer, shootPoint.position, shootPoint.rotation);

        float velx = initialVelocity * Mathf.Cos(angle);
        float vely = initialVelocity * Mathf.Sin(angle);

        Vector3 velocity = new Vector3(velx, vely, 0f);

        bombIntantiate.GetComponent<Rigidbody2D>().velocity = velocity;

    }

    void PreShoot()
    {

        GameObject bombIntantiate1 = Instantiate(prebombPrefabPlayer, shootPoint.position, shootPoint.rotation);

        velocity.x = force * Mathf.Cos((angle));
        velocity.y = force * Mathf.Sin((angle));
        velocity.z = 0;
        bombIntantiate1.GetComponent<Rigidbody2D>().velocity = velocity;

    }

    #endregion

    #region FWCalculations

    void IntialVelocityCalc()
    {
        velx = Pre_ArrowPlayer.preVelocity.x;
        vely = Pre_ArrowPlayer.preVelocity.y;

        velxSq = velx * velx;
        velySQ = vely * vely;

        initialVelocity = Mathf.Sqrt(velxSq + velySQ);
    }
    #endregion

    #region DynamicFormulaWindow

    void DynamicFormulaCalculations()
    {
        #region Calculations
        //Horizontal Velocity
        Vx = force * MathF.Cos(angleNew);

        //Vertical Velocity
        Vy = force * Mathf.Sin(angleNew);

        //Flight Time
        t = Vy / g;

        //Final Horizontal Distance
        Dx = (2 * t) * Vx;

        //tSquare
        tSquare = t * t;

        velx = Pre_ArrowPlayer.preVelocity.x;
        vely = Pre_ArrowPlayer.preVelocity.y;

        velxSq = velx * velx;
        velySQ = vely * vely;

        initialVelocity = Mathf.Sqrt(velxSq + velySQ);

        //Intial Vertical Distance
        Dintial = Vy * t - 1 / 2 * (g * tSquare);
        float tt = (force * MathF.Sin(MathF.PI / 180 * degrees)) / 9.81f;
        //Velocity
        _velocity = (force / rb.mass) * tt;


        //time
        time = (initialVelocity * MathF.Sin(MathF.PI / 180 * degrees)) / 9.81f;

        //timeSquare
        timeSquare = ExTr_Shoot.preciseTimeInAir * ExTr_Shoot.preciseTimeInAir;



        //DistanceX
        distanceX = initialVelocity * Mathf.Cos(MathF.PI / 180 * 45) * ExTr_Shoot.preciseTimeInAir;

        //heightY
        //heightY   =  (initialVelocity * Mathf.Sin(MathF.PI / 180 * 45) * ETShoot.ExTr_Shoot.preciseTimeInAir - 0.5f * (9.81) * timeSquare) ;
        heightY = (initialVelocity * Mathf.Sin(MathF.PI / 180 * 45) * ExTr_Shoot.preciseTimeInAir - 0.5f * (9.81) * timeSquare) ;

        heightFromPTOTarget = heightY; // - 14.54f;

        forceSquare = force * force;
        angleSquare = degrees * degrees;

        height = forceSquare * Mathf.Sin(angleSquare) / (2 * -9.81);



        #endregion

        #region TMPRODisplay

        //HV.text = "<color=red>" + Vx.ToString("F2") + "</color> " + " = " + "<color=blue>" + force.ToString("F2") + "</color> " + " cos( " + "<color=blue>" + angle.ToString("F2") + "</color> " + " )";
        //VV.text = "<color=red>" + Vy.ToString("F2") + "</color> " + " = " + "<color=blue>" + force.ToString("F2") + "</color> " + " sin( " + "<color=blue>" + angle.ToString("F2") + "</color> " + " )";
        //FlightTime.text = "<color=red>" + t.ToString("F2") + "</color> " + " = " + "<color=blue>" + Vy.ToString("F2") + "</color> " + " / " + "<color=green>" + g.ToString("F2") + "</color> ";
        //FHD.text = "<color=red>" + Mathf.Abs(Dx).ToString("F2") + "</color> " + " = " + " 2 (" + "<color=blue>" + t + "</color> " + ")" + " " + "(" + "<color=blue>" + Vx.ToString("F2") + "</color> " + ")";
        //IVD.text = "<color=red>" + Mathf.Abs(Dintial).ToString("F2") + "</color> " + " = " + Dfinal.ToString() + " - " + "<color=blue>" + Vy.ToString("F2") + "</color> " + " * " + "<color=blue>" + t.ToString("F2") + "</color> "
        //    + " - " + " 1/2 " + "  (" + "<color=green>" + g.ToString("F2") + "</color> " + ")" + " (" + "<color=blue>" + t.ToString("F2") + "</color> " + ")" + "^2";

        #endregion

        #region MainDynamicWindow

        distanceXText.text = "<#F08EED>" + "<b>" + distanceX.ToString("F2") + "</b>" + "</color> " + " = " + "<color=white>" + initialVelocity.ToString("F2") + "</color> " + " * cos (" + "<color=white>" +
           degrees.ToString("F2") + "</color> " + ") * " + "<color=white>" + ExTr_Shoot.preciseTimeInAir.ToString("F2") + "</color> ";

        heightXText.text = "<#F08EED>" + heightFromPTOTarget.ToString("F2") + "</color> " + " = " + "<color=white>" + initialVelocity.ToString("F2") + "</color> " + " * sin (" + "<color=white>" +
          degrees.ToString("F2") + "</color> " + ") * " + "<color=white>" + ExTr_Shoot.preciseTimeInAir.ToString("F2") + "</color> " + "-" + " (1/2) * (9.81) * (" + "<color=white>" + ExTr_Shoot.preciseTimeInAir.ToString("F2") + "</color> " + ")^2 ";

        timeText.text = "<color=green>" + time.ToString("F2") + "</color> " + "= (" + "<color=white>" + initialVelocity.ToString("F2") + "</color> " + "*" + "sin( " + "<color=white>" +
           degrees.ToString("F2") + "</color> " + ") ) / 9.81";

        #endregion

    }

    #endregion

    IEnumerator ISDRAGBool()
    {
        isDRAG = true;
        yield return new WaitForSeconds(0.1f);
        isDRAG = false;
    }


}