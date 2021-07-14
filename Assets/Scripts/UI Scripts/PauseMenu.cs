using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused;

    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;
    public GameObject TutorialUI;


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
            if (SettingsMenuUI.activeInHierarchy || TutorialUI.activeInHierarchy)
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
        TutorialUI.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Physics2D.gravity = new Vector2(0, -9.81f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenSettings()
    {
        SettingsMenuUI.SetActive(true);
        PauseMenuUI.SetActive(false);
        TutorialUI.SetActive(false);
    }

    public void OpenTutorial()
    {
        TutorialUI.SetActive(true);
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