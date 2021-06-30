using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    private float timeCounter= 0f;

    GameColorTypes GameColor1;
    GameColorTypes GameColor2;

    GameColorTypes BGField1Color;

 
    private void Start()
    {
        GameColor1 = GameObject.Find("PlayerA").GetComponent<PlayerController>().MyColorType;
        GameColor2 = GameObject.Find("PlayerB").GetComponent<PlayerController>().MyColorType;
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        //var 1. collision.gameObject.tag.Equals("Player") && this.gameObject.GetComponent<BGField>().FieldColor == GameColor1 || this.gameObject.GetComponent<BGField>().FieldColor == GameColor2

        //var 2. collision.gameObject.tag.Equals("Player") && this.gameObject.GetComponent<BGField>().FieldColor.Equals(GameColor1) || this.gameObject.GetComponent<BGField>().FieldColor.Equals(GameColor2)

        //var 3.  this.gameObject.GetComponent <BGField>().FieldColor.Equals(collision.gameObject.GetComponent<PlayerController>().MyColorType)


        if (collision.gameObject.tag.Equals("Player") && collision.GetComponent<PlayerController>().MyColorType == this.gameObject.GetComponent<BGField>().FieldColor)
            {
                Debug.Log("Das hier klappt");

                timeCounter += Time.deltaTime;

                if (timeCounter > 2f)
                {
                    timeCounter = 0;

                    Debug.Log("Bin bis hier gekommen");

                    SceneManager.LoadScene("SampleScene 1");
                    //GameOver Screen SetActive
                }
        }
    }

}
