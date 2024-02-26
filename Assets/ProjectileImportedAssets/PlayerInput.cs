using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PlayerInput : MonoBehaviour
{
    #region Variables

    [SerializeField] float minPower;
    [SerializeField] float maxPower;
    float force;
    float distance;


    float _angle;
    float angleFront;
    float angleBack;
    [SerializeField] SpriteRenderer cannon;

    [SerializeField] TextMeshProUGUI anglePlayer;
    [SerializeField] TextMeshProUGUI forcePlayer;

    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform shootPoint;

    float angle;

    bool isMouseButtonDown;

    //bool isAngleFront;
    //bool isAngleBack;

    // [SerializeField] SpriteRenderer powerupSprite;

    #endregion

    #region UpdateFunc

    void Update()
    {
        if (angleFront <= 90 && angleFront >= 0)
        {
            forcePlayer.text = "Force:" + force.ToString("F0") + " m/s";
            
        }

        if (angleBack <= 90 && angleBack >= 0)
        {
            forcePlayer.text = "Force:" + force.ToString("F0") + " m/s";

        }

        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonDown = true;
            force = 0;
            angleFront = 0;
            angleBack = 0;
        }

        if (Input.GetMouseButton(0) && isMouseButtonDown == true)
        {
           
                CalculateAngleFront();
                CalculateAngleBack();
                CalculateForce();

              if(angleFront <= 90 && angleFront >= 0)
              {
                angle = angleFront;
                AngleAdjustmentFront(angle);
                
                anglePlayer.text = "Angle:" + angle.ToString("F0") + " deg";
            }


            if (angleBack <= 90 && angleBack >= 0)
            {
                AngleAdjustmentBack(angleBack);
                anglePlayer.text = "Angle:" + angleBack.ToString("F0") + " deg";
            }

            UpdateForce(distance);
        }

         if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonDown = false;
            Shoot(force);
        }
      

    }

    #endregion

    #region AngleCalculationFront
    void CalculateAngleFront()
    {
        //isAngleFront = true;
        //isAngleBack = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 dir = mousePos - transform.position;
        angleFront = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    void AngleAdjustmentFront(float value)
    {
        _angle = value;
        cannon.transform.rotation = Quaternion.AngleAxis(angleFront, Vector3.forward);
    }

    #endregion

    #region AngleCalculationBack
    void CalculateAngleBack()
    {
        //isAngleFront = false;
        //isAngleBack = true;

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

        //powerupSprite.transform.localScale = new Vector2(force / 12, force / 12);
        Debug.Log("Force: " + force);
    }
    #endregion

    #region Shoot

    void Shoot(float power )
    {
        GameObject bombIntantiate = Instantiate(bombPrefab, shootPoint.position, shootPoint.rotation);
        bombIntantiate.GetComponent <Projectile>().Initialize(power);
        //bombIntantiate.GetComponent<Rigidbody2D>().velocity = transform.right * power;
    }


    #endregion

}
