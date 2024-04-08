using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Keyboard keyboard;
    private readonly GameSaver gameSaver = new();
    
    private const float speed = 3.0f;
    void Start()
    {
        keyboard = Keyboard.current;
        InvokeRepeating(nameof(SaveGame), 3.0f, 3.0f);
        LoadPlayerPosition();
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        if(keyboard.wKey.isPressed)
        {
            transform.position += transform.forward * step;
        }
        else if(keyboard.sKey.isPressed)
        {
            transform.position -= transform.forward * step;
        }

        if(keyboard.aKey.isPressed)
        {
            transform.position -= transform.right * step;
        }
        else if(keyboard.dKey.isPressed)
        {
            transform.position += transform.right * step;
        }
        
        if(keyboard.numpad4Key.isPressed)
        {
            transform.Rotate(new Vector3(0, -1, 0), Space.World);
        }
        else if(keyboard.numpad6Key.isPressed)
        {
            transform.Rotate(new Vector3(0, 1, 0), Space.World);
        }
        
        if(keyboard.shiftKey.isPressed)
        {
            transform.position += transform.up * step;
        }
        else if(keyboard.ctrlKey.isPressed)
        {
            transform.position -= transform.up * step;
        }

        if(keyboard.escapeKey.isPressed)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
        }
    }

    private void SaveGame()
    {
        gameSaver.SavePlayerData(transform);

        Debug.Log("Game Saved");
    }

    private void LoadPlayerPosition()
    {   
        gameSaver.LoadSavedPlayerData(transform);
    }
}
