#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/Directions/Movement")]
public class MovementDirection : StageDirection
{
    public AnimationCurve xCurve = new();
    public AnimationCurve yCurve = new();
    public float duration;

    public override void PerformDirection(RectTransform spriteTransform, float performanceTime)
    {
        spriteTransform.anchoredPosition = new Vector2(xCurve.Evaluate(performanceTime / duration), yCurve.Evaluate(performanceTime / duration));
    }

    public override void FinishDirection(RectTransform spriteTransform)
    {
        spriteTransform.anchoredPosition = Vector2.zero;
    }
}