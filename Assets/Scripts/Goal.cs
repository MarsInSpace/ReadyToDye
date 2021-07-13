using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Goal : MonoBehaviour
{

    //bool[] allPlayers = new bool[2]; //Array mit 2 Stellen, wenn beide Stellen true = gewonnen, wenn einer rausgeht ist eine Stelle wieder false
    int PlayersInGoal;
    public bool PlayerInGoal;

    public bool WinningCondition = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag.Equals("Player"))
        {
            PlayersInGoal++;
            PlayerInGoal = true;

            if (PlayersInGoal == 2)
            {
                WinningCondition = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            PlayersInGoal--;
            if(PlayersInGoal == 0)
                PlayerInGoal = false;
        }
    }  
}
