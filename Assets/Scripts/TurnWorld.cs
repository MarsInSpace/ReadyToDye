using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWorld : MonoBehaviour
{
    bool Turning;                   //true when this instances turning is actived

    [SerializeField]
    float RotationSpeed;            //Speed at which the BG will be rotated
    float RotationSinceTurning;

    float RotationTimer;
    float TriggerCoolDown;          //serves as timer to avoid immediate reactivation of the world turn (TO BE CHANGED?)

    Quaternion TargetRotation;
    Vector2 TargetGravity;

    [SerializeField]
    Camera MainCam;



    private void FixedUpdate()
    {
        if (Turning)
            UpdateWorldTurn();

        if (!Turning && TriggerCoolDown > 0)
        {
            TriggerCoolDown -= Time.deltaTime;
        }
    }


    //Version 4: kammera drehen, und gravity orientierung wechseln?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!OrientationMaster.Instance.CanTurn || TriggerCoolDown > 0)
            return;

        OrientationMaster.Instance.CanTurn = false;
        Turning = true;

        TargetRotation = Quaternion.Euler(0, 0, MainCam.transform.rotation.eulerAngles.z + 180);
        TargetGravity = Physics2D.gravity * -1;

        //Debug.Log("targetRotation = " + TargetRotation.eulerAngles);

        RotationTimer = 0;
        RotationSinceTurning = 0;
    }



    void UpdateWorldTurn()
    {
        RotationTimer += Time.deltaTime;

        float radiantFactor = Mathf.Cos(Mathf.PI * RotationTimer * RotationSpeed);
        float newRotationSinceTurning = 180 - 180 * (radiantFactor * Mathf.Abs(radiantFactor));
        //float newRotationSinceTurning = 90 - 90 * Mathf.Cos(2 * Mathf.PI * RotationTimer * RotationSpeed);

        float rotationAngle = newRotationSinceTurning - RotationSinceTurning;
        RotationSinceTurning = newRotationSinceTurning;

        //Debug.Log("rotationAngle = " + rotationAngle);

        //MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, TargetRotation, RotationSpeed);
        MainCam.transform.Rotate(0, 0, rotationAngle);
        Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, TargetGravity, RotationSpeed * 0.1f);


        if (Mathf.Abs(MainCam.transform.rotation.eulerAngles.z - TargetRotation.eulerAngles.z) < 0.02f)
        {
            MainCam.transform.rotation = TargetRotation;
            Physics2D.gravity = TargetGravity;

            //Debug.Log("Done Turning");

            OrientationMaster.Instance.CanTurn = true;
            Turning = false;

            //IsTurned = !IsTurned;

            //if (IsTurned)
            //    OrientationMaster.Instance.SetLevelOrientation(OrientationMaster.LevelOrientations.half);
            //else
            //    OrientationMaster.Instance.SetLevelOrientation(OrientationMaster.LevelOrientations.normal);

            OrientationMaster.Instance.SetOrientationByAngle(MainCam.transform.rotation.eulerAngles.z);

            TriggerCoolDown = 1;
        }
    }
}
