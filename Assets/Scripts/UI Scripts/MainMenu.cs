using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SettingsMenuUI;
    public GameObject TutorialUI;


    public void Start()
    {
        SettingsMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsMenuUI.activeInHierarchy || TutorialUI.activeInHierarchy)
                HideSettings();
        }
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
        TutorialUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void OpenTutorial()
    {
        //MainMenuUI.SetActive(false); (brauchen wir nicht wenn tutorial ui opaque ist)
        TutorialUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}