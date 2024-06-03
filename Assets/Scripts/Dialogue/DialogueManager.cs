using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class DialogueManager : MonoBehaviour
{    
    [Header("Dependencies")]
    [SerializeField] private InputSubsystem? inputSubsystem;
    [SerializeField] private GameObject? Sidebar;
    [SerializeField] private GameObject? Healthbar;
    [SerializeField] private GameObject? Manabar;
    [SerializeField] private DialogueRunner? dialogueRunner;
    
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void StartDialogue(string node)
    {
        dialogueRunner.onDialogueComplete.AddListener(OnCompleteDialogue);
        // set input system to UI so that player cannot perform other actions
        if (inputSubsystem != null) inputSubsystem.PushMap(nameof(Actions.UI));
        // remove sidebar
        if (Sidebar != null) Sidebar.SetActive(false);
        // remove healthbar
        if (Healthbar != null) Healthbar.SetActive(false);
        // remove manabar
        if (Manabar != null) Manabar.SetActive(false);
        // start dialogue at start node
        if (dialogueRunner != null) dialogueRunner.StartDialogue(node);
    }

    public void OnCompleteDialogue()
    {
        dialogueRunner.onDialogueComplete.RemoveListener(OnCompleteDialogue);
        if (inputSubsystem != null) inputSubsystem.PopMap();
        if (Sidebar != null) Sidebar.SetActive(true);
        if (Healthbar != null) Healthbar.SetActive(true);
        if (Manabar != null) Manabar.SetActive(true);
        
    }
}
