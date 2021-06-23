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


    //false when level orientation must not be changed
    public bool CanTurn = true;

    public enum LevelOrientations
    {
        normal,     //0°
        left,       //90°
        right,      //-90°
        half        //180°
    }

    LevelOrientations LevelOrientation = LevelOrientations.normal;



    public void SetLevelOrientation(LevelOrientations orientation)
    {
        LevelOrientation = orientation;
        Debug.Log("new Orientation: " + LevelOrientation);
    }

    public void SetOrientationByAngle(float zAngle)
    {
        //Debug.Log("angle = " + zAngle);

        zAngle = zAngle % 360;

        switch(Mathf.RoundToInt(zAngle))
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



    /// <summary>
    /// returns the horizontal input relative to the levelorientation
    /// </summary>
    /// <returns>a value between -1 and 1</returns>
    public float GetHorizontalAxisInput()
    {
        //Debug.Log("returning horizontal axis");

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

    /// <summary>
    /// provides a vetor pointing up relative to the levelorientation
    /// </summary>
    /// <returns>a normalised vector pointing up</returns>
    public Vector2 Up()
    {
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

    /// <summary>
    /// provides a vetor pointing down relative to the levelorientation
    /// </summary>
    /// <returns>a normalised vector pointing down</returns>
    public Vector2 Down()
    {
        return new Vector2(Up().x, -Up().y);
    }


    /// <summary>
    /// translates a vector pointing in a direction relative to the level orientation
    /// </summary>
    /// <param name="original">direction vector to be translated</param>
    /// <returns>the new direction vecot</returns>
    public Vector2 translateDirection(Vector2 original)
    {
        switch (LevelOrientation)
        {
            case LevelOrientations.normal:
                return original;
            case LevelOrientations.left:
                return new Vector2(-original.y, original.x);
            case LevelOrientations.right:
                return new Vector2(original.y, -original.x);
            case LevelOrientations.half:
                return original * -1;
            default:
                return Vector2.zero;
        }
    }
}
