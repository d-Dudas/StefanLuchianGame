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
    private bool isEnterKeyPressed = false;
    private const int maxLowerViewAngleLimit = 50;
    private const int minLowerViewAngleLimit = -1;
    private const int maxUpperViewAngleLimit = 360;
    private const int minUpperViewAngleLimit = 290;
    private const int minimumInteractionDistanceUnit = 2;

    public GameObject cursor;

    public Transform head;

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
        if(keyboard.enterKey.wasPressedThisFrame && !isEnterKeyPressed && currentView == CameraView.firstPersonView)
        {
            isEnterKeyPressed = true;
            ObjectRaycast();
        }
        else if(keyboard.enterKey.wasReleasedThisFrame)
        {
            isEnterKeyPressed = false;
        }
        if(currentView == CameraView.firstPersonView)
        {
            ObjectHover();
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
            cursor.SetActive(false);
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
            AdjustCameraBeforeViewChange();
            cursor.SetActive(true);
            currentView = CameraView.firstPersonView;
            transform.position += transform.forward * 4.3f;
            transform.eulerAngles = new Vector3(
                0,
                transform.eulerAngles.y,
                transform.eulerAngles.z
            );
        }
    }

    private void AdjustCameraBeforeViewChange()
    {
        float desiredDistance = 4.0f;
        Vector3 desiredCameraPosition = head.position - transform.forward * desiredDistance;
        transform.position = desiredCameraPosition;
    }

    void LateUpdate()
    {
        if (currentView == CameraView.thirdPersonView)
        {
            AdjustCameraIfItHitsAWall();
        }
    }

    private void AdjustCameraIfItHitsAWall()
    {
        float desiredDistance = 4.0f;
        Vector3 desiredCameraPosition = head.position - transform.forward * desiredDistance;

        if (Physics.Raycast(head.position, -transform.forward, out RaycastHit hit, desiredDistance))
        {
            transform.position = Vector3.Lerp(transform.position, head.position - transform.forward * hit.distance, 20f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, desiredCameraPosition, 20f * Time.deltaTime);
        }
    }

    private void ObjectRaycast()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, minimumInteractionDistanceUnit))
        {
            hitInfo.transform.gameObject.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void ObjectHover()
    {
        Ray ray = new(transform.position, transform.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, minimumInteractionDistanceUnit))
        {
            Debug.Log("Looking for object");
            if (hit.transform.TryGetComponent<IInteractiveObject>(out var interactiveUI))
            {
                Debug.Log("Object found");
                interactiveUI.HandleHoverEnter();
            }
        }
    }
}
