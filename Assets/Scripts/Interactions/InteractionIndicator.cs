#nullable enable
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractionIndicator : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] public RectTransform? rect;
    [SerializeField] public CameraVariable? camVar;
    [SerializeField] public TextMeshProUGUI? interactionText;
    [SerializeField] public Image? arrowImage;
    [SerializeField] public float alphaTweenDuration = 0.5f;
    [SerializeField] public RectTransform? arrowRect;
    [SerializeField] public float minArrowHeight = 30;
    [SerializeField] public float maxArrowHeight = 60;
    [SerializeField] public AnimationCurve heightCurve = new();
    [SerializeField] public float heightPeriod = 0.5f;
    
    [Header("State")]
    
    [SerializeField] public Interactable? target;
    [SerializeField] public float aliveTime;
    [FormerlySerializedAs("invalid")] [SerializeField] public bool isDisposed;

    private void Awake()
    {
        if (interactionText != null) interactionText.CrossFadeAlpha(0, 0, true);
        if (arrowImage != null) arrowImage.CrossFadeAlpha(0, 0, true);
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
        if (target != null && Input.GetKeyDown(KeyCode.E) && !isDisposed)
        {
            target.Interact();
        }
    }

    public void Initialize(Interactable interactable)
    {
        target = interactable;
        if (interactionText != null)
        {
            interactionText.text = target.promptText;
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
}
