using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    //private const string OPENABLE_TAG = "OpenableDoor";

    private const string PLAYER_TAG = "Player";
    private const string OPEN_STATE = "OpenDoor";
    private const string CLOSE_STATE = "CloseDoor";
    private const string OPEN_TRIGGER = "tr_open_door";
    private const string CLOSE_TRIGGER = "tr_close_door";
    private const string LOCKED_TRIGGER = "tr_locked_door";
    private const string UNLOCKED_TRIGGER = "tr_unlocked_door";

    [SerializeField]
    private bool locked;
    [SerializeField]
    private bool opened;
    [SerializeField]
    private bool forever_locked;

    private Animator parent_animator;

    private void Awake()
    {
        //Due to the scale of the project, it is assumed animator is supplied
        parent_animator = GetComponentInParent<Animator>();

        ////Handle the doors that should not be ever opened by the player
        //if (transform.parent.CompareTag(OPENABLE_TAG))
        //{
        //    forever_locked = false;
        //}
        ////Other openable doors
        //else
        //{
        //    forever_locked = true;
        //}

        
        //Handling the intitial state defined in inspector
        if(opened)
        {
            parent_animator.SetTrigger(OPEN_TRIGGER);
        }
        else
        {
            parent_animator.SetTrigger(CLOSE_TRIGGER);
        }
    }

    /// <summary>
    /// Attempts to open or close the door.
    /// </summary>
    /// <returns>
    /// True if door was opened or closed successfully.
    /// </returns>
    public bool OpenClose()
    {
        Debug.Log("Open/Close called");

        //Debug.Log("Is locked: " + locked);
        //Debug.Log("Is forever locked: " + forever_locked);

        //Don't try if the door is locked
        if (forever_locked || locked)
            return false;

        parent_animator.ResetTrigger(CLOSE_TRIGGER);
        parent_animator.ResetTrigger(OPEN_TRIGGER);

        //Debug.Log("is not locked");
        //Close if the door is opened
        if (parent_animator)
        {
            if (parent_animator.GetCurrentAnimatorStateInfo(0).IsName(OPEN_STATE))
            {
                Debug.Log("Closing...");
                parent_animator.SetTrigger(CLOSE_TRIGGER);
                
                opened = false;
            }
            //Open if the door is closed
            else if(parent_animator.GetCurrentAnimatorStateInfo(0).IsName(CLOSE_STATE))
            {
                Debug.Log("Openning...");
                parent_animator.SetTrigger(OPEN_TRIGGER);

                opened = true;
            }
        }

        return true;
    }

}
