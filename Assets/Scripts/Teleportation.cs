using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject OtherPortal;
    private static bool justTP = false;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player" && !justTP )
        {
            collider.GetComponent<PlayerController>().Interacting = true;

            Vector3 posOffset = transform.position - collider.transform.position;
            collider.gameObject.transform.position = OtherPortal.transform.position + posOffset;
            justTP = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Interacting = false;
            justTP = false;
        }
    }

}

