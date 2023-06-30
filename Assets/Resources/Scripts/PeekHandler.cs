using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekHandler : MonoBehaviour, IInteractable
{
    private const string PLAYER_CAMERA_TAG = "PlayerCamera";

    private Camera peekCamera;
    private bool isPeaking;

    private void Awake()
    {
        isPeaking = false;
        peekCamera = GetComponent<Camera>();
    }

    public bool Interact(GameObject sender)
    {
        return Peek(sender);
    }

    public bool Peek(GameObject player)
    {
        Camera player_camera = player.GetComponentInChildren<Camera>();
        //Determines if the correct camera was picked
        if (!player_camera.CompareTag(PLAYER_CAMERA_TAG))
            return false;

        //Player was already peaking
        if (isPeaking)
        {
            Debug.Log("Peeking is stopped");
            peekCamera.enabled = false;

            player_camera.GetComponent<IMovable>().CanMove = true;
            player.GetComponent<IMovable>().CanMove = true;
        }
        //Player just enteres the peaking state
        else
        {
            //player_camera.enabled = true;
            peekCamera.enabled = true;

            player.GetComponent<IMovable>().CanMove = false;
            player_camera.GetComponent<IMovable>().CanMove = false;
        }

        isPeaking = !isPeaking;

        return true;

    }
}
