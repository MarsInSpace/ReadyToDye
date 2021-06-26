using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWorld : MonoBehaviour
{
    bool Turning;                   //true when this instances turning is actived
    bool TurningSquares;

    [SerializeField]
    float RotationSpeed;            //Speed at which the BG will be rotated
    float RotationSinceTurning;

    float RotationTimer;
    float TriggerCoolDown;          //serves as timer to avoid immediate reactivation of the world turn (TO BE CHANGED?)

    Quaternion TargetRotation;
    Vector2 TargetGravity;

    [SerializeField]
    Camera MainCam;

    [SerializeField]
    GameObject PlayerOne;
    [SerializeField]
    GameObject PlayerTwo;

    Collider2D BigCollider;
    Collider2D SmallCollider;

    [SerializeField]
    GameObject BigSquare;
    [SerializeField]
    GameObject SmallSquare;
    [SerializeField]
    GameObject BigBlue;
    [SerializeField]
    GameObject SmallBlue;

    bool PosOne = true;
    bool PosTwo = false;

    Vector3 oldBigPos = new Vector3(-0.23f, 2.66f);
    Vector3 oldSmallPos = new Vector3(-7.16f, -2.38f);
    Vector3 newBigPos = new Vector3(4.834f, 2.66f);
    Vector3 newSmallPos = new Vector3(0.313f, -2.38f);

    private void Start()
    {
        BigCollider = BigSquare.GetComponent<Collider2D>();
        SmallCollider = SmallSquare.GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        CheckSquarePosition(); 

        if (Turning)
        {
            UpdateWorldTurn();
        }
        if(TurningSquares)
            UpdateSquarePosition();

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
        TurningSquares = true;

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

    void CheckSquarePosition()
    {
        if (BigSquare.transform.position == oldBigPos && SmallSquare.transform.position == oldSmallPos)
        {
            PosOne = true;
            PosTwo = false;
            //Debug.Log("PosOne is true now");
        }
        if (BigSquare.transform.position == newBigPos && SmallSquare.transform.position == newSmallPos)
        {
            PosOne = false;
            PosTwo = true;
            //Debug.Log("PosTwo is true now");
        }

    }

    void MovePlayers(int sign)
    {
        float moveDistanceBig = Mathf.Abs(newBigPos.x) - Mathf.Abs(oldBigPos.x);
        float moveDistanceSmall = Mathf.Abs(newSmallPos.x) - Mathf.Abs(oldSmallPos.x);

        if (PlayerOne.GetComponent<Collider2D>().Distance(BigCollider).distance <= 0.3)
        {
            PlayerOne.transform.position = Vector3.Lerp(PlayerOne.transform.position, PlayerOne.transform.position + new Vector3((sign * moveDistanceBig), 0, 0), RotationSpeed * 0.3f);
            Debug.Log("Player Lerped");
        }

        if (PlayerOne.GetComponent<Collider2D>().Distance(SmallCollider).distance <= 0.3)
        {
            PlayerOne.transform.position = Vector3.Lerp(PlayerOne.transform.position, PlayerOne.transform.position + new Vector3((sign * moveDistanceSmall), 0, 0), RotationSpeed * 0.3f);
        }

        if (PlayerTwo.GetComponent<Collider2D>().Distance(BigCollider).distance <= 0.3)
        {
            PlayerTwo.transform.position = Vector3.Lerp(PlayerTwo.transform.position, PlayerTwo.transform.position + new Vector3((sign * moveDistanceBig), 0, 0), RotationSpeed * 0.3f);
        }

        if (PlayerTwo.GetComponent<Collider2D>().Distance(SmallCollider).distance <= 0.3)
        {
            PlayerTwo.transform.position = Vector3.Lerp(PlayerTwo.transform.position, PlayerTwo.transform.position + new Vector3((sign * moveDistanceSmall), 0, 0), RotationSpeed * 0.3f);
        }
    }

    void UpdateSquarePosition()
    {
        if (PosOne && TurningSquares)
        {
            BigSquare.transform.position = Vector3.Lerp(BigSquare.transform.position, newBigPos, RotationSpeed * 0.3f);
            SmallSquare.transform.position = Vector3.Lerp(SmallSquare.transform.position, newSmallPos, RotationSpeed * 0.3f);

            MovePlayers(1);

            Debug.Log("PosTwo is lerped to");

            BigBlue.transform.position = new Vector3(-5.14f, -2.36f, 0);
            SmallBlue.transform.position = new Vector3(-1.782f, 2.61f, 0);

            if(BigSquare.transform.position == newBigPos && SmallSquare.transform.position == newSmallPos)
            {
                TurningSquares = false;
                Debug.Log("Finished Lerping One");
            }
        }
        if (PosTwo && TurningSquares)
        {
            BigSquare.transform.position = Vector3.Lerp(BigSquare.transform.position, oldBigPos, RotationSpeed * 0.3f);
            SmallSquare.transform.position = Vector3.Lerp(SmallSquare.transform.position, oldSmallPos, RotationSpeed * 0.3f);

            MovePlayers(-1);

            Debug.Log("PosOne is lerped to");

            BigBlue.transform.position = new Vector3(-1.69f, -2.36f, 0);
            SmallBlue.transform.position = new Vector3(6.33f, 2.61f, 0);

            if (BigSquare.transform.position == oldBigPos && SmallSquare.transform.position == oldSmallPos)
            {
                TurningSquares = false;
                Debug.Log("Finished Lerping Two");
            }
        }
        else
            Debug.Log("Lerp of Bg Squares did not work");
    }
    //TO-DO's: New Collider for small orange
    // - New Collider set for light blue, activate it when lerped
}
