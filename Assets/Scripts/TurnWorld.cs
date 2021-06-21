using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWorld : MonoBehaviour
{
    bool IsTurned;                  //is true, when world has been turned

    bool Turning;                   //true while turning, scaling and moving

    //Version 3

    [SerializeField]
    GameObject BG;                  //Parentobject of all Objects, that should be scaled and turned
    [SerializeField]
    GameObject OtherObjects;        //Parent Object of all objects that should be moved but not scaled


    [SerializeField]
    float RotationSpeed;            //Speed at which the BG will be rotated
    [SerializeField]
    float ScalingSpeed;             //Speed at which Lerping will be executed

    Vector3 NewScale;               //Placeholder Var for remembering the target Scale of the BG, while lerping
    Vector3[] NewObjectPositions;   //Placeholder var for rememberring target positions of each object to be moved (performance?)

    float TriggerCoolDown;          //serves as timer to avoid immediate reactivation of the world turn (TO BE CHANGED?)



    ////Version 4

    //[SerializeField]
    //Camera MainCam;

    

    //Version 3: Scale and rotate Background (and Objects seperately)
        //TODO:
            //make look good...


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





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TriggerCoolDown > 0)
            return;


        NewObjectPositions = new Vector3[OtherObjects.transform.childCount];

        if (IsTurned)
        {
            IsTurned = false;
                        
            NewScale = new Vector3((BG.transform.localScale.x / 9) * 16, (BG.transform.localScale.y / 16) * 9, BG.transform.localScale.z);

            foreach (Transform block in OtherObjects.GetComponentsInChildren<Transform>())
            {
                if (block.gameObject == OtherObjects)
                    continue;

                NewObjectPositions[block.GetSiblingIndex()] = new Vector3((block.transform.position.y / 9) * 16, (-block.transform.position.x / 16) * 9, block.transform.position.z);
            }
        }
        else
        {
            IsTurned = true;

            NewScale = new Vector3((BG.transform.localScale.x / 16) * 9, (BG.transform.localScale.y / 9) * 16, BG.transform.localScale.z);

            foreach (Transform block in OtherObjects.GetComponentsInChildren<Transform>())
            {
                if (block.gameObject == OtherObjects)
                    continue;

                //Debug.Log("Object: " + block.gameObject.name + " - SiblingIndex: " + block.GetSiblingIndex());

                NewObjectPositions[block.GetSiblingIndex()] = new Vector3((-block.transform.position.y / 9) * 16, (block.transform.position.x / 16) * 9, block.transform.position.z);
            }
        }


        Turning = true;
        GetComponent<CircleCollider2D>().isTrigger = false;

        foreach (Transform block in OtherObjects.GetComponentsInChildren<Transform>())
        {
            if (block.tag.Equals("Player"))
                block.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        foreach (GameObject field in GameObject.FindGameObjectsWithTag("BackgroundField"))
            field.GetComponent<PolygonCollider2D>().enabled = false;
    }





    void UpdateWorldTurn()
    {
        if (IsTurned)
        {
            if (BG.transform.rotation.eulerAngles.z < 90)
                BG.transform.Rotate(new Vector3(0, 0, RotationSpeed));
            else
                BG.transform.rotation = Quaternion.Euler(0, 0, 90);

            BG.transform.localScale = Vector3.Lerp(BG.transform.localScale, NewScale, ScalingSpeed);

            foreach (Transform block in OtherObjects.GetComponentsInChildren<Transform>())
            {
                if (block.gameObject == OtherObjects)
                    continue;

                block.transform.rotation = Quaternion.Lerp(block.transform.rotation, Quaternion.Euler(0, 0, 90), ScalingSpeed);

                block.transform.position = Vector3.Lerp(block.transform.position, NewObjectPositions[block.GetSiblingIndex()], ScalingSpeed * 0.9f);
            }
        }
        else
        {
            if (BG.transform.rotation.eulerAngles.z < 90 && BG.transform.rotation.eulerAngles.z > 0)
                BG.transform.Rotate(new Vector3(0, 0, -RotationSpeed));
            else
                BG.transform.rotation = Quaternion.identity;

            BG.transform.localScale = Vector3.Lerp(BG.transform.localScale, NewScale, ScalingSpeed);

            foreach (Transform block in OtherObjects.GetComponentsInChildren<Transform>())
            {
                if (block.gameObject == OtherObjects)
                    continue;

                block.transform.rotation = Quaternion.Lerp(block.transform.rotation, Quaternion.Euler(0, 0, 0), ScalingSpeed);

                block.transform.position = Vector3.Lerp(block.transform.position, NewObjectPositions[block.GetSiblingIndex()], ScalingSpeed * 0.9f);
            }
        }



        if (Mathf.Abs(BG.transform.localScale.x - NewScale.x) < 0.005f && Mathf.Abs(BG.transform.localScale.y - NewScale.y) < 0.005f)
        {
            BG.transform.localScale = NewScale;

            foreach (Transform block in OtherObjects.GetComponentsInChildren<Transform>())
            {
                if (block.gameObject == OtherObjects)
                    continue;

                if (IsTurned)
                    block.transform.rotation = Quaternion.Euler(0, 0, 90);
                else
                    block.transform.rotation = Quaternion.Euler(0, 0, 0);

                block.transform.position = NewObjectPositions[block.GetSiblingIndex()];

                if (block.tag.Equals("Player"))
                    block.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }

            foreach (GameObject field in GameObject.FindGameObjectsWithTag("BackgroundField"))
                field.GetComponent<PolygonCollider2D>().enabled = true;

            foreach (BGEdge edge in BG.GetComponentsInChildren<BGEdge>())
                edge.UpdateEffectorOrientation(IsTurned);

            Turning = false;
            TriggerCoolDown = 2;
        }
    }


    ////Version 4: kammera drehen, und gravity orientierung wechseln?
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (TriggerCoolDown > 0)
    //        return;


    //    Turning = true;



    //    TriggerCoolDown = 3;
    //}

    //void UpdateWorldTurn()
    //{
    //    if (IsTurned)
    //    {
    //        if (MainCam.transform.rotation.eulerAngles.z < 0)
    //            MainCam.transform.Rotate(new Vector3(0, 0, RotationSpeed));
    //        else
    //        {
    //            MainCam.transform.rotation = Quaternion.Euler(0, 0, 0);
    //            Turning = false;
    //        }
    //    }
    //    else
    //    {
    //        if (MainCam.transform.rotation != Quaternion.Euler(0, 0, -90)) 
    //        {
    //            MainCam.transform.rotation = Quaternion.Lerp(MainCam.transform.rotation, Quaternion.Euler(0, 0, -90), RotationSpeed);
    //            Physics2D.gravity = Vector2.Lerp(Physics2D.gravity, new Vector2(-9.81f, 0), RotationSpeed);
    //        }
    //        else
    //            Turning = false;
    //    }


    //    if (!Turning)
    //    {
    //        IsTurned = !IsTurned;
    //    }
    //}



}
