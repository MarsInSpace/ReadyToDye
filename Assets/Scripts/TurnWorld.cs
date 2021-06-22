using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWorld : MonoBehaviour
{
    bool IsTurned;                  //is true, when world has been turned

    bool Turning;                   //true while turning, scaling and moving


    [SerializeField]
    float RotationSpeed;            //Speed at which the BG will be rotated
    

    float TriggerCoolDown;          //serves as timer to avoid immediate reactivation of the world turn (TO BE CHANGED?)

    Quaternion TargetRotation;
    Vector2 TargetGravity;

    [SerializeField]
    float CamSize;


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
        if (Turning || TriggerCoolDown > 0)
            return;

        Turning = true;

        TargetRotation =  Quaternion.Euler(0, 0, MainCam.transform.rotation.eulerAngles.z + 180);
        TargetGravity = Physics2D.gravity * -1;
    }



    void UpdateWorldTurn()
    {  
        MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation,TargetRotation, RotationSpeed);
        Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, TargetGravity, RotationSpeed);

        

        if (MainCam.transform.rotation == TargetRotation)
        {
            Turning = false;
            IsTurned = !IsTurned;
            TriggerCoolDown = 1;
        }
    }



}
