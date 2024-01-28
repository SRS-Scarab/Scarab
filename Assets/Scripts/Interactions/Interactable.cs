#nullable enable
using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] public InteractionSubsystem? subsystem;

    [Header("Parameters")]
    
    [SerializeField] public float interactionRange = 1;
    [SerializeField] public int interactionPriority;
    [SerializeField] public string promptText = string.Empty;

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

    public void Interact()
    {
        if (subsystem != null) subsystem.RemoveInteractable(this);
        OnInteract?.Invoke(this, EventArgs.Empty);
    }
}