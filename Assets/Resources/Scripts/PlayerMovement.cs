using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private int speed;
    float x_move;
    float z_move;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 5;
    }

    // Update is called once per frame
    private void Update()
    {
        
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
