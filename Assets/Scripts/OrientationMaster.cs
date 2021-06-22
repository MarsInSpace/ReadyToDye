using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationMaster : MonoBehaviour
{
    public static OrientationMaster Instance;

    private void Awake()
    {
        if (OrientationMaster.Instance == null)
            OrientationMaster.Instance = this;
        else
            Destroy(this.gameObject);
    }


    //public float zRotatoin;

    public enum LevelOrientations
    {
        normal, 
        left, 
        right, 
        half
    }

    LevelOrientations LevelOrientation = LevelOrientations.normal;

    public void SetLevelOrientation(LevelOrientations orientation)
    {
        LevelOrientation = orientation;
        Debug.Log("new Orientation: " + LevelOrientation);
    }

    public void UpdateLvlOrientation(float zAngle)
    {
        zAngle = zAngle % 360;

        switch(zAngle)
        {
            case 0:
                LevelOrientation = LevelOrientations.normal;
                break;
            case 180:
            case -180:
                LevelOrientation = LevelOrientations.half;
                break;
            case 90:
            case -270:
                LevelOrientation = LevelOrientations.left;
                break;
            case -90:
            case 270:
                LevelOrientation = LevelOrientations.right;
                break;
            default:
                Debug.LogError("invalid Angle");
                break;
        }
    }

    //public void TurnLevel(float angle)
    //{
    //    if (angle <= 180 && angle >= -180)
    //    {
    //        zRotatoin += angle;
    //        zRotatoin = zRotatoin % 180;
    //    }
    //    else
    //        Debug.Log("invalid angle passed");
    //}

    //public void SetLevelRotation(float angle)
    //{
    //    angle = angle % 360;

    //    if (angle > 180 || angle < -180)
    //        zRotatoin = (angle % 180) * -1;
    //    else
    //        zRotatoin = angle;
    //}


    public float GetHorizontalAxis()
    {
        Debug.Log("returning horizontal axis");

        switch (LevelOrientation)
        {
            case LevelOrientations.normal: 
            case LevelOrientations.left:
                return Input.GetAxisRaw("Horizontal");
            case LevelOrientations.half:
            case LevelOrientations.right:
                return Input.GetAxisRaw("Horizontal") * -1;
            default:
                return 0;
        }
    }

    public Vector2 Up()
    {
        Debug.Log("returning up vector");

        switch (LevelOrientation)
        {
            case LevelOrientations.normal:
                return Vector2.up;
            case LevelOrientations.left:
                return  Vector2.left;
            case LevelOrientations.right:
                return Vector2.right;
            case LevelOrientations.half:
                return Vector2.down;
            default:
                return Vector2.zero;
        }
    }

    public Vector2 Down()
    {
        return new Vector2(Up().y, Up().x);
    }

}
