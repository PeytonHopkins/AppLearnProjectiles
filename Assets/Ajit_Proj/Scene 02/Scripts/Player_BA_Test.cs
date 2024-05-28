using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Unity.VisualScripting;

public class Player_BA_Test : MonoBehaviour/*, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler*/
{

    #region Variables

    [Header ("Points")]
    Vector3 startPoint;
    Vector3 currentPoint;

    Vector3 _startPos;
    Vector3 _endPos;

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

    [Header("PrefabRef")]
    [SerializeField] GameObject bombPrefabPlayer;
    [SerializeField] GameObject prebombPrefabPlayer;
    [SerializeField] Transform shootPoint;

    [SerializeField] PhotonView photonView;
    
     #endregion

    //#region PointerFns

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    force = 0;
    //    angle = 0;
    //    startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    startPoint.z = 15;
    //    Debug.Log("OnPointerDown");
    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    _startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Debug.Log("BeginDrag");
    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    currentPoint.z = 15;
    //    AngleCalc();
    //    ForceCalc();
    //    PreShoot();
    //    Debug.Log("OnDrag");
    //}
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Shoot();
    //    Debug.Log("OnEndDrag");
    //}

    //#endregion

    #region UpdateFunc

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown();
        }

        if (Input.GetMouseButton(0))
        {
            OnDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnEndDrag();
        }

        AngleAdjustment(angle);
        DynamicFormulaCalculations();
    }

    private void OnPointerDown()
    {
        force = 0;
        angle = 0;
        startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPoint.z = 15;
        //Debug.Log("OnPointerDown");
    }

    private void OnDrag()
    {
        _endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPoint.z = 15;
        AngleCalc();
        ForceCalc();
        PreShoot();
        //Debug.Log("OnDrag");

    }

    private void OnEndDrag()
    {
        Shoot();
        //Debug.Log("OnEndDrag");
    }


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
        photonView.RPC("PunShoot", RpcTarget.All, shootPoint.position, shootPoint.rotation, angle, initialVelocity);
    }

    [PunRPC]
    void PunShoot(Vector3 position, Quaternion rotation,float ang,float init)
    {
        if(PhotonNetwork.IsMasterClient)
        {        
            GameObject bombIntantiate = PhotonNetwork.Instantiate("Arrow", position, rotation);

            float velx = init * Mathf.Cos(ang);
            float vely = init * Mathf.Sin(ang);

            Vector3 velocity = new Vector3(velx, vely, 0f);
            bombIntantiate.GetComponent<Arrow>().SetVelocity(velocity);
            
        }

        Debug.Log($"{position},{rotation},{ang},{init},{velocity.x},{velocity.y},{velocity.z}");
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

    void DynamicFormulaCalculations()
    {
        velx = Pre_ArrowPlayer.preVelocity.x;
        vely = Pre_ArrowPlayer.preVelocity.y;

        velxSq = velx * velx;
        velySQ = vely * vely;

        initialVelocity = Mathf.Sqrt(velxSq + velySQ);
    }
         #endregion

 }
