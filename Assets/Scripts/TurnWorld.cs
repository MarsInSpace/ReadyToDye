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

    WorldTurnAnimation AnimationScript;


    private void Start()
    {
        AnimationScript = GetComponent<WorldTurnAnimation>();
    }

    private void FixedUpdate()
    {


        if (Turning)
        {
            UpdateWorldTurn();
        }

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

        OrientationMaster.Instance.DisableTurning();
        Turning = true;

        TargetRotation = Quaternion.Euler(0, 0, MainCam.transform.rotation.eulerAngles.z + 180);
        TargetGravity = Physics2D.gravity * -1;


        RotationTimer = 0;
        RotationSinceTurning = 0;

        AnimationScript.TriggerAnimation();
    }



    void UpdateWorldTurn()
    {
        RotationTimer += Time.deltaTime;

        float radiantFactor = Mathf.Cos(Mathf.PI * RotationTimer * RotationSpeed);
        float newRotationSinceTurning = 180 - 180 * (radiantFactor * Mathf.Abs(radiantFactor));


        float rotationAngle = newRotationSinceTurning - RotationSinceTurning;
        RotationSinceTurning = newRotationSinceTurning;

        MainCam.transform.Rotate(0, 0, rotationAngle);
        Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, TargetGravity, RotationSpeed * 0.1f);


        if (Mathf.Abs(MainCam.transform.rotation.eulerAngles.z - TargetRotation.eulerAngles.z) < 0.02f)
        {
            MainCam.transform.rotation = TargetRotation;
            Physics2D.gravity = TargetGravity;

            //Debug.Log("Done Turning");

            OrientationMaster.Instance.EnableTurning();
            Turning = false;

            OrientationMaster.Instance.SetOrientationByAngle(MainCam.transform.rotation.eulerAngles.z);

            TriggerCoolDown = 1;
        }
    }
}
