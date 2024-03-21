using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private const int maxLowerViewAngleLimit = 50;
    private const int minLowerViewAngleLimit = -1;
    private const int maxUpperViewAngleLimit = 360;
    private const int minUpperViewAngleLimit = 290;

    private Keyboard keyboard;

    void Start()
    {
        keyboard = Keyboard.current;
    }

    void Update()
    {
        if (keyboard.numpad8Key.isPressed && currentView != CameraView.thirdPersonView)
        {
            
            if (CanLookUp())
            {
                transform.Rotate(new Vector3(-1, 0, 0));
            }
        }
        else if (keyboard.numpad2Key.isPressed && currentView != CameraView.thirdPersonView)
        {
            if (CanLookDown())
            {
                transform.Rotate(new Vector3(1, 0, 0));
            }
        }
        if (keyboard.vKey.wasPressedThisFrame && !isVKeyPressed)
        {
            isVKeyPressed = true;
            ChangeView();
        }
        else if (keyboard.vKey.wasReleasedThisFrame)
        {
            isVKeyPressed = false;
        }
    }

    private bool CanLookUp()
    {
        int currentViewAngle = (int)transform.eulerAngles.x;
        return (currentViewAngle <= maxLowerViewAngleLimit && currentViewAngle >= minLowerViewAngleLimit) || 
                (currentViewAngle > minUpperViewAngleLimit && currentViewAngle <= maxUpperViewAngleLimit);
    }

    private bool CanLookDown()
    {
        int currentViewAngle = (int)transform.eulerAngles.x;
        return (currentViewAngle < maxLowerViewAngleLimit && currentViewAngle >= minLowerViewAngleLimit) ||
                (currentViewAngle >= minUpperViewAngleLimit - 1 && currentViewAngle <= maxUpperViewAngleLimit);
    }

    private void ChangeView()
    {
        if (currentView == CameraView.firstPersonView)
        {
            currentView = CameraView.thirdPersonView;
            transform.eulerAngles = new Vector3(
                20,
                transform.eulerAngles.y,
                transform.eulerAngles.z
            );
            transform.position -= transform.forward * 4f;
        }
        else
        {
            currentView = CameraView.firstPersonView;
            transform.position += transform.forward * 4f;
            transform.eulerAngles = new Vector3(
                0,
                transform.eulerAngles.y,
                transform.eulerAngles.z
            );
        }
    }
}
