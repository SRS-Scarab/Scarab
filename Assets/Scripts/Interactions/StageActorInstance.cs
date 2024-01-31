#nullable enable
using UnityEngine;
using UnityEngine.UI;

public class StageActorInstance : MonoBehaviour
{
    [Header("State")]
    
    [SerializeField] private RectTransform? instanceTransform;
    [SerializeField] private RectTransform? spriteTransform;
    [SerializeField] private Image? spriteImage;
    
    [Header("State")]
    
    [SerializeField] private StageActor? actor;

    public void Initialize(StageActor stageActor)
    {
        actor = stageActor;
        if (spriteImage != null)
        {
            spriteImage.sprite = actor.defaultSprite;
            spriteImage.preserveAspect = true;
        }
    }

    public StageActor? GetActor() => actor;

    public void SetPosition(Vector2 relative)
    {
        if (instanceTransform != null)
        {
            instanceTransform.anchorMin = relative;
            instanceTransform.anchorMax = relative;
            instanceTransform.anchoredPosition = Vector2.zero;
            instanceTransform.sizeDelta = Vector2.zero;
        }
    }

    public void SetOrientation(bool isFacingLeft)
    {
        if (spriteTransform != null) spriteTransform.localScale = new Vector3(isFacingLeft ? -1 : 1, 1, 1);
    }
    
    public void SetExpression(string expressionName)
    {
        if (actor != null && spriteImage != null)
        {
            foreach (var expression in actor.expressions)
            {
                if (expression.expressionName == expressionName) spriteImage.sprite = expression.sprite;
            }
        }
    }
}
