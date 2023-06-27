using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //The player object this camera is attached to
    private GameObject player;
    private int rotation_sensitivity = 10;
    private const float ROTATION_LIMIT = 60f;

    //Rotation values for the camera
    private float xRotation = 0f;
    private float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation(ROTATION_LIMIT);
    }

    /// <summary>
    /// Local camera rotation along x-axis (up-down)
    /// and local player rotation along y-axis (left-right).
    /// Should be called in every frame
    /// </summary>
    /// <param name="rotationLimit">
    /// The upper and lower angle bounds of x-axis rotation of the camera
    /// </param>
    private void Rotation(float rotationLimit)
    {
        xRotation += Input.GetAxis("Mouse X") * rotation_sensitivity;
        yRotation -= Input.GetAxis("Mouse Y") * rotation_sensitivity;
        //Limits the x-axis rotation of the camera
        yRotation = Mathf.Clamp(yRotation, -rotationLimit, rotationLimit);

        player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, xRotation, 0f);
        transform.eulerAngles = new Vector3(yRotation, transform.eulerAngles.y, 0f);
    }
}
