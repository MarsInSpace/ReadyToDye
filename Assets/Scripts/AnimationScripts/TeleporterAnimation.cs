using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterAnimation : MonoBehaviour
{
    Animator anim;

    [SerializeField] Sprite InnerSprite;
    [SerializeField] Sprite OuterSprite;

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

    public void SwitchEnabled()
    {
        SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Sprite temp = sr.sprite;
        sr.sprite = InnerSprite;
        InnerSprite = temp;

        sr = transform.GetChild(1).GetComponent<SpriteRenderer>();
        temp = sr.sprite;
        sr.sprite = OuterSprite;
        OuterSprite = temp;

        GetComponent<Animator>().enabled = !GetComponent<Animator>().enabled;
    }
}
