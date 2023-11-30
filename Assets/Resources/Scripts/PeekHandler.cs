using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekHandler : MonoBehaviour, IInteractable
{
    private const string PLAYER_CAMERA_TAG = "MainCamera";

    //private Camera peekCamera;
    private bool isPeaking;

    private void Awake()
    {
        isPeaking = false;
        //peekCamera = gameObject.AddComponent<Camera>();
        //peekCamera.targetDisplay = 1;
        //peekCamera.enabled = false;
    }

    public bool Interact(GameObject sender)
    {
        Debug.Log("Interacted"); 
        return PeekBehavior(sender);
    }


    /// <summary>
    /// Allows the player to peek through the door
    /// </summary>
    public bool Peek(GameObject player
    {
        Camera player_camera = player.GetComponentInChildren<Camera>();

        //Determines if the correct camera was picked
        if (!player_camera.CompareTag(PLAYER_CAMERA_TAG))
            return false;

        //Player was already peaking, so stop!
        if (isPeaking)
        {
            Debug.Log("Peeking is stopped");
            //player_camera.enabled = true;
            //peekCamera.enabled = false;
            player_camera.transform.position = Vector3.zero;// player.transform.position;
            //player_camera.transform.rotation = Quaternion;// player.transform.rotation;
            

            player_camera.GetComponent<IMovable>().CanMove = true;
            player.GetComponent<IMovable>().CanMove = true;
        }
        //Player just enteres the peaking state
        else
        {
            //player_camera.enabled = false;
            //peekCamera.enabled = true;
            player_camera.transform.position = gameObject.transform.position;
            //player_camera.transform.rotation = gameObject.transform.rotation;
            
            player.GetComponent<IMovable>().CanMove = false;
            player_camera.GetComponent<IMovable>().CanMove = false;
        }

        isPeaking = !isPeaking;

        return true;

    }
}
