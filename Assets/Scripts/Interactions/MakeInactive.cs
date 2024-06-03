#nullable enable
using UnityEngine;

public class MakeInactive : MonoBehaviour
{
    private DialogueInteraction dialogueInteraction;
    void Start()
    {
        dialogueInteraction = GetComponent<DialogueInteraction>();
    }
    void Update()
    {
        if(dialogueInteraction.changePosition){
            gameObject.SetActive(false);
        }
    }

}
