using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuButtons;
    public GameObject CreditsPage;

    void Start()
    {
        // Debug.Log("Hello World!");
        MainMenuButton();
        // CreditsButton();
    }

    public void PlayNowButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void MainMenuButton()
    {
        Debug.Log("In Main Menu button function");
        MainMenuButtons.SetActive(true);
        CreditsPage.SetActive(false);
    }

    public void CreditsButton()
    {
        Debug.Log("In Credits button function");
        MainMenuButtons.SetActive(false);
        CreditsPage.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
