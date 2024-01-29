#nullable enable
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] public InteractionSubsystem? subsystem;
    [SerializeField] public GameObject? indicatorPrefab;
    
    [Header("State")]
    
    [SerializeField] public Interactable? previous;
    [SerializeField] public InteractionIndicator? indicator;

    private void Update()
    {
        if (subsystem == null || indicatorPrefab == null) return;
        var interactable = subsystem.GetInteractable();
        if (interactable != previous)
        {
            previous = interactable;
            if (indicator != null)
            {
                indicator.Dispose();
                indicator = null;
            }
            if (interactable != null)
            {
                indicator = Instantiate(indicatorPrefab, transform).GetComponent<InteractionIndicator>();
                indicator.Initialize(interactable);
            }
        }
    }
}
