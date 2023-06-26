using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private const string OPENABLE_TAG = "OpenableDoor";

    private const string PLAYER_TAG = "Player";
    private const string OPEN_TRIGGER = "tr_open_door";
    private const string CLOSE_TRIGGER = "tr_close_door";
    private const string LOCKED_TRIGGER = "tr_locked_door";
    private const string UNLOCKED_TRIGGER = "tr_unlocked_door";

    [SerializeField]
    private bool locked;
    [SerializeField]
    private bool opened;
    private bool forever_locked;

    private Animator parent_animator;

    private void Awake()
    {
        //Due to the scale of the project, it is assumed animator is supplied
        parent_animator = GetComponentInParent<Animator>();

        //Handle the doors that should not be ever opened by the player
        if (transform.parent.CompareTag(OPENABLE_TAG))
        {
            forever_locked = true;
        }
        //Other openable doors
        else
        {
            forever_locked = false;
        }

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


    private void OnCollisionEnter(Collision collision)
    {
        //Plays the openning door animation
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            Debug.Log("Openning the door");
            OpenClose();
        }
    }

    /// <summary>
    /// Attempts to open or close the door.
    /// </summary>
    /// <returns>
    /// True if door was opened or closed successfully.
    /// </returns>
    private bool OpenClose()
    {
        Debug.Log("Open/Close called");

        //Don't try if the door is locked
        if (forever_locked || locked)
            return false;

        //Close if the door is opened
        if (opened)
        {
            parent_animator.SetTrigger(CLOSE_TRIGGER);
            opened = false;
        }
        //Open if the door is closed
        else
        {
            parent_animator.SetTrigger(OPEN_TRIGGER);
            opened = true;
        }
        //Debug.Log("Current Animation: " + parent_animator.GetCurrentAnimatorClipInfo(0));

        return true;
    }

}
