using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGField : MonoBehaviour
{
    [SerializeField]
    GameColorTypes FieldColor;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag.Equals("Player") && !collision.transform.IsChildOf(this.transform))
    //    {
    //        Debug.Log(collision.name + " enters " + this.name);

    //        ColliderDistance2D overlap = this.GetComponent<BoxCollider2D>().Distance(collision);

    //        Vector3 correctionVector = overlap.pointA - overlap.pointB;

    //        Debug.Log("old pos = " + collision.transform.position);


    //        //correct position
    //        collision.transform.position += new Vector3(correctionVector.x, correctionVector.y, 0);

    //        Debug.Log("distance = " + correctionVector);
    //        Debug.Log("new pos = " + collision.transform.position);


    //        collision.transform.position = collision.transform.position + correctionVector;
    //    }
    //}


    ////TODO: Make work :D
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag.Equals("Player") && collision.GetComponent<PlayerController>().MyColorType == FieldColor)
    //    {
    //        if (collision.GetComponent<Rigidbody2D>().Distance(GetComponent<BoxCollider2D>()).distance < -collision.transform.localScale.x)
    //            Debug.Log("LifeDraining");
    //    }
    //}


    private void OnTriggerExit2D(Collider2D collision)
    {
        //cheep disable
        if (GetComponent<BoxCollider2D>().enabled == false)
            return;

        //is trigger player and actually out of field or just interacting with other collider
        if (collision.gameObject.tag.Equals("Player") && collision.GetComponent<PlayerController>().Interacting == false)
        {
            //Debug.Log(this.gameObject.name + " changed " + collision.gameObject.name + " to Color " + FieldColor);
            collision.gameObject.GetComponent<PlayerController>().ChangeColor(FieldColor);
        }
    }
}
