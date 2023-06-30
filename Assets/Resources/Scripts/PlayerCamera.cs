using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour, IMovable
{
    //The player object this camera is attached to
    private GameObject player;
    private const int rotation_sensitivity = 10;
    private const float ROTATION_LIMIT = 60f;
    private const float yStandPos = 0.89f;

    /// <summary>
    /// Rotation value for the camera
    /// </summary>
    private float xRotation, yRotation;

    /// <summary>
    /// Can the camera be rotated or moved?
    /// </summary>
    public bool CanMove { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        xRotation = 0f;
        yRotation = 0f;
    }

    private void Awake()
    {
        CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent.gameObject;
        ResetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
            Rotation(ROTATION_LIMIT);
    }

    /// <summary>
    /// Resets the position of the camera local to its player
    /// </summary>
    public void ResetPosition() => transform.localPosition.Set(0f, yStandPos, 0f);


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
