using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuButtons;
    public GameObject CreditsPage;

    void Start()
    {
        MainMenuButton();
    }

    public void PlayNowButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void MainMenuButton()
    {
        MainMenuButtons.SetActive(true);
        CreditsPage.SetActive(false);
    }

    public void CreditsButton()
    {
        MainMenuButtons.SetActive(false);
        CreditsPage.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
