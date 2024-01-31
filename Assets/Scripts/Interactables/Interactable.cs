using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    //message displayed to player when their loooking at an interactable
    [SerializeField]
    public string promptMessage;
    public void baseInteract()
    {
        if(useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }
    
    protected virtual void Interact()
    {
        //No code goes here
        //Template function to be overridden by subclasses
    }
}
