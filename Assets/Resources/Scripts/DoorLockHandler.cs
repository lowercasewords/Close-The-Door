using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorLockHandler : MonoBehaviour, IInteractable
{
    /// <summary>
    /// The door this lock is handled by
    /// </summary>
    private DoorHandler parentDoor;
    /// <summary>
    /// A Keycode to trigger an interaction
    /// </summary>
    private const KeyCode INTERACT_KEY = KeyCode.E;
    /// <summary>
    /// Max frames required to be completed to finish a continuous interaction
    /// </summary>
    private const float FRAMES_TO_INTERACT = 600;
    /// <summary>
    /// Incrementing frame timer for the continous interaction
    /// </summary>
    private float timer;
    /// <summary>
    /// The gameobject that continuously interacts with the script
    /// </summary>
    private GameObject interactionSender;

    /// <summary>
    /// The UI loading element playing while locking the door
    /// </summary>
    private Image lockUIImage;

    private void Awake()
    {
        lockUIImage = GameObject.Find("DoorLockBar")?.GetComponent<Image>();
        if(lockUIImage)
            lockUIImage.fillAmount = 0;
        parentDoor = gameObject.GetComponentInParent<DoorHandler>();
        interactionSender = null;
        timer = 0;
    }

    private void Update()
    {
        InteractLong();
    }

    private void InteractLong()
    {
        //SHOULD BE ALLOWING PLAYER AND ITS CAMERA TO MOVE ONCE
        //INTERACTION ENDED, YOU CAN USE GAMEOBJECT FIELD INSTEAD
        //OF BOOLEAN TO DETECT AND HANDLE SUCH THING

        //Try to unlock the door 
        if (interactionSender != null && Input.GetKey(INTERACT_KEY))
        {
            if (lockUIImage != null)
                lockUIImage.fillAmount = timer / FRAMES_TO_INTERACT;

            if (++timer > FRAMES_TO_INTERACT)
            {
                if (lockUIImage != null)
                    lockUIImage.fillAmount = 0;
                parentDoor.Lock();
                CanPlayerMove(true);
                interactionSender = null;
                timer = 0;
            }
        }
        //Stop continuous interaction
        else if (interactionSender != null && Input.GetKeyUp(INTERACT_KEY))
        {
            if (lockUIImage != null)
                lockUIImage.fillAmount = 0;
            timer = 0;
            CanPlayerMove(true);
            interactionSender = null;
        }
    }

    public bool Interact(GameObject sender)
    {
        //Door was unlocked (instantly)
        if (parentDoor.Unlock())
        {
            Debug.Log("Successfully unlocked the door!");
        }
        else
        {
            //Allows the continuous interaction to start since the locking the door
            //requires the button to be pressed continuously!
            interactionSender = sender;
            CanPlayerMove(false);
        }
        return true;
    }

    /// <summary>
    /// Sets the ability for the player and their camera to move
    /// </summary>
    /// <param name="canMove">
    /// True if player can move, false otherwise
    /// </param>
    private void CanPlayerMove(bool canMove)
    {
        if (interactionSender.GetComponent<PlayerBehavior>())
            interactionSender.GetComponent<PlayerBehavior>().CanMove = canMove;
        if (interactionSender.GetComponentInChildren<PlayerCamera>())
            interactionSender.GetComponentInChildren<PlayerCamera>().CanMove = canMove;
    }
    private IEnumerator Timer(float seconds)
    {
        Debug.Log("Timer started");

        yield return new WaitForSeconds(seconds);

        Debug.Log("Timer stopped");
    }
}
