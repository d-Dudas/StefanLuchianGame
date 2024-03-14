using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private enum CameraView
    {
        firstPersonView,
        thirdPersonView
    };

    private CameraView currentView = CameraView.firstPersonView;

    private bool isVKeyPressed = false;

    private Keyboard keyboard;

    void Start()
    {
        keyboard = Keyboard.current;
    }

    void Update()
    {
        if(keyboard.numpad8Key.isPressed && currentView != CameraView.thirdPersonView)
        {
            transform.Rotate(new Vector3(-1, 0, 0));
        }
        else if(keyboard.numpad2Key.isPressed && currentView != CameraView.thirdPersonView)
        {
            transform.Rotate(new Vector3(1, 0, 0));
        }
        if(keyboard.vKey.wasPressedThisFrame && !isVKeyPressed)
        {
            isVKeyPressed = true;
            ChangeView();
        }
        else if (keyboard.vKey.wasReleasedThisFrame)
        {
            isVKeyPressed = false;
        }
    }

    private void ChangeView()
    {
        if(currentView == CameraView.firstPersonView)
        {
            currentView = CameraView.thirdPersonView;
            transform.position -= transform.forward * 4f;
        }
        else
        {
            currentView = CameraView.firstPersonView;
            transform.position += transform.forward * 4f;
        }
    }
}
