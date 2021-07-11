using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private float timeCounter = 0;
    private float GameOverTimeDelay = 2;



    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player") && collision.GetComponent<PlayerController>().MyColorType == this.gameObject.GetComponent<BGField>().FieldColor)
        {
            //Debug.Log("Check for overlaps");

            //Check that Player is only overlaping one BGField
            Collider2D[] PlayerOverlaps = Physics2D.OverlapBoxAll(collision.transform.position, collision.transform.localScale, 0);

            int counter = 0;

            foreach(Collider2D col in PlayerOverlaps)
            {
                if (col.gameObject.GetComponent<BGField>())
                    counter++;

                if (counter == 2)
                    return;
            }

            //Debug.Log("only one Overlap found");


            //.Log("DeathCollision");

            timeCounter += Time.deltaTime;
            //Debug.Log("timer = " + timeCounter);


            collision.GetComponent<PlayerEffects>().Die(timeCounter);


            if (timeCounter > GameOverTimeDelay)
            {
                timeCounter = 0;
                GameManager.Instance.SetGameOver(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
            timeCounter = 0;
    }
}
