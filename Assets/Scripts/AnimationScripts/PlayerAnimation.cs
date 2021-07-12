using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator Anim;
   
   // public Teleportation teleporterScript;

    public void Start()
    {
        Anim = GetComponent<Animator>();
      //  teleporterScript = GameObject.Find("TeleporterLilaB").GetComponent<Teleportation>();
    }

    public void Update()
    {

        //Idle/breathing is standard

        if(GetComponent<PlayerController>().Active == true)
        {
            //JUMP
            if (GetComponent<PlayerController>().Grounded && GetComponent<PlayerController>().SpaceKeyDown == false && Input.GetKey(KeyCode.Space))
            {
                Anim.SetTrigger("jumping");
            }

            //Walking
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Anim.SetBool("isRolling", true);
            }
            else
            {
                Anim.SetBool("isRolling", false);
            }


            //IN TELEPORT 
            /*if (teleporterScript.justTP == false)
            {
                Anim.SetTrigger("teleporting");
            }*/
        }



    }

    //IN TELEPORT 
    
   /* private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.transform.tag == "Teleporter" )
        {
            Anim.SetTrigger("teleporting");
            
        }
    }*/
  
    
}
