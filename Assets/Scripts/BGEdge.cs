using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGEdge : MonoBehaviour
{
    bool FlipAngle;

    //TODO fix flipangle

    public void UpdateEffectorOrientation(bool isTurned)
    {

        if(!isTurned)
        {

            if (GetComponent<PlatformEffector2D>().rotationalOffset < 0)
                GetComponent<PlatformEffector2D>().rotationalOffset = 360 + GetComponent<PlatformEffector2D>().rotationalOffset;

            if (GetComponent<PlatformEffector2D>().rotationalOffset > 270 || GetComponent<PlatformEffector2D>().rotationalOffset < 90)
                FlipAngle = false;
            else
                FlipAngle = true;




            //float gradient = Mathf.Tan(GetComponent<PlatformEffector2D>().rotationalOffset * Mathf.Deg2Rad);

            //Debug.Log("offset: " + GetComponent<PlatformEffector2D>().rotationalOffset);

            //Debug.Log("gradient: " + gradient);

            //Vector2 gradientTranslation = new Vector2((gradient / 9f) * 16, (-1f / 16f) * 9);

            //Debug.Log("gradient translated: " + gradientTranslation);

            //Debug.Log("new offset: " + (-Mathf.Atan((16f / 9f) / (9f / 16f) * (gradientTranslation.x / gradientTranslation.y)) * Mathf.Rad2Deg));



            



            Vector2[] points = GetComponent<EdgeCollider2D>().points;

            if (points.Length > 2)
                Debug.LogError("Too many points on edgcollider of " + gameObject.name);

            Vector2 distance = points[0] - points[1];

            if (distance.x < 0)
                distance *= -1;

            if (FlipAngle)
                GetComponent<PlatformEffector2D>().rotationalOffset = 180 + Mathf.Atan(distance.y / distance.x) * Mathf.Rad2Deg;
            else
                GetComponent<PlatformEffector2D>().rotationalOffset = Mathf.Atan(distance.y / distance.x) * Mathf.Rad2Deg;

        }
        //Debug.Log("Updated " + gameObject.name);

        //Vector2[] points = GetComponent<EdgeCollider2D>().points;

        //if (points.Length > 2)
        //    Debug.LogError("Too many points on edgcollider of " + gameObject.name);

        //Debug.Log("points (Collider): " + points[0] + ", " + points[1]);
        //Debug.Log("points (World): " + transform.TransformPoint(points[0]) + ", " + transform.TransformPoint(points[1]));


        //Vector2 distance = transform.TransformPoint(points[0]) - transform.TransformPoint(points[1]);

        //points[0] = new Vector3((-points[0].y / 9) * 16, (points[0].x / 16) * 9);
        //points[1] = new Vector3((-points[1].y / 9) * 16, (points[1].x / 16) * 9);

        //Vector2 distance = points[0] - points[1];




        //if (distance.x < 0)
        //    distance *= -1;

        //Debug.Log("Steigung: " + distance.y / distance.x);

        //Debug.Log("new orientation: " + -Mathf.Atan((16f / 9f) / (9f / 16f) * (distance.x / distance.y)) * Mathf.Rad2Deg);


        //Vector2 temp = points[0] - points[1];
        //if (temp.x < 0)
        //    temp *= -1;
        //Debug.Log("is this the same?: " +  Mathf.Atan((temp.y / temp.x)) * Mathf.Rad2Deg);
        else if (isTurned)
        {

            if (GetComponent<PlatformEffector2D>().rotationalOffset < 0)
                GetComponent<PlatformEffector2D>().rotationalOffset = 360 + GetComponent<PlatformEffector2D>().rotationalOffset;

            if (GetComponent<PlatformEffector2D>().rotationalOffset > 270 || GetComponent<PlatformEffector2D>().rotationalOffset < 90)
                FlipAngle = false;
            else
                FlipAngle = true;

            //Debug.Log("backwards calculation (Steigung Original): " + Mathf.Tan(GetComponent<PlatformEffector2D>().rotationalOffset * Mathf.Deg2Rad));
            //Debug.Log("is this the same?: " + (points[0] - points[1]).y / (points[0] - points[1]).x);

            float gradient = Mathf.Tan(GetComponent<PlatformEffector2D>().rotationalOffset * Mathf.Deg2Rad);
            Vector2 gradientTranslation = new Vector2((-gradient / 9f) * 16, (1f / 16f) * 9);

            //Debug.Log("steigung Uebersetzung (Vector): " + steigungUebersetzung);
            //Debug.Log("steigung Uebersetzung: " + (steigungUebersetzung.y / steigungUebersetzung.x));


            //if(FlipAngle)
            //    Debug.Log("backwards calculation (Steigung Uebersetzt): " + (180 - Mathf.Atan((16f / 9f) / (9f / 16f) * (steigungUebersetzung.x/steigungUebersetzung.y)) * Mathf.Rad2Deg));
            //else
            //    Debug.Log("backwards calculation (Steigung Uebersetzt): " + -Mathf.Atan((16f / 9f) / (9f / 16f) * (steigungUebersetzung.x / steigungUebersetzung.y)) * Mathf.Rad2Deg);





            //GetComponent<PlatformEffector2D>().rotationalOffset =  Mathf.Atan(distance.y / distance.x) * Mathf.Rad2Deg;

            //GetComponent<PlatformEffector2D>().rotationalOffset = -Mathf.Atan((16f/9f) / (9f/16f) * (distance.x / distance.y)) * Mathf.Rad2Deg;

            if (FlipAngle)
                GetComponent<PlatformEffector2D>().rotationalOffset = (180 - Mathf.Atan((16f / 9f) / (9f / 16f) * (gradientTranslation.x / gradientTranslation.y)) * Mathf.Rad2Deg);
            else
                GetComponent<PlatformEffector2D>().rotationalOffset = -Mathf.Atan((16f / 9f) / (9f / 16f) * (gradientTranslation.x / gradientTranslation.y)) * Mathf.Rad2Deg;


            //GetComponent<PlatformEffector2D>().rotationalOffset = Mathf.Tan(GetComponent<PlatformEffector2D>().rotationalOffset) * (16f / 9f) / (9f / 16f);
        }
    }
}

