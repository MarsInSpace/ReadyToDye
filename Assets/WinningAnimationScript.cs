using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningAnimationScript : MonoBehaviour
{
    Animator Anim;
    GameObject GoalObject;

    float waitforAnimation = 0;
    

    private void Start()
    {
        Anim = GetComponent<Animator>();
        GoalObject = GameObject.Find("Goal");
    
    }


   void Update()
    {
        if (GoalObject.GetComponent<Goal>().WinningCondition == true)
        {

            Anim.SetTrigger("Win");

            waitforAnimation += Time.deltaTime;

            if(waitforAnimation >1.15f )
            {
                GameManager.Instance.SetGameOver(true);
            }

        }
    }

    
}

