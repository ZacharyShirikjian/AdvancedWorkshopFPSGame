using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5.0f; //temporary speed variable, speed tbd
    //sprint speed option?
    //crouch speed?

    public CharacterController playerController;  //access character controller in player object
    private Vector3 moveDirection = Vector3.zero;

    public float turnSpeed = 2.0f; //temporary cam speed variable
    public float turnAngle = 90.0f;
    private Vector2 rotation = Vector2.zero;
    public Camera playerCamera;
    private float smooth = 5.0f;

    private bool isCrouching;
    private float playerHeight;
    private float crouchHeight = 1.0f;
    private Vector3 playerCenter;
    private Vector3 standPosition;
    private Vector3 crouchPosition;


    // Start is called before the first frame update
    void Start()
    {

        playerController = GetComponent<CharacterController>();
        playerHeight = playerController.height;
        playerCenter = playerController.center;
        standPosition = playerCamera.transform.localPosition;
        crouchPosition = new Vector3(playerCamera.transform.localPosition.x, crouchHeight, playerCamera.transform.localPosition.z);

        playerCamera = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        KeyboardControls();
        MovingCamera();

    }


    void MovingCamera()
    {
        rotation.x += Input.GetAxis("Mouse X") * turnSpeed;
        rotation.y += Input.GetAxis("Mouse Y") * turnSpeed;

        rotation.y = Mathf.Clamp(rotation.y, -turnAngle, turnAngle);

        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
    }


    void KeyboardControls()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = playerCamera.transform.rotation * moveDirection;
        playerController.Move(moveDirection * speed * Time.deltaTime);
        

        if (Input.GetButton("Crouch"))  //set crouch button input
        {
            playerController.height = crouchHeight;
            playerController.center = new Vector3(playerController.center.x, -0.5f, playerController.center.z);
            playerCamera.transform.localPosition = Vector3.Lerp(crouchPosition, standPosition, Time.deltaTime * smooth);
            isCrouching = true;
        }

        if (!Input.GetButton("Crouch") && isCrouching)
        {
            playerController.height = playerHeight;
            playerController.center = playerCenter;
            playerCamera.transform.localPosition = Vector3.Lerp(standPosition, crouchPosition, Time.deltaTime * smooth);
            isCrouching = false;
        }
    }
}
