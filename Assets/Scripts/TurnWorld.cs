using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWorld : MonoBehaviour
{
    //bool IsTurned;                  //is true, when world has been turned

    bool Turning;

    [SerializeField]
    float RotationSpeed;            //Speed at which the BG will be rotated    

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

            if(TriggerCoolDown <= 0)
                GetComponent<CircleCollider2D>().isTrigger = true;
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
    }



    void UpdateWorldTurn()
    {  
        MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, TargetRotation, RotationSpeed);
        Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, TargetGravity, RotationSpeed);


        if (Mathf.Abs(MainCam.transform.rotation.eulerAngles.z - TargetRotation.eulerAngles.z) < 0.02f)
        {
            MainCam.transform.rotation = TargetRotation;
            Physics2D.gravity = TargetGravity;

            Debug.Log("Done Turning");

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
