#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

public class StageActorInstance : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private RectTransform? instanceTransform;
    [SerializeField] private RectTransform? spriteTransform;
    [SerializeField] private Image? spriteImage;
    
    [Header("Parameters")]
    
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeAlpha;
    
    [Header("State")]
    
    [SerializeField] private StageActor? actor;
    [SerializeField] private StageDirection? currentDirection;
    [SerializeField] private float directionTime;

    public void Initialize(StageActor stageActor)
    {
        actor = stageActor;
        if (spriteImage != null)
        {
            spriteImage.sprite = actor.defaultSprite;
            spriteImage.preserveAspect = true;
            spriteImage.CrossFadeAlpha(fadeAlpha, 0, true);
        }
    }

    public StageActor? GetActor() => actor;

    public void FadeIn()
    {
        if (spriteImage != null) spriteImage.CrossFadeAlpha(1, fadeDuration, false);
    }

    public void FadeOut()
    {
        if (spriteImage != null) spriteImage.CrossFadeAlpha(fadeAlpha, fadeDuration, false);
    }

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

    public void ResetExpression()
    {
        if (actor != null && spriteImage != null) spriteImage.sprite = actor.defaultSprite;
    }

    public void PerformDirection(StageDirection direction)
    {
        if (spriteTransform != null && currentDirection != null) currentDirection.FinishDirection(spriteTransform);
        currentDirection = direction;
        directionTime = 0;
    }

    public void ResetDirection()
    {
        if (spriteTransform != null && currentDirection != null) currentDirection.FinishDirection(spriteTransform);
        currentDirection = null;
    }

    private void Update()
    {
        if (spriteTransform != null && currentDirection != null)
        {
            directionTime += Time.deltaTime;
            currentDirection.PerformDirection(spriteTransform, directionTime);
        }
    }
}
