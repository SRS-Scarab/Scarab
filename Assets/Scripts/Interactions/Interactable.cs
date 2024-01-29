#nullable enable
using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private InteractionSubsystem? subsystem;

    [Header("Parameters")]
    
    [SerializeField] private float interactionRange = 1;
    [SerializeField] private int interactionPriority;
    [SerializeField] private string promptText = string.Empty;

    public event EventHandler? OnInteract;

    private void Awake()
    {
        var trigger = GetComponent<CircleCollider2D>();
        if (trigger == null) trigger = gameObject.AddComponent<CircleCollider2D>();
        trigger.radius = interactionRange;
        trigger.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (subsystem == null) return;
        if (other.gameObject == subsystem.GetPlayerObject()) subsystem.AddInteractable(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (subsystem == null) return;
        if (other.gameObject == subsystem.GetPlayerObject()) subsystem.RemoveInteractable(this);
    }

    public int GetInteractionPriority() => interactionPriority;
    
    public string GetPromptText() => promptText;

    public void SetPromptText(string newText) => promptText = newText;

    public void Interact()
    {
        if (subsystem != null) subsystem.RemoveInteractable(this);
        OnInteract?.Invoke(this, EventArgs.Empty);
    }
}