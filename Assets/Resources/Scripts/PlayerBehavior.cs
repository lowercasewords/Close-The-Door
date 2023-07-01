using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate bool UseHandler();

public class PlayerBehavior : MonoBehaviour, IMovable
{
    //A tag for player's camera retrieval
    private const string CAMERA_TAG = "PlayerCamera";
    private const string DOOR_TAG = "Door";

    /// <summary>
    /// A camera component for player, acting as player's "eyes"
    /// </summary>
    private GameObject eyes;
    /// <summary>
    /// Rigidbody component attached to the player
    /// </summary>
    private Rigidbody rb;


    /// <summary>
    /// Movement speed of the player in all directions
    /// </summary>
    private int speed;
    /// <summary>
    /// Player's movement input on x-axis
    /// </summary>
    private float x_move;
    /// <summary>
    /// Player's movement input on z-axis
    /// </summary>
    private float z_move;

    /// <summary>
    /// Can the player move?
    /// </summary>s
    public bool CanMove { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5;
        CanMove = true;

        eyes = gameObject.transform.GetChild(0).gameObject;
        if(!eyes.CompareTag(CAMERA_TAG))
        {
            eyes = null;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PreInteract();
        }
    }

    // FixedUpdate is called once per fixed frame
    private void FixedUpdate()
    {
        if(CanMove)
            Move();
    }

    private void Move()
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

    /// <summary>
    /// Performs an acting on the object received after raycasting
    /// </summary>
    private void PreInteract()
    {
        Vector3 origin = eyes.transform.position;
        Vector3 direction = eyes.transform.forward;

        RaycastHit[] hitInfos = Physics.RaycastAll(origin, direction, 2f);
        bool? result = null;
        foreach (var hit in hitInfos)
        {
            //Only interact with one thing
            result = hit.collider?.GetComponent<IInteractable>()?.Interact(gameObject);
            /*
            if (result != null && result == true)
            {
                break;
            }
            */
        }
    }

    private IEnumerator<WaitForSeconds> DeleteTimer(GameObject toDelete, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(toDelete);
    }

}
