#nullable enable
using System;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private new CameraVariable? camera;

    [SerializeField]
    private BillboardAlignment alignment;

    private void Update()
    {
        if (camera == null || camera.Provide() == null) return;
        
        switch (alignment)
        {
            case BillboardAlignment.Axis:
                transform.forward = camera.Provide()!.transform.forward;
                break;
            case BillboardAlignment.XZAxis:
                var forward = camera.Provide()!.transform.forward;
                forward.y = 0;
                transform.forward = forward.normalized;
                break;
            case BillboardAlignment.Radius:
                transform.LookAt(camera.Provide()!.transform.position);
                break;
            default:
                Debug.Log($"Unknown billboard alignment type {alignment}");
                break;
        }
    }
}

public enum BillboardAlignment
{
    Axis,
    XZAxis,
    Radius,
}
