using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Keyboard keyboard;
    
    private const float speed = 3.0f;
    void Start()
    {
        keyboard = Keyboard.current;
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
    }
}
