using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SettingsMenuUI;

    public void Start()
    {
        SettingsMenuUI.SetActive(false);
    }

    //loads level overview
    public void LoadLevelOverview()
    {
        SceneManager.LoadScene("LevelOverview");
    }

    public void ShowSettings()
    {
        SettingsMenuUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }

    public void HideSettings()
    {
        SettingsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
