using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public bool Active = true;
    public Teleportation OtherPortal;
    private static bool justTP = false;

    TeleporterAnimation AnimationScript;

    private void Start()
    {
        AnimationScript = GetComponent<TeleporterAnimation>();
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Active) return;

        if (collider.transform.tag == "Player" && !justTP)
        {               
            collider.GetComponent<PlayerController>().Interacting = true;
            collider.gameObject.transform.position = OtherPortal.transform.position;

            AnimationScript.TriggerAnimation();

            justTP = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Active) return;

        if (collision.transform.tag == "Player")
        {
            collision.GetComponent<PlayerController>().Interacting = false;
            justTP = false;
        }
    }
}

