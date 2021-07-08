using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator Anim;

    public void Start()
    {
        Anim = GetComponent<Animator>();
      
    }

    public void Update()
    {

        //Idle/breathing is standard

        //JUMP

        if (GetComponent<PlayerController>().Active == true)
        {

        if (GetComponent<PlayerController>().Grounded && GetComponent<PlayerController>().SpaceKeyDown == false  && Input.GetKey(KeyCode.Space) && GetComponent<PlayerController>().Active == true)
        {

                //Anim.Play("Base Layer.Jump, 0, 1f");
                Anim.SetTrigger("jumping");
            }

            //WALKING
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && GetComponent<PlayerController>().Active == true )
        {
            Anim.SetBool("isRolling", true);
        }
        else
        {
            Anim.SetBool("isRolling", false);
        }

        }

    }

}
