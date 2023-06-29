using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate bool UseHandler();

public class PlayerBehavior : MonoBehaviour
{
    //A tag for player's camera retrieval
    private const string CAMERA_TAG = "PlayerCamera";
    private const string DOOR_TAG = "Door";

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
            PreInteract();
        }
    }

    /// <summary>
    /// Performs an acting on the object received after raycasting
    /// </summary>
    private void PreInteract()
    {
        Vector3 origin = eyes.transform.position;
        Vector3 direction = eyes.transform.forward;
        RaycastHit rayInfo;


        bool hit = Physics.Raycast(origin, direction, out rayInfo);

        Collider hit_collider = rayInfo.collider;

        if (hit_collider != null)
            hit_collider.GetComponent<IInteractable>()?.Interact();
    }

    private IEnumerator<WaitForSeconds> DeleteTimer(GameObject toDelete, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(toDelete);
    }

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
