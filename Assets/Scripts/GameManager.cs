using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] Canvas GameOverScreen;
    [SerializeField] Canvas GameWonScreen;

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

        Time.timeScale = 0f;

        if (won)
            GameWonScreen.gameObject.SetActive(true);
        else
            GameOverScreen.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Physics2D.gravity = new Vector2(0, -9.81f);
        Time.timeScale = 1f;
    }
}
