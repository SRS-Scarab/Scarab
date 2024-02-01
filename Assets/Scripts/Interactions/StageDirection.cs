#nullable enable
using UnityEngine;

public abstract class StageDirection : ScriptableObject
{
    public string directionName = string.Empty;
    
    public abstract void PerformDirection(RectTransform spriteTransform, float performanceTime);

    public abstract void FinishDirection(RectTransform spriteTransform);
}
