using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPortal : MonoBehaviour
{
    [SerializeField]
    GameColorTypes Color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerController>().Interacting = true;

            collision.GetComponent<PlayerController>().ChangeColor(Color);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
            collision.GetComponent<PlayerController>().Interacting = false;
    }
}
