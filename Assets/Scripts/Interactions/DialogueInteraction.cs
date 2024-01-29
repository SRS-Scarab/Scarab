#nullable enable
using System;
using UnityEngine;
using Yarn.Unity;

public class DialogueInteraction : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private DialogueRunner? dialogueRunner;
    [SerializeField] private Interactable? interactable;

    private void OnEnable()
    {
        if (interactable != null) interactable.OnInteract += OnTriggerDialogue;
    }

    private void OnDisable()
    {
        if (interactable != null) interactable.OnInteract -= OnTriggerDialogue;
    }

    private void OnTriggerDialogue(object sender, EventArgs args)
    {
        if (dialogueRunner != null) dialogueRunner.StartDialogue(dialogueRunner.startNode);
    }
}
