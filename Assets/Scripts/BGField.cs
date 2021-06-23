using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGField : MonoBehaviour
{
    [SerializeField]
    GameColorTypes FieldColor;

    private void OnTriggerExit2D(Collider2D collision)
    {
        //cheep disable
        if (GetComponent<BoxCollider2D>().enabled == false)
            return;

        //is trigger player and actually out of field or just interacting with other collider?
        if (collision.gameObject.tag.Equals("Player") && collision.GetComponent<PlayerController>().Interacting == false)
        {
            //Debug.Log(this.gameObject.name + " changed " + collision.gameObject.name + " to Color " + FieldColor);
            collision.gameObject.GetComponent<PlayerController>().ChangeColor(FieldColor);
        }
    }
}
