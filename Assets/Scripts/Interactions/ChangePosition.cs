using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePosition : MonoBehaviour
{
    private DialogueInteraction dialogueInteraction;
    public Transform newPosition;
    // Start is called before the first frame update
    void Start()
    {
        dialogueInteraction = GetComponent<DialogueInteraction>();
    }
    void Update()
    {
        if(dialogueInteraction.changePosition){
            gameObject.transform.position = newPosition.position;
        }
    }

}
