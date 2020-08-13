﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character, IInteractable
{
    public virtual void DeSelect()
    {

    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public virtual void Interact()
    {
        // Not implemented
        Debug.Log("This will open a dialogue with the NPC");
    }

    public virtual void StopInteract()
    {

    }
}