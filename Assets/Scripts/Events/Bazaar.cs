using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Bazaar : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    [SerializeField] private string node;
    void Start(){
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player") {
            dialogueRunner.Stop();
            dialogueRunner.StartDialogue(node);
        }
    }
}
