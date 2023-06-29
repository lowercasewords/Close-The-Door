using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockHandler : MonoBehaviour, IInteractable
{
    private DoorHandler parentDoor;

    private void Awake()
    {
        parentDoor = gameObject.GetComponentInParent<DoorHandler>();
    }

    public bool Interact()
    {
        return parentDoor.UnlockLock();
    }
}
