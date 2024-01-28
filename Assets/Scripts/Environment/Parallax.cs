#nullable enable
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private CameraVariable? camVar;
    
    [Header("Parameters")]
    
    [SerializeField] [Range(-1, 1)] private float depth = 0;

    private void Update()
    {
        if (camVar == null) return;
        var cam = camVar.Provide();
        if (cam == null) return;
        var position = transform.position;
        position.x = cam.transform.position.x * depth;
        transform.position = position;
    }
}
