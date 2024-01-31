using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    [SerializeField]
    private GameObject Switch;
    private bool SwitchUp;
    protected override void Interact()
    {
        SwitchUp = !SwitchUp;
        Switch.GetComponent<Animator>().SetBool("IsFlipped", SwitchUp);
    }
}
