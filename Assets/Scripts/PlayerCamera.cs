using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //The player object this camera is attached to
    private GameObject player;
    private int rotation_sensitivity = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float x_axis = Input.GetAxis("Mouse X");
        float y_axis = Input.GetAxis("Mouse Y");
        player.transform.Rotate(Vector3.up * x_axis * rotation_sensitivity);
    }


}
