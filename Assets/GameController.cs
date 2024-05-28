using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Keyboard keyboard;
    private Vector3 initialPlayerPosition = new(0, 0, -10);

    public Canvas mainMenuCanvas;

    public Image mainMenuBackground;

    public Button ContinueGameButton;

    public Transform player;

    public TextMeshProUGUI savingGameText;

    public TextMeshProUGUI levelText;


    void Start()
    {
        keyboard = Keyboard.current;
        mainMenuBackground.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        savingGameText.rectTransform.position = mainMenuBackground.rectTransform.sizeDelta - savingGameText.rectTransform.sizeDelta;

        Vector3 levelTextPosition = levelText.rectTransform.position;
        levelTextPosition.x = Screen.width / 2;
        levelText.rectTransform.position = levelTextPosition;

        SetGameOnPause();
        VerifyIfGameCanBeContinued();

        InvokeRepeating(nameof(SaveGame), 1.0f, 10.0f);
        GlobalContext.PopulateLevels();
    }

    public void StartNewGame()
    {
        SetGameOnRunning();
        player.SetPositionAndRotation(initialPlayerPosition, Quaternion.identity);
    }

    public void ContinueGame()
    {
        SetGameOnRunning();

        player.SetPositionAndRotation(
            new Vector3(
                PlayerPrefs.GetFloat("playerX"),
                PlayerPrefs.GetFloat("playerY"),
                PlayerPrefs.GetFloat("playerZ")),
            Quaternion.Euler(
                PlayerPrefs.GetFloat("playerRotationX"),
                PlayerPrefs.GetFloat("playerRotationY"),
                PlayerPrefs.GetFloat("playerRotationZ")));
        
        GlobalContext.currentLevel = PlayerPrefs.GetInt("playerLevel");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public bool IsGameSaved()
    {
        return PlayerPrefs.HasKey("playerX") &&
               PlayerPrefs.HasKey("playerY") &&
               PlayerPrefs.HasKey("playerZ") &&
               PlayerPrefs.HasKey("playerRotationX") &&
               PlayerPrefs.HasKey("playerRotationY") &&
               PlayerPrefs.HasKey("playerRotationZ") &&
               PlayerPrefs.HasKey("playerLevel");
    }

    public void VerifyIfGameCanBeContinued()
    {
        ContinueGameButton.interactable = IsGameSaved();
    }

    private void SaveGame()
    {
        savingGameText.gameObject.SetActive(true);

        PlayerPrefs.SetFloat("playerX", player.position.x);
        PlayerPrefs.SetFloat("playerY", player.position.y);
        PlayerPrefs.SetFloat("playerZ", player.position.z);

        PlayerPrefs.SetFloat("playerRotationX", player.rotation.x);
        PlayerPrefs.SetFloat("playerRotationY", player.rotation.y);
        PlayerPrefs.SetFloat("playerRotationZ", player.rotation.z);

        PlayerPrefs.SetInt("playerLevel", GlobalContext.currentLevel);

        Invoke(nameof(HideSavingGameText), 3.0f);
    }

    private void HideSavingGameText()
    {
        savingGameText.gameObject.SetActive(false);
    }

    private void SetGameOnPause()
    {
        GlobalContext.gameStatus = GameStatus.paused;
        Time.timeScale = 0;
        mainMenuCanvas.gameObject.SetActive(true);
        VerifyIfGameCanBeContinued();
    }

    private void SetGameOnRunning()
    {
        GlobalContext.gameStatus = GameStatus.running;
        Time.timeScale = 1;
        mainMenuCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if(keyboard.escapeKey.wasPressedThisFrame && !GlobalContext.isAnyPanelOpen)
        {
            if(GlobalContext.gameStatus == GameStatus.paused)
            {
                SetGameOnRunning();
            }
            else
            {   
                SetGameOnPause();
            }
        }

        levelText.text = "Level " + GlobalContext.currentLevel;
    }
}
