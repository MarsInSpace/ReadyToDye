using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator anim;
    public GameObject Halbkreis;

    private void Awake()
    {
        anim = Halbkreis.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Animator not found");
        }
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
 
    {
        if(collision.transform.tag == "Player")
        {
           // anim.SetTrigger("CircleFaster");
            anim.SetBool("SpinFaster", true);
            Debug.Log("Triggert");
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            anim.SetBool("SpinFaster", false);
            Debug.Log("Triggert nicht mehr");
        }
    }
}
