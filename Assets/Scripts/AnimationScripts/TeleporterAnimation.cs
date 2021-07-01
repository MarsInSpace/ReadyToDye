using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterAnimation : MonoBehaviour
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

    public void TriggerAnimation()
    {
        anim.SetTrigger("SpinFaster");
    }
}
