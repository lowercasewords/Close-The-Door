using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes the basic behavior of the player, including its movement and interaction with
/// interactable objects
/// </summary>
public class PlayerBehavior : MonoBehaviour, IMovable
{
    //A tag for player's camera retrieval
    private const string CAMERA_TAG = "PlayerCamera";
    private const string DOOR_TAG = "Door";
    public const KeyCode INTERACT_KEY = KeyCode.E;

    private delegate void Interaction();
    /// <summary>
    /// A camera component for player, acting as player's "eyes"
    /// </summary>
    private GameObject eyes;
    /// <summary>
    /// Rigidbody component attached to the player
    /// </summary>
    private Rigidbody rb;
    /// <summary>
    /// The instance that player is currently interacting with
    /// </summary>
    private Collider interactableCollider;

    private float debugCount = 0;
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
        //Tries to interact with an instant interactable object
        if (Input.GetKeyDown(INTERACT_KEY))
        {
            Debug.Log("Pressed down!");

            //Tries to find the interactable object
            if(FindInteractable(out RaycastHit raycastInfo))
                raycastInfo.collider?.GetComponent<IInteractable>()?.Interact(gameObject);
        }
        //Tries to continuously interact with an object
        else if(Input.GetKey(INTERACT_KEY))
        {
            Debug.Log("Pressing...");


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

        Vector3 forward = transform.forward * z_move;
        Vector3 right = transform.right * x_move;

        rb.velocity = (forward + right).normalized;
    }

    
    /// <summary>
    /// Performs an acting on the object received after raycasting
    /// </summary>
    private bool FindInteractable(out RaycastHit hit)
    {
        Vector3 origin = eyes.transform.position;
        Vector3 direction = eyes.transform.forward;

        //Casts the ray to find the interactable
        return Physics.Raycast(origin, direction, out hit, 1f);
        

        /*
        RaycastHit[] hitInfos = Physics.RaycastAll(origin, direction, 2f);
        bool result = false;
        
        foreach (RaycastHit hit in hitInfos)
        {
            //Only interact with one thing
            result = (null == hit.collider?.GetComponent<IInteractable>()?.Interact(gameObject));
            if (!result)
                break;
        }
        hits = hitInfos;
        return result;

        */
    }

    private IEnumerator<WaitForSeconds> DeleteTimer(GameObject toDelete, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(toDelete);
    }

}
