using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class OpenableDoor : MonoBehaviour, IInteractiveObject
{
    public Transform playerTransform;
    private bool isDoorOpened = false;

    public int requiredLevel = 0;

    private const int closedDoorAngle = 0;
    private const int openedDoorAngle = 90;
    private const float doorOpeningAnimationDuration = 0.5f;

    private const float hoverTextDuration = 0.2f;
    private float lastHover;

    public GameObject frontText;
    // public TextMeshPro frontText;
    public GameObject backText;
    
    void Start()
    {
        frontText.SetActive(false);
        backText.SetActive(false);
    }

    void Update()
    {
        lastHover -= Time.deltaTime;

        if(lastHover <= 0.0f)
        {
            HandleHoverExit();
        }
    }

    public void Interact()
    {
        if(GlobalContext.currentLevel < requiredLevel)
        {
            frontText.GetComponent<TextMeshProUGUI> ().text = "Required level: " + requiredLevel;
            backText.GetComponent<TextMeshProUGUI> ().text = "Required level: " + requiredLevel;
            return;
        }
        int openedDoorRelativeAngle = openedDoorAngle * (ShouldOpenClockwise() ? -1 : 1);
        StartCoroutine(ChangeDoorAngle(isDoorOpened ? closedDoorAngle : openedDoorRelativeAngle));
        isDoorOpened = !isDoorOpened;
    }

    private bool ShouldOpenClockwise()
    {
        Vector3 toPlayer = playerTransform.position - transform.position;
        float dot = Vector3.Dot(transform.forward, toPlayer);

        return dot > 0;
    }

    private IEnumerator ChangeDoorAngle(int angle)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

        float elapsedTime = 0;

        while (elapsedTime < doorOpeningAnimationDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / doorOpeningAnimationDuration);
            yield return null;
        }
    }

    public void HandleHoverEnter()
    {
        if(ShouldOpenClockwise())
        {
            frontText.SetActive(true);
        }
        else
        {
            backText.SetActive(true);
        }
        
        lastHover = hoverTextDuration;
    }

    public void HandleHoverExit()
    {
        frontText.SetActive(false);
        backText.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI> ().text = "Press Enter";
        backText.GetComponent<TextMeshProUGUI> ().text = "Press Enter:";
    }
}
