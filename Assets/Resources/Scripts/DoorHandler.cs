using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour, IInteractable
{
    private const string PLAYER_TAG = "Player";
    /// <summary>
    /// A boolean parameter for an Animator component to signify open/close door
    /// </summary>
    private const string P_DOOR_OPENED = "bl_door_opened";
    /// <summary>
    /// A boolean parameter for an Animator component to signify unlocked/locked door
    /// </summary>
    private const string P_DOOR_UNLOCKED = "bl_door_unlocked";


    [SerializeField]
    private bool unlocked;
    [SerializeField]
    private bool opened;
    [SerializeField]
    private bool forever_locked;
    

    private Animator parentAnimator;
    private PlayerBehavior player;

    private void Update()
    {
        if (Input.GetKey(KeyCode.N))
            Debug.Log("Pressing N");

    }
    private void Awake()
    {
        //Due to the small scale of the project, it is assumed animator is supplied
        parentAnimator = GetComponentInParent<Animator>();
        parentAnimator.SetBool(P_DOOR_OPENED, opened);
        parentAnimator.SetBool(P_DOOR_UNLOCKED, unlocked);
    }

    public bool Interact(GameObject sender)
    {
        bool result = OpenClose();
        //Debug.Log("Interacted? " + result);
        return result;
    }

    /// <summary>
    /// Tries to either unlock or lock the closed door depending on its current state
    /// </summary>
    /// <returns>
    /// True if unlocking or locking was successful
    /// </returns>
    public bool UnlockLock()
    {
        //Don't unlock or lock an opened door!
        if (opened)
            return false;

        //unlocked = !unlocked;
        //parentAnimator.SetBool(P_DOOR_UNLOCKED, unlocked);

        return unlocked ? Lock() : Unlock();
        
    }

    /// <summary>
    /// Tries to lock the closed door with the animation
    /// </summary>
    /// <returns>
    /// True if locking was successful
    /// </returns>
    public bool Lock()
    {
        Debug.Log("Lock called");
        if (!opened && unlocked)
        {
            
            unlocked = false;
            parentAnimator.SetBool(P_DOOR_UNLOCKED, unlocked);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Tries to unlock the closed door with the animation
    /// </summary>
    /// <returns>
    /// True if unlocking was successful
    /// </returns>
    public bool Unlock()
    {
        Debug.Log("Unlock called");
        if (!opened && !unlocked)
        {
            unlocked = true;
            parentAnimator.SetBool(P_DOOR_UNLOCKED, unlocked);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Attempts to open or close the door.
    /// </summary>
    /// <returns>
    /// True if door was opened or closed successfully.
    /// </returns>
    private bool OpenClose()
    {
        //Debug.Log("Open/Close called");

        //Don't try if the door is locked
        if (forever_locked || !unlocked)
            return false;

        //Only allow to change open/close door boolean state when it's not locked
        //because it wouldn't make sense to open the door while it's locked
        if (parentAnimator.GetBool(P_DOOR_UNLOCKED))
        {
            opened = !opened;
            parentAnimator.SetBool(P_DOOR_OPENED, opened);
        }
        return true;
    }

}
