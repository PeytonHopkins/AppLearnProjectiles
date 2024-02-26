#region libfuns
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;
using System.Net;
using System.Drawing;
#endregion

public class Main_Player_Input_ : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region Variables

    #region MainVar
    [SerializeField] float minPower;
    [SerializeField] float maxPower;
    float force;
    float distance;



    float _angle;
    float angleFront;
    float angleBack;
    float angle;
    public static float angle__;
    float degrees;
    float V;
    float x2;
    float x1;
    float xAngle;
    float x_Velocity;
    float y_Velocity;
    Vector3 velocity;
    Vector3 _startPos;
    Vector3 _endPos;

    [SerializeField] SpriteRenderer cannon;
    [SerializeField] SpriteRenderer _cannon;
    // [SerializeField] Sprite[] cannons;

    [SerializeField] GameObject hand2;




    [SerializeField] TextMeshProUGUI anglePlayer;
    [SerializeField] TextMeshProUGUI forcePlayer;
    [SerializeField] TextMeshProUGUI forcePlayer1;

    [SerializeField] GameObject anglePlayer2;
    [SerializeField] GameObject forcePlayer2;
    [SerializeField] GameObject anglePlayer3;
    [SerializeField] GameObject forcePlayer3;

    [SerializeField] GameObject bombPrefabPlayer;
    [SerializeField] GameObject prebombPrefabPlayer;
    [SerializeField] Transform shootPoint;

    bool isAngleFront;
    bool isAngleBack;

    Vector3 startPoint;
    Vector3 currentPoint;

    #endregion

    #region Variables_Dynamic_Formula_Window

    #region Var
    float Vx;
    float Vy;
    float t;
    float tSquare;
    float g = 9.81f;
    float Dx;
    float Dintial;
    float Dfinal = 0;

    float angleNew;

    [SerializeField] GameObject dynamicFormulaWindow_gameobject;
    [SerializeField] GameObject dynamicFormulaMainWindow_gameobject;
    #endregion

    #region TmProVars

    [SerializeField] TextMeshProUGUI HV;
    [SerializeField] TextMeshProUGUI VV;
    [SerializeField] TextMeshProUGUI FlightTime;
    [SerializeField] TextMeshProUGUI FHD;
    [SerializeField] TextMeshProUGUI IVD;

    //[SerializeField] Sprite[] bow;

    //[SerializeField] GameObject arrow;
    //[SerializeField] GameObject arrowTest;

    #endregion


    #endregion

    #region DistanceTransform

    Vector3 transformScreem;

    float transformX;
    float transformY;

    [SerializeField] TextMeshProUGUI distanceXText;
    [SerializeField] TextMeshProUGUI heightXText;
    [SerializeField] TextMeshProUGUI timeText;

    float ETAngle = 45;

    float distanceX;
    double heightY;
    double heightFromPTOTarget;
    float time;
    float timeSquare;

    float sinValue;
    float cosValue;

    float _velocity;
    float velx;
    float vely;
    float velxSq;
    float velySQ;
    float initialVelocity;

    [SerializeField] Rigidbody2D rb;

    double height;
    float forceSquare;
    float angleSquare;


    GameObject target;

    [SerializeField] Rigidbody2D rigidbody;
    Vector2 rigidbodyVelocity;
    float rbVelX;
    float rbVelY;

    [SerializeField] Transform ground;
    [SerializeField] Transform Cannon;
    float y0;

    #endregion

    #region lrVariables

    [SerializeField] LineRenderer lr;
    Vector3 startPointLr;
    Vector3 currentPointLr;
    Vector3 endPointLr;

    [SerializeField] Camera cam;


    #endregion

    #region FW ANIM

    [SerializeField] Animator anim;

    [SerializeField] GameObject[] Formulas_Front;
    [SerializeField] GameObject[] Formulas_Back;

    [SerializeField] GameObject ibutton1;
    [SerializeField] GameObject ibutton2;

    [SerializeField] GameObject bg_Circle_Front;
    [SerializeField] GameObject bg_Circle_Back;

    #endregion

    #region BowStringVariables

    [SerializeField] LineRenderer bowStringLineRenderer1;
    [SerializeField] LineRenderer bowStringLineRenderer2;

    //[SerializeField] GameObject TopAnchor;
    //[SerializeField] GameObject BottomAnchor;

    //[SerializeField] GameObject PS1;
    //[SerializeField] GameObject PS2;


    //[SerializeField] GameObject BowString;

    static public bool isDRAG;
    static public bool isEndDrag;

    //float startAnglee;

    //bool isPointerFunction;

    //bool drawingBow;

    #endregion

    #region ArrowTrail

    static public bool isDRAGReleased;

    [SerializeField] GameObject psFire;

    #endregion



    #endregion


    #region MainFunc
    #region PointerFns
    public void OnPointerDown(PointerEventData eventData)
    {
        force = 0;
        angle = 0;
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPoint.z = 15;

        StartCoroutine(ISDRAGBool());



        //StartCoroutine(ET());

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void OnDrag(PointerEventData eventData)
    {
        _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPoint.z = 15;
        CalculateAngleFront();
        //CalculateForce();
        _Force();
        _PreShoot();

        if (SnakeChallenge_Weapon_Manager.arrowCount > 0 && PeterPan_Movement.isGEMCollected == true)
        {
            SnakeChallenge_Weapon_Manager.isFireArrowEnabled = true;
        }

        //_cannon.sprite = bow[1];
        //arrow.transform.position = new Vector3(-28f, transform.position.y, transform.position.z);
        //arrowTest.transform.position = new Vector3(-0.79f, transform.position.y, transform.position.z);

        DrawLine(startPoint, currentPoint);

        //anglePlayer2.SetActive(false);
        //forcePlayer2.SetActive(false);

        //if (anglePlayer3.activeInHierarchy == false)
        //{
        //    anglePlayer3.SetActive(true);
        //}
        //if (forcePlayer3.activeInHierarchy == false)
        //{
        //    forcePlayer3.SetActive(true);
        //}



    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //if (GM_C4.generalArrow == true && SnakeChallenge_Weapon_Manager.isFireArrowEnabled == false)
        //{
        //    _Shoot();
        //}

        if (GM_C4.fireArrow == true && GM_C4.currentBulletCountred > 0)
        {
            _Shoot();
        }

        if (GM_C4.currentBulletCountred == 0 && GM_C4.thunderArrow == true && GM_C4.currentBulletCountredblue > 0)
        {
            _Shoot();
        }


        if (GM_C4.currentBulletCountred > 0 && GM_C4.thunderArrow == true && GM_C4.currentBulletCountredblue > 0)
        {
            _Shoot();
        }

        if (GM_C4.currentBulletCountred == 0 && GM_C4.currentBulletCountredblue == 0 && GM_C4.iceArrow == true && GM_C4.currentBulletCountyellow > 0)
        {
            _Shoot();
        }


        if (GM_C4.currentBulletCountred > 0 && GM_C4.currentBulletCountredblue > 0 && GM_C4.iceArrow == true && GM_C4.currentBulletCountyellow > 0)
        {
            _Shoot();
        }

        if (SnakeChallenge_Weapon_Manager.arrowCount > 0 && PeterPan_Movement.isGEMCollected == true)
        {


            _Shoot();
            SnakeChallenge_Weapon_Manager.arrowCount--;
        }

        if (SnakeChallenge_Weapon_Manager.arrowCount <= 0)
        {
            SnakeChallenge_Weapon_Manager.isFireArrowEnabled = false;
            psFire.SetActive(false);
        }
        //Shoot(distance);

        rigidbody.AddForce(transform.right * force);

        Vector2 rigidbodyVelocity = rigidbody.velocity;
        rbVelX = rigidbodyVelocity.x; rbVelY = rigidbodyVelocity.y;
        //  Debug.Log("RigidBodyVelocity: " + rbVelX);
        EndLine();

        if (GM_C4.fireArrow == true)
        {
            GM_C4.currentBulletCountred--;

            if (GM_C4.currentBulletCountred < 0)
            {
                GM_C4.currentBulletCountred = 0;
            }
        }

        if (GM_C4.thunderArrow == true)
        {
            GM_C4.currentBulletCountredblue--;

            if (GM_C4.currentBulletCountredblue < 0)
            {
                GM_C4.currentBulletCountredblue = 0;
            }
        }

        if (GM_C4.iceArrow == true)
        {
            GM_C4.currentBulletCountyellow--;

            if (GM_C4.currentBulletCountyellow < 0)
            {
                GM_C4.currentBulletCountyellow = 0;
            }
        }
        //_cannon.sprite = bow[1];
        //arrow.transform.position = new Vector3(-25f, transform.position.y, transform.position.z);
        //arrowTest.transform.position = new Vector3(-26.88f, transform.position.y, transform.position.z);


    }

    //IEnumerator ET()
    //{
    //    isEndDrag = true;
    //    yield return new WaitForSeconds(0.1f);
    //    if (ETShoot.isETtrue == true)
    //    {
    //        isEndDrag = false;
    //    }

    //}

    IEnumerator ISDRAGBool()
    {
        isDRAG = true;
        yield return new WaitForSeconds(0.1f);
        isDRAG = false;
    }

    #endregion


    void Awake()
    {

        //AngleAdjustmentFront(45);
        //shootPointGO = GameObject.FindGameObjectWithTag("ShootPoint");
        target = GameObject.FindGameObjectWithTag("Target");
        //bombPrefab = GameObject.FindGameObjectWithTag("Bomb");

        y0 = cannon.transform.position.y - ground.transform.position.y;
        Debug.Log("y0:" + y0);
        cannon.transform.rotation = Quaternion.Euler(0f, 0f, 45);
        GM_C4.fireArrow = true;

    }
    void Update()
    {

        AngleAdjustmentFront(angle);


        if (ETShoot.isETtrue == true)
        {
            //forcePlayer.text = "Force:" + ETShoot.preciseVelocityMag.ToString("F0") + " m/s";
            forcePlayer1.text = ETShoot.preciseVelocityMag.ToString("F0") + " m/s";

            anglePlayer.text = ETAngle.ToString("F0") + "deg";
        }




        if (ExpectedTrajectory_Player.isET == false)
        {


            if (xAngle < 0)

            {
                // AngleAdjustmentFront(angle);
                //UpdateForce(distance);
                _Force();

                if (ETShoot.isETtrue == false)
                {
                    forcePlayer1.text = initialVelocity.ToString("F0") + " m/s";
                }
            }
        }


        angle__ = angle;


        forcePlayer.text = "Force:" + force.ToString("F0") + " m/s";

        //if (angle <= 91 && angle >= 0)
        //{
        if (ETShoot.isETtrue == false)
        {
            anglePlayer.text = degrees.ToString("F0") + " deg";
        }
        // }
        DynamicFormulaCalculations();
        // GetLaunchVelocity(t, sp, tar);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Height:" + height);
        }

        //if(initialVelocity < 20)
        //{
        //    cannon.sprite = cannons[0];
        //}
        //if (initialVelocity > 20 && initialVelocity < 40 )
        //{
        //    cannon.sprite = cannons[1];
        //}
        //if (initialVelocity > 40 )
        //{
        //    cannon.sprite = cannons[2];
        //}
    }
    #endregion

    #region LineRenderer

    void DrawLine(Vector3 startpointLr, Vector3 endpointLr)
    {
        lr.positionCount = 2;
        Vector3[] allPoint = new Vector3[2];
        allPoint[0] = startpointLr;
        allPoint[1] = endpointLr;
        lr.SetPositions(allPoint);
    }

    void EndLine()
    {
        lr.positionCount = 0;
    }

    #endregion


    #region AngleCalculationFront
    void CalculateAngleFront()
    {


        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        //Vector3 dir = mousePos - _cannon.transform.position;
        Vector3 dir = currentPoint - startPoint;
        //angleFront = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Atan2(dir.y, dir.x);

        if (angle > 0)
        {
            angle -= Mathf.PI;
        }
        else if (angle < 0)
        {
            angle += Mathf.PI;
        }
        //MathF.Sin(MathF.PI/180*angle)
        angleNew = angle * MathF.PI / 180;


        x2 = currentPoint.x;
        x1 = startPoint.x;

        xAngle = x2 - x1;

    }

    void AngleAdjustmentFront(float value)
    {
        degrees = value * Mathf.Rad2Deg;
        cannon.transform.rotation = Quaternion.Euler(0f, 0f, degrees);
        //_angle = value;
        //cannon.transform.rotation = Quaternion.AngleAxis(angleFront, Vector3.forward);


    }

    #endregion

    #region AngleCalculationBack
    void CalculateAngleBack()
    {
        isAngleFront = false;
        isAngleBack = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 dir = -(mousePos - transform.position);
        angleBack = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    void AngleAdjustmentBack(float value)
    {
        _angle = value;
        cannon.transform.rotation = Quaternion.AngleAxis(angleBack, Vector3.forward);
    }

    #endregion

    #region ForceCalculation
    void CalculateForce()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        distance = Vector3.Distance(mousePos, transform.position) * 2;


    }

    void UpdateForce(float distanceAmount)
    {
        force = Mathf.Clamp(distanceAmount, minPower, maxPower);


    }
    void _Force()
    {
        force = (_endPos - _startPos).magnitude;
        x_Velocity = force * Mathf.Cos(angle);
        y_Velocity = force * Mathf.Sin(angle);

        //Debug.Log("x_velocity: " + System.Math.Round(x_Velocity, 2) + ", " + "y_Velocity" + System.Math.Round(y_Velocity, 2));
    }
    #endregion

    #region Shoot

    void Shoot(float power)
    {
        GameObject bombIntantiate = Instantiate(bombPrefabPlayer, shootPoint.position, shootPoint.rotation);
        bombIntantiate.GetComponent<Bomb_Player>().Initialize(power);

    }

    void _Shoot()
    {

        GameObject bombIntantiate = Instantiate(bombPrefabPlayer, shootPoint.position, shootPoint.rotation);

        float velx = initialVelocity * Mathf.Cos(angle);
        float vely = initialVelocity * Mathf.Sin(angle);

        Vector3 velocity = new Vector3(velx, vely, 0f);

        bombIntantiate.GetComponent<Rigidbody2D>().velocity = velocity;

    }

    void _PreShoot()
    {

        GameObject bombIntantiate1 = Instantiate(prebombPrefabPlayer, shootPoint.position, shootPoint.rotation);

        velocity.x = force * Mathf.Cos((angle));
        velocity.y = force * Mathf.Sin((angle));
        velocity.z = 0;
        bombIntantiate1.GetComponent<Rigidbody2D>().velocity = velocity;

    }


    #endregion

    #region BOWSTring

    //    private void ContinueBowDraw()
    //    {
    //        Vector3 startPoint = TopAnchor.transform.position;
    //        Vector3 endPoint = BottomAnchor.transform.position;
    //        Vector3 POS1 = PS1.transform.position;
    //        Vector3 POS2 = PS2.transform.position;
    //        if (drawingBow)
    //        {
    //            bowStringLineRenderer2.gameObject.SetActive(true);
    //            bowStringLineRenderer1.SetPosition(0, startPoint);
    //            bowStringLineRenderer1.SetPosition(1, POS2);
    //            bowStringLineRenderer2.SetPosition(0, POS2);
    //            bowStringLineRenderer2.SetPosition(1, endPoint);
    //        }
    //        else
    //        {
    //            bowStringLineRenderer2.gameObject.SetActive(false);
    //            bowStringLineRenderer1.SetPosition(0, startPoint);
    //            bowStringLineRenderer1.SetPosition(1, endPoint);
    //        }

    //    }

    //    private void RenderBowString(Vector3 arrowPos)
    //    {
    //        Vector3 startPoint = TopAnchor.transform.position;
    //        Vector3 endPoint = BottomAnchor.transform.position;

    //        if (drawingBow)
    //        {
    //            bowStringLineRenderer2.gameObject.SetActive(true);
    //            bowStringLineRenderer1.SetPosition(0, startPoint);
    //            bowStringLineRenderer1.SetPosition(1, arrowPos);
    //            bowStringLineRenderer2.SetPosition(0, arrowPos);
    //            bowStringLineRenderer2.SetPosition(1, endPoint);
    //        }
    //        else
    //        {
    //            bowStringLineRenderer2.gameObject.SetActive(false);
    //            bowStringLineRenderer1.SetPosition(0, startPoint);
    //            bowStringLineRenderer1.SetPosition(1, endPoint);
    //        }
    //    }

    //    private void BeginBowDraw()
    //    {

    //        {
    //            Vector3 startPoint = TopAnchor.transform.position;
    //            Vector3 endPoint = BottomAnchor.transform.position;
    //            Vector3 POS1 = PS1.transform.position;
    //            Vector3 POS2 = PS2.transform.position;
    //            if (drawingBow)
    //            {
    //                bowStringLineRenderer2.gameObject.SetActive(true);
    //                bowStringLineRenderer1.SetPosition(0, startPoint);
    //                bowStringLineRenderer1.SetPosition(1, POS1);
    //                bowStringLineRenderer2.SetPosition(0, POS1);
    //                bowStringLineRenderer2.SetPosition(1, endPoint);
    //            }
    //            else
    //            {
    //                bowStringLineRenderer2.gameObject.SetActive(false);
    //                bowStringLineRenderer1.SetPosition(0, startPoint);
    //                bowStringLineRenderer1.SetPosition(1, endPoint);
    //            }

    //        }

    //}

    #endregion

    #region Dynamic_Formula_Window

    public void IButton1()
    {


        anim.SetTrigger("FW1");
        ibutton1.SetActive(false);
        bg_Circle_Front.SetActive(false);

        StartCoroutine(ButtonDelay1());


    }

    public void IButton2()
    {

        anim.SetTrigger("FW2");
        ibutton2.SetActive(false);
        bg_Circle_Back.SetActive(false);

        StartCoroutine(ButtonDelay2());

    }

    IEnumerator ButtonDelay1()
    {
        yield return new WaitForSeconds(1.2f);
        ibutton2.SetActive(true);
        bg_Circle_Back.SetActive(true);
        for (int i = 0; i < Formulas_Front.Length; i++)
        {
            Formulas_Front[i].SetActive(false);
            Formulas_Back[i].SetActive(true);
        }
    }

    IEnumerator ButtonDelay2()
    {
        yield return new WaitForSeconds(1.2f);
        ibutton1.SetActive(true);
        bg_Circle_Front.SetActive(true);
        for (int i = 0; i < Formulas_Front.Length; i++)
        {
            Formulas_Front[i].SetActive(true);
            Formulas_Back[i].SetActive(false);
        }

    }

    public void IButtonMDW()
    {
        if (dynamicFormulaMainWindow_gameobject.activeInHierarchy == false)
        {
            dynamicFormulaMainWindow_gameobject.SetActive(true);
        }

        //else if (_GameManager_.ISScoreReached == false)

        //{
        //    dynamicFormulaMainWindow_gameobject.SetActive(true);
        //}


        for (int i = 0; i < Formulas_Back.Length; i++)
        {
            if (Formulas_Back[i].activeInHierarchy == true)
            {
                Formulas_Back[i].SetActive(false);
            }


        }

        for (int i = 0; i < Formulas_Front.Length; i++)
        {
            if (Formulas_Front[i].activeInHierarchy == false)
            {
                Formulas_Front[i].SetActive(true);
            }


        }

        if (ibutton1.activeInHierarchy == false && Formulas_Front[0].activeInHierarchy == true)
        {
            ibutton1.SetActive(true);
        }
        if (ibutton2.activeInHierarchy == false && Formulas_Back[0].activeInHierarchy == true)
        {
            ibutton2.SetActive(true);
        }


    }

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

        velx = PreBomb_Player.preVelocity.x;
        vely = PreBomb_Player.preVelocity.y;

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
        timeSquare = ETShoot.preciseTimeInAir * ETShoot.preciseTimeInAir;



        //DistanceX
        distanceX = initialVelocity * Mathf.Cos(MathF.PI / 180 * 45) * ETShoot.preciseTimeInAir;

        //heightY
        //heightY   =  (initialVelocity * Mathf.Sin(MathF.PI / 180 * 45) * ETShoot.preciseTimeInAir - 0.5f * (9.81) * timeSquare) ;
        heightY = (initialVelocity * Mathf.Sin(MathF.PI / 180 * 45) * ETShoot.preciseTimeInAir - 0.5f * (9.81) * timeSquare) + 14.54f;

        heightFromPTOTarget = heightY - 14.54f;

        forceSquare = force * force;
        angleSquare = degrees * degrees;

        height = forceSquare * Mathf.Sin(angleSquare) / (2 * -9.81);



        #endregion

        #region TMPRODisplay

        HV.text = "<color=red>" + Vx.ToString("F2") + "</color> " + " = " + "<color=blue>" + force.ToString("F2") + "</color> " + " cos( " + "<color=blue>" + angle.ToString("F2") + "</color> " + " )";
        VV.text = "<color=red>" + Vy.ToString("F2") + "</color> " + " = " + "<color=blue>" + force.ToString("F2") + "</color> " + " sin( " + "<color=blue>" + angle.ToString("F2") + "</color> " + " )";
        FlightTime.text = "<color=red>" + t.ToString("F2") + "</color> " + " = " + "<color=blue>" + Vy.ToString("F2") + "</color> " + " / " + "<color=green>" + g.ToString("F2") + "</color> ";
        FHD.text = "<color=red>" + Mathf.Abs(Dx).ToString("F2") + "</color> " + " = " + " 2 (" + "<color=blue>" + t + "</color> " + ")" + " " + "(" + "<color=blue>" + Vx.ToString("F2") + "</color> " + ")";
        IVD.text = "<color=red>" + Mathf.Abs(Dintial).ToString("F2") + "</color> " + " = " + Dfinal.ToString() + " - " + "<color=blue>" + Vy.ToString("F2") + "</color> " + " * " + "<color=blue>" + t.ToString("F2") + "</color> "
            + " - " + " 1/2 " + "  (" + "<color=green>" + g.ToString("F2") + "</color> " + ")" + " (" + "<color=blue>" + t.ToString("F2") + "</color> " + ")" + "^2";

        #endregion

        #region MainDynamicWindow

        distanceXText.text = "<#F08EED>" + "<b>" + distanceX.ToString("F2") + "</b>" + "</color> " + " = " + "<color=white>" + initialVelocity.ToString("F2") + "</color> " + " * cos (" + "<color=white>" +
           degrees.ToString("F2") + "</color> " + ") * " + "<color=white>" + ETShoot.preciseTimeInAir.ToString("F2") + "</color> ";

        heightXText.text = "<#F08EED>" + heightFromPTOTarget.ToString("F2") + "</color> " + " = " + "<color=white>" + initialVelocity.ToString("F2") + "</color> " + " * sin (" + "<color=white>" +
          degrees.ToString("F2") + "</color> " + ") * " + "<color=white>" + ETShoot.preciseTimeInAir.ToString("F2") + "</color> " + "-" + " (1/2) * (9.81) * (" + "<color=white>" + ETShoot.preciseTimeInAir.ToString("F2") + "</color> " + ")^2 ";

        timeText.text = "<color=green>" + time.ToString("F2") + "</color> " + "= (" + "<color=white>" + initialVelocity.ToString("F2") + "</color> " + "*" + "sin( " + "<color=white>" +
           degrees.ToString("F2") + "</color> " + ") ) / 9.81";

        #endregion

    }

    #endregion

    #region UU
    private Vector3 GetLaunchVelocity(float flightTime, Vector3 startingPoint, Vector3 endPoint)
    {


        Vector3 gravityNormal = Physics.gravity.normalized;
        Vector3 dx = Vector3.ProjectOnPlane(endPoint, gravityNormal) - Vector3.ProjectOnPlane(startingPoint, gravityNormal);
        Vector3 initialVelocityX = dx / flightTime;

        Vector3 dy = Vector3.Project(endPoint, gravityNormal) - Vector3.Project(startingPoint, gravityNormal);
        Vector3 g = 0.5f * Physics.gravity * (flightTime * flightTime);
        Vector3 initialVelocityY = (dy - g) / flightTime;


        Debug.Log(initialVelocityX + initialVelocityY);

        return initialVelocityX + initialVelocityY;
    }
    #endregion
}
