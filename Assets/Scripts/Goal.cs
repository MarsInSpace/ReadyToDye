using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Goal : MonoBehaviour
{
 
    bool[] allPlayers = new bool[2]; //Array mit 2 Stellen, wenn beide Stellen true = gewonnen, wenn einer rausgeht ist eine Stelle wieder false
    bool allPlayersInGoal = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerA")
        {
            allPlayers[0] = true;
            Debug.Log("Player A true");
        }

        else if (collision.gameObject.name == "PlayerB")
        {
            allPlayers[1] = true;
            Debug.Log("Player B true");
        }
        else return;


        if (allPlayers[0] && allPlayers[1])
        {
            allPlayersInGoal = true;
            Debug.Log("allPlayers true");

        }
        else allPlayersInGoal = false;
        


        if (allPlayersInGoal == true)
        {
            //Winning Screen!
            SceneManager.LoadScene("MarScene");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerA")
        {
            allPlayers[0] = false;
        }

        else if (collision.gameObject.name == "PlayerB")
        {
            allPlayers[1] = false;
        }
    }

  
}
