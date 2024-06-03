#nullable enable
using UnityEngine;

public class RendererAlphaControl : MonoBehaviour
{
    [SerializeField]
    private Renderer? target;
    
    [SerializeField]
    private AnimationCurve alphaCurve = new();

    [SerializeField]
    private LifetimeControl? lifetime;

    private void Update()
    {
        if (target == null || lifetime == null) return;

        var progress = Mathf.Clamp01(1 - lifetime.CurrentLifetime / lifetime.Lifetime);
        var color = target.material.color;
        color.a = alphaCurve.Evaluate(progress);
        target.material.color = color;
    }
}
