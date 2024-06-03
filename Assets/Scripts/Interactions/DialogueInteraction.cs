#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueInteraction : MonoBehaviour
{
    [SerializeField]
    private Interactable? interactable;

    [SerializeField]
    private List<string> nodes = new();

    [SerializeField]
    private int index;
    
    public bool changePosition;

    [SerializeField]
    private UnityEvent onFinished = new();

    private void OnEnable()
    {
        if (interactable != null)
        {
            interactable.OnInteract += TriggerDialogue;
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.OnInteract -= TriggerDialogue;
        }
    }

    private void Update()
    {
        if (index == nodes.Count && !DialogueManager.instance.isDialogueRunning && !changePosition)
        {
            changePosition = true;
            onFinished.Invoke();
        }
    }

    private void TriggerDialogue(object sender, EventArgs args)
    {
        DialogueManager.instance.StartDialogue(nodes[index]);
        index++;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(nodes[index]);
        index++;
    }
}