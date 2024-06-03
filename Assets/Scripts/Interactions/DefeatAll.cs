#nullable enable
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DefeatAll : MonoBehaviour
{
    [SerializeField]
    private CombatEntity[] entities = Array.Empty<CombatEntity>();

    [SerializeField]
    private bool triggered;

    [SerializeField]
    private UnityEvent onCompleted = new();
    
    private void Update()
    {
        if (!triggered && entities.All(e => e == null))
        {
            onCompleted.Invoke();
            triggered = true;
        }
    }
}
