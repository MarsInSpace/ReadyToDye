using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public bool Active = true;
    public Teleportation OtherPortal;
    public bool justTP = false;

   

    TeleporterAnimation AnimationScript;

    private void Start()
    {
        AnimationScript = GetComponent<TeleporterAnimation>();
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Active) return;

        if (collider.transform.tag == "Player" && !OtherPortal.justTP)
        {
            collider.GetComponent<PlayerController>().Interacting = true;

            collider.gameObject.transform.position = OtherPortal.transform.position;

            AnimationScript.TriggerAnimation();

            justTP = true;

            //Debug.Log("justTP = " + justTP + " in " + this.gameObject);
        }        
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!Active) return;

        if (collision.transform.tag == "Player")
        {
            if (OtherPortal.justTP)
            {
                OtherPortal.justTP = false;
                //Debug.Log("justTP = " + OtherPortal.justTP + " in " + OtherPortal.gameObject);
            }

            collision.GetComponent<PlayerController>().Interacting = false;
        }
    }


    public void SetActive(bool active)
    {
        if (Active == active) return;

        Active = active;
        AnimationScript.SwitchEnabled();
    }

}

