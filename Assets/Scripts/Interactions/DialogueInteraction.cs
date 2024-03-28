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
  [SerializeField] private GameObject? qamaar;
  [SerializeField] private GameObject? anubis;
  [SerializeField] private AudioSource music;
  [SerializeField] private AudioClip newMusic;

  private void OnEnable()
  {
    if (interactable != null)
    {
      interactable.OnInteract += OnTriggerDialogue;
      if (dialogueRunner != null) dialogueRunner.onDialogueComplete.AddListener(OnCompleteDialogue);
      if (dialogueRunner != null) dialogueRunner.onNodeStart.AddListener(onNodeStart);
    }
  }

  private void OnDisable()
  {
    if (interactable != null)
    {
      interactable.OnInteract -= OnTriggerDialogue;
      if (dialogueRunner != null) dialogueRunner.onDialogueComplete.RemoveListener(OnCompleteDialogue);
      if (dialogueRunner != null) dialogueRunner.onNodeStart.RemoveListener(onNodeStart);
    }
  }

  private void OnTriggerDialogue(object sender, EventArgs args)
  {
    // set input system to UI so that player cannot perform other actions
    if (inputSubsystem != null) inputSubsystem.PushMap(nameof(Actions.UI));
    // remove hotbar
    if (Hotbar != null) Hotbar.SetActive(false);
    // start dialogue at start node
    if (dialogueRunner != null) dialogueRunner.StartDialogue(dialogueRunner.startNode);
  }

  private void OnCompleteDialogue()
  {
    if (inputSubsystem != null) inputSubsystem.PopMap();
    if (Hotbar != null) Hotbar.SetActive(true);
  }

  private void onNodeStart(string nodeName)
  {
    if (nodeName == "Qamaar" && qamaar != null && anubis != null)
    {
      music.Pause();
      qamaar.SetActive(true);
      anubis.SetActive(true);
      music.clip = newMusic;
      music.Play();
    }
  }
}
