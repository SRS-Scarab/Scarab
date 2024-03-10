#nullable enable
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionIndicator : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private ActionsVariable? actionsVar;
    [SerializeField] private RectTransform? rect;
    [SerializeField] private CameraVariable? camVar;
    [SerializeField] private TextMeshProUGUI? interactionText;
    [SerializeField] private Image? arrowImage;
    [SerializeField] private float alphaTweenDuration = 0.5f;
    [SerializeField] private RectTransform? arrowRect;
    [SerializeField] private float minArrowHeight = 40;
    [SerializeField] private float maxArrowHeight = 70;
    [SerializeField] private AnimationCurve heightCurve = new();
    [SerializeField] private float heightPeriod = 0.5f;
    
    [Header("State")]
    
    [SerializeField] private Interactable? target;
    [SerializeField] private float aliveTime;
    [SerializeField] private bool isDisposed;

    private void Awake()
    {
        if (interactionText != null) interactionText.CrossFadeAlpha(0, 0, true);
        if (arrowImage != null) arrowImage.CrossFadeAlpha(0, 0, true);
    }

    private void Start()
    {
        if (actionsVar != null) actionsVar.Provide().Gameplay.Interact.performed += OnTryInteract;
    }

    private void Update()
    {
        if (rect != null && camVar != null && camVar.Provide() != null && target != null)
        {
            var screenPos = camVar.Provide()!.WorldToScreenPoint(target.transform.position);
            rect.anchoredPosition = new Vector2(screenPos.x - Screen.width / 2f, screenPos.y - Screen.height / 2f);
        }
        if (arrowRect != null)
        {
            aliveTime += Time.deltaTime / heightPeriod;
            var pos = arrowRect.anchoredPosition;
            pos.y = (maxArrowHeight - minArrowHeight) * heightCurve.Evaluate(aliveTime % 1) + minArrowHeight;
            arrowRect.anchoredPosition = pos;
        }
    }

    public void Initialize(Interactable interactable)
    {
        target = interactable;
        if (interactionText != null)
        {
            interactionText.text = target.GetPromptText();
            interactionText.CrossFadeAlpha(1, alphaTweenDuration, false);
        }
        if (arrowImage != null) arrowImage.CrossFadeAlpha(1, alphaTweenDuration, false);
    }

    public void Dispose()
    {
        isDisposed = true;
        if (interactionText != null) interactionText.CrossFadeAlpha(0, alphaTweenDuration, false);
        if (arrowImage != null) arrowImage.CrossFadeAlpha(0, alphaTweenDuration, false);
        Destroy(gameObject, alphaTweenDuration);
    }

    private void OnTryInteract(InputAction.CallbackContext context)
    {
        if (target != null && !isDisposed)
        {
            target.Interact();
        }
    }
}
