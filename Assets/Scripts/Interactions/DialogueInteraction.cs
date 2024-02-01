#nullable enable
using System;
using UnityEngine;
using Yarn.Unity;

public class DialogueInteraction : MonoBehaviour
{
    [Header("Dependencies")]

    [SerializeField] private InputSubsystem? inputSubsystem;
    [SerializeField] private DialogueRunner? dialogueRunner;
    [SerializeField] private Interactable? interactable;
    [SerializeField] private GameObject? characters;
    public void Start()
    {
        if (characters != null) characters.SetActive(false);
    }

    private void OnEnable()
    {
        if (interactable != null)
        {
            interactable.OnInteract += OnTriggerDialogue;
            if (dialogueRunner != null) dialogueRunner.onDialogueComplete.AddListener(OnDialogueComplete);
        }
    }

    private void OnDisable()
    {
        if (interactable != null)
        {
            interactable.OnInteract -= OnTriggerDialogue;
            if (dialogueRunner != null) dialogueRunner.onDialogueComplete.RemoveListener(OnDialogueComplete);
        }
    }

    private void OnTriggerDialogue(object sender, EventArgs args)
    {
        if (inputSubsystem != null) inputSubsystem.PushMap(nameof(Actions.UI));
        if (dialogueRunner != null)
        {
            dialogueRunner.StartDialogue(dialogueRunner.startNode);
            if (characters != null) characters.SetActive(true);
        }
    }

    private void OnDialogueComplete()
    {
        if (inputSubsystem != null) inputSubsystem.PopMap();
        if (characters != null) characters.SetActive(false);
    }
}
