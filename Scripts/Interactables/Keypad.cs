using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;

    //this functionj is where we will design our interaction using code
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
