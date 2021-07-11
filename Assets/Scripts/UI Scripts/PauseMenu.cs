using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused;

    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;


    void Start()
    {
        PauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        GamePaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsMenuUI.activeInHierarchy)
                PauseGame();

            else if (GamePaused)
                ResumeGame();

            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        SettingsMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Settings()
    {
        SettingsMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
