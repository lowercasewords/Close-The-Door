using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //A tag for player's camera retrieval
    private const string CAMERA_TAG = "PlayerCamera";
    //A camera for player, acting as player's "eyes"
    private GameObject eyes;
    ////A ray coming from player's "eyes"
    //private Physics vision;
    private Rigidbody rb;
    private int speed;
    //Player's movement input on x-axis
    float x_move;
    //Player's movement input on z-axis
    float z_move;

    // Start is called before the first frame update
    void Start()
    {
        eyes = gameObject.transform.GetChild(0).gameObject;
        if(!eyes.CompareTag(CAMERA_TAG))
        {
            eyes = null;
        }
        rb = GetComponent<Rigidbody>();
        speed = 5;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    /// <summary>
    /// Performs an acting on the object received after raycasting
    /// </summary>
    private void Interact()
    {
        Vector3 origin = eyes.transform.position;
        Vector3 direction = eyes.transform.forward;

        //Debug.Log("Rotation: " + direction);
        Debug.DrawRay(origin, direction, Color.red);

        bool hit = Physics.Raycast(origin, direction, out RaycastHit ray_info);
        DoorHandler doorHandler = ray_info.collider.gameObject.GetComponent<DoorHandler>();

        //Tries to open/close the door if possible
        if (hit && doorHandler)
        {
            doorHandler.OpenClose();
        }
    }

    //private IEnumerator<WaitForSeconds> DeleteTimer(Component toDelete, float time)
    //{
    //    yield return new WaitForSeconds(time);

    //    Destroy(toDelete);
    //}

    // FixedUpdate is called once per fixed frame
    private void FixedUpdate()
    {
        x_move = Input.GetAxis("Horizontal");
        z_move = Input.GetAxis("Vertical");

        /*
        Vector3 up = new Vector3(0f, rb.velocity.y, 0f);

        rb.velocity = (transform.forward * z_move + transform.right * x_move).normalized * movement_speed + up;
        */

        Vector3 forward = transform.forward * z_move;
        Vector3 right = transform.right * x_move;

        rb.velocity = (forward + right).normalized;
    }
}
