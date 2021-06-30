using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTurn : MonoBehaviour
{
    [SerializeField]
    Vector3 StartPosition;
    [SerializeField]
    Vector3 PositionB;

    [SerializeField]
    float Speed;

    Vector3 newPosition;

    Vector3 Distance;

    bool Moving;

    float MoveTimer;


    private void Start()
    {
        newPosition = StartPosition;
        OrientationMaster.Instance.OnTurn += InitMove;
    }
    private void FixedUpdate()
    {
        if(Moving)
        {
            MoveTimer += Time.deltaTime;
            Move();
        }
    }

    void InitMove()
    {
        if (newPosition == StartPosition)
            newPosition = PositionB;
        else
            newPosition = StartPosition;

        Distance = newPosition - transform.position;


        Vector3 boxSize = GetComponent<BoxCollider2D>().size;
        RaycastHit2D[] collisions = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        foreach(RaycastHit2D collision in collisions)
        {
            if (collision.collider.tag.Equals("Player"))
            {
                collision.collider.transform.parent = this.transform;
                collision.collider.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                collision.collider.gameObject.GetComponent<PlayerController>().CanChangeColor = false;
            }
        }

        Moving = true;
    }


    private void Move()
    {
        //transform.position = Vector3.Lerp(transform.position, newPosition, Speed);

        float step = Mathf.Pow(Mathf.Sin(MoveTimer * Speed), 1);

        transform.position = transform.position + Distance * step;


        if ((transform.position - newPosition).magnitude < 0.1f)
        {
            transform.position = newPosition;
            Moving = false;
            MoveTimer = 0;

            PlayerController[] playerScripts = transform.GetComponentsInChildren<PlayerController>();
            foreach (PlayerController playerScript in playerScripts)
            {
                playerScript.transform.parent = null;
                playerScript.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                playerScript.CanChangeColor = true;
            }
        }
    }
}
