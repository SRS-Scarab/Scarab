#nullable enable
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private InteractionSubsystem? subsystem;
    [SerializeField] private GameObject? indicatorPrefab;
    
    [Header("State")]
    
    [SerializeField] private Interactable? previous;
    [SerializeField] private InteractionIndicator? indicator;

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
