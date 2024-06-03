#nullable enable
using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private InteractionSubsystem? subsystem;

    [Header("Parameters")]
    [SerializeField]
    private float interactionRange = 1;

    [SerializeField]
    private int interactionPriority;

    [SerializeField]
    private string promptText = string.Empty;

    [SerializeField]
    private UnityEvent onInteract = new();

    public event EventHandler? OnInteract;

    private void Awake()
    {
        var trigger = GetComponent<SphereCollider>();
        if (trigger == null)
        {
            trigger = gameObject.AddComponent<SphereCollider>();
        }

        trigger.radius = interactionRange;
        trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (subsystem == null) return;
        if (other.attachedRigidbody != null && other.attachedRigidbody.gameObject == subsystem.GetPlayerObject()) subsystem.AddInteractable(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (subsystem == null) return;
        if (other.attachedRigidbody != null && other.attachedRigidbody.gameObject == subsystem.GetPlayerObject()) subsystem.RemoveInteractable(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (subsystem == null) return;
        if (other.attachedRigidbody != null && other.attachedRigidbody.gameObject == subsystem.GetPlayerObject()) subsystem.AddInteractable(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (subsystem == null) return;
        if (other.attachedRigidbody != null && other.attachedRigidbody.gameObject == subsystem.GetPlayerObject()) subsystem.RemoveInteractable(this);
    }

    public int GetInteractionPriority() => interactionPriority;

    public string GetPromptText() => promptText;

    public void SetPromptText(string newText) => promptText = newText;

    public void Interact()
    {
        if (!gameObject.activeInHierarchy) return;
        if (subsystem != null) subsystem.RemoveInteractable(this);
        onInteract.Invoke();
        OnInteract?.Invoke(this, EventArgs.Empty);
    }
}