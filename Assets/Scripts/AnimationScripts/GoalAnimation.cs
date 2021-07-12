using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{
    private Animator GoalAnimator;

    //Goal GoalScript;

    private void Awake()
    {
        GoalAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        GoalAnimator.SetBool("PlayerInGoal", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoalAnimator.SetBool("PlayerInGoal", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GoalAnimator.SetBool("PlayerInGoal", false);
    }
}
