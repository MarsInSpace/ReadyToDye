using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverview : MonoBehaviour
{
    public GameObject LevelOverviewUI;
    public GameObject SettingsMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        SettingsMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingsMenuUI.activeInHierarchy)
            {
                HideSettings();
            }

            else
            {
                ShowSettings();
            }
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowSettings()
    {
        SettingsMenuUI.SetActive(true);
        LevelOverviewUI.SetActive(false);
    }    

    public void HideSettings()
    {
        SettingsMenuUI.SetActive(false);
        LevelOverviewUI.SetActive(true);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("MarScene");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("MarScene");
    }
}
