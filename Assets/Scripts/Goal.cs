using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCenter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            Debug.Log(collision.name + "Entered Goal");
        }
    }
}
