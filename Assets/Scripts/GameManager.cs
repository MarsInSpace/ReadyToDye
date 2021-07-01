using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    bool GameOver;

    private void Awake()
    {
        if (GameManager.Instance == null)
            GameManager.Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void SetGameOver(bool won)
    {
        GameOver = true;

        //TODO - initiate according behaviour
        Debug.Log("Won = " + won);

        Physics2D.gravity = new Vector2(0, -9.81f);
        SceneManager.LoadScene("MarScene");
    }
}
