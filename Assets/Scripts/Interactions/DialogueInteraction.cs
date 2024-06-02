#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueInteraction : MonoBehaviour
{

  [SerializeField] private Interactable? interactable;
  [SerializeField] private List<String> nodes;
  private int index;

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

  private void TriggerDialogue(object sender, EventArgs args) {
    DialogueManager.instance.StartDialogue(nodes[index]);
    if(index < nodes.Count-1) index++;
  }


}
