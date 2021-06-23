using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused;

    public GameObject PauseMenuUI;

    void Start()
    {
        PauseMenuUI.SetActive(false);
        GamePaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
                ResumeGame();

            else
                PauseGame();
        }

    }

    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GamePaused = true;
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GamePaused = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
