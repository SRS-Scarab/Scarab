#nullable enable
using UnityEngine;
using UnityEngine.Events;

public class VolumeTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObjectVariable? playerVar;
    
    [SerializeField]
    private UnityEvent onTrigger = new();
    
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.activeInHierarchy && playerVar != null && playerVar.Provide() == other.gameObject)
        {
            onTrigger.Invoke();
        }
    }
}
