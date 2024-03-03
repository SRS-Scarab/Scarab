#nullable enable
using System;
using UnityEngine;
using Yarn.Unity;

public class DialogueInteraction : MonoBehaviour
{
  [Header("Dependencies")]

  [SerializeField] private InputSubsystem? inputSubsystem;
  [SerializeField] private GameObject? Hotbar;

  [SerializeField] private DialogueRunner? dialogueRunner;
  [SerializeField] private Interactable? interactable;

  private void OnEnable()
  {
    if (interactable != null)
    {
      interactable.OnInteract += OnTriggerDialogue;
      if (dialogueRunner != null) dialogueRunner.onDialogueComplete.AddListener(OnCompleteDialogue);
    }
  }

  private void OnDisable()
  {
    if (interactable != null)
    {
      interactable.OnInteract -= OnTriggerDialogue;
      if (dialogueRunner != null) dialogueRunner.onDialogueComplete.RemoveListener(OnCompleteDialogue);
    }
  }

  private void OnTriggerDialogue(object sender, EventArgs args)
  {
    if (inputSubsystem != null) inputSubsystem.PushMap(nameof(Actions.UI));
    if (Hotbar != null) Hotbar.SetActive(false);
    if (dialogueRunner != null) dialogueRunner.StartDialogue(dialogueRunner.startNode);
  }

  private void OnCompleteDialogue()
  {
    if (inputSubsystem != null) inputSubsystem.PopMap();
    if (Hotbar != null) Hotbar.SetActive(true);
  }
}
