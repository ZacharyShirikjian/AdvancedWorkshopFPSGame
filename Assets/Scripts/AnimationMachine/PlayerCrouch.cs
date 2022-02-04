using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : StateMachineBehaviour
{

    public GameObject player;
    private CharacterController playerController;
    private Camera playerCamera;
    private float crouchSpeed = 2.0f;
    private float crouchHeight; // value tbd
    private float smooth = 3.0f;

    private Vector3 moveDirection = Vector3.zero;
    public Vector3 standPosition;
    public Vector3 crouchPosition;
    public float standHeight;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)


    public void OnStateEnter()
    {
        playerCamera = Camera.main;
        playerController = player.GetComponent<CharacterController>();

        standPosition = playerController.center;
        standHeight = playerController.center.y;
        crouchPosition = new Vector3(playerController.center.x, -0.5f, playerController.center.z);

        playerController.height = crouchHeight;
        playerController.center = crouchPosition;

        playerCamera.transform.localPosition = Vector3.Lerp(standPosition, crouchPosition, Time.deltaTime * smooth);

    }


    public void OnStateExit()
    {
        playerCamera.transform.localPosition = Vector3.Lerp(crouchPosition, standPosition, Time.deltaTime * smooth);
        playerController.height = standHeight;
        playerController.center = standPosition;
    }



    public void OnStateUpdate()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = playerCamera.transform.rotation * moveDirection;
        playerController.Move(moveDirection * crouchSpeed * Time.deltaTime);
    }
}
