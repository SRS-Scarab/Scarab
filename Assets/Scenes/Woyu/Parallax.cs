using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private CameraVariable camVar;
    
    [Header("Parameters")]
    
    [SerializeField] [Range(-1, 1)] private float depth = 0;

    private void Update()
    {
        if (camVar.value != null)
        {
            Vector3 position = transform.position;
            position.x = camVar.value.transform.position.x * depth;
            transform.position = position;
        }
    }
}
