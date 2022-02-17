using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{


    public float turnSpeed = 2.0f; //temporary cam speed variable
    public float turnAngle = 90.0f;
    private Vector2 rotation = Vector2.zero;
    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x += Input.GetAxis("Mouse X") * turnSpeed;
        rotation.y += Input.GetAxis("Mouse Y") * turnSpeed;

        rotation.y = Mathf.Clamp(rotation.y, -turnAngle, turnAngle);

        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
    }
}
