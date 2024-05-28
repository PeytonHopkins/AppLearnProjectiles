using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowString : MonoBehaviour
{
    #region Variables

    [SerializeField] GameObject bow;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject bowString;

    [SerializeField] GameObject topPoint;
    [SerializeField] GameObject bottomPoint;

    [SerializeField] LineRenderer line1;
    [SerializeField] LineRenderer line2;

    bool activeDrawingBowString = true;

    #endregion

    void Start()
    {
        line1 = bowString.transform.GetChild(0).GetComponent<LineRenderer>();
        line2 = bowString.transform.GetChild(1).GetComponent<LineRenderer>();
    }

    void Update()
    {
        GenerateBowString();
    }

    void GenerateBowString()
    {
        Vector3 topPointPosition = topPoint.transform.position;
        Vector3 bottomPointPosition = bottomPoint.transform.position;
        Vector3 arrowPosition= arrow.transform.position;

        if (activeDrawingBowString)
        {
            line2.gameObject.SetActive(true);
            line1.SetPosition(0, topPointPosition);
            line1.SetPosition(1,arrowPosition);
            line2.SetPosition(0, arrowPosition);
            line2.SetPosition(1, bottomPointPosition);

        }

        else
        {
            line2.gameObject.SetActive(false);
            line1.SetPosition(0, topPointPosition);
            line1.SetPosition(1, bottomPointPosition);

        }
    }
}
