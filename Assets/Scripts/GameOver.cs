using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    private float timeCounter= 0f;


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player") && collision.GetComponent<PlayerController>().MyColorType == this.gameObject.GetComponent<BGField>().FieldColor)
            {
                //.Log("DeathCollision");

                timeCounter += Time.deltaTime;

                if (timeCounter > 2f)
                {
                    timeCounter = 0;

                    Debug.Log("Tot");

                    SceneManager.LoadScene("MarScene");
                    //GameOver Screen SetActive
                }
        }
    }

}
