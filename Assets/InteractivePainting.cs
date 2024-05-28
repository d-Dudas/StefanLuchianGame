using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractivePainting : MonoBehaviour, IInteractiveObject
{
    public Transform playerTransform;

    public bool isAchieved = false;

    public GameObject backgroundImage;
    private Keyboard keyboard;

    private bool isCanvasMoving = false;
    Vector3 startPosition = new(Screen.width / 2, Screen.height * 1.5f, 0f);
    Vector3 targetPosition = new(Screen.width / 2, Screen.height / 2, 0f);

    public GameObject frontText;

    private const float hoverTextDuration = 0.2f;
    private float lastHover;

    private bool canHoverTextBeDisplayer = true;
    private void Start()
    {
        keyboard = Keyboard.current;
        frontText.SetActive(false);
        backgroundImage.SetActive(false);
    }

    public void Interact()
    {
        GlobalContext.isAnyPanelOpen = true;

        if (!isCanvasMoving)
        {
            StartCoroutine(OpenCanvasAnimation());
        }
    }

    private IEnumerator OpenCanvasAnimation()
    {
        frontText.SetActive(false);
        backgroundImage.SetActive(true);
        isCanvasMoving = true;
        canHoverTextBeDisplayer = false;

        float animationDuration = 0.7f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);
            backgroundImage.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        isCanvasMoving = false;
    }

    private IEnumerator CloseCanvasAnimation()
    {
        isCanvasMoving = true;

        float animationDuration = 0.7f;
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);
            backgroundImage.transform.position = Vector3.Lerp(targetPosition, startPosition, t);
            yield return null;
        }

        isCanvasMoving = false;
        backgroundImage.SetActive(false);
        canHoverTextBeDisplayer = true;
    }
    public void HandleHoverEnter()
    {
        if(canHoverTextBeDisplayer)
        {
            frontText.SetActive(true);
            lastHover = hoverTextDuration;
        }
    }

    public void HandleHoverExit()
    {
        frontText.SetActive(false);
    }

    private void EnablePlayerMovement()
    {
        GlobalContext.isAnyPanelOpen = false;
    }

    private void Update()
    {
        if (keyboard.escapeKey.wasPressedThisFrame)
        {
            if(backgroundImage.activeSelf)
            {
                Invoke(nameof(EnablePlayerMovement), 0.5f);
                StartCoroutine(CloseCanvasAnimation());
                isAchieved = true;
                GlobalContext.CheckAchievements();
            }
        }

        var dt = Time.deltaTime;
        lastHover -= dt;

        if(lastHover <= 0.0f)
        {

            HandleHoverExit();
        }
    }
}