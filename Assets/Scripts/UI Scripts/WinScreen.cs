using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinScreen : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            LoadMenu();
    }

    public void LoadMenu()
    {
        Physics2D.gravity = new Vector2(0, -9.81f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        Physics2D.gravity = new Vector2(0, -9.81f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevelOverview()
    {
        Physics2D.gravity = new Vector2(0, -9.81f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelOverview");
    }
}
