using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Animator not found");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
 
    {
        if(collision.transform.tag == "Player")
        {
            anim.SetBool("FasterSpin", true);
            Debug.Log("Triggert");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            anim.SetBool("FasterSpin", false);
            Debug.Log("Triggert nicht mehr");
        }
    }
    
}
