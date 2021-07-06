using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject OtherPortal;
    public bool justTP = false;


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player" && !justTP )
        {
           collider.GetComponent<PlayerController>().Interacting = true;
            collider.gameObject.transform.position = OtherPortal.transform.position;
            justTP = true;
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Interacting = false;
            justTP = false;
        }
    }
   
}

