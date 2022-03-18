using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : StateMachineBehaviour
{



    public float speed = 5.0f; //temporary speed variable, speed tbd

    public GameObject player;
    private CharacterController playerController;  //access character controller in player object
    private Vector3 moveDirection = Vector3.zero;

    private Camera playerCamera;


    public void OnStateEnter()
    {
        playerController = player.GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }



    public void OnStateUpdate()     //FIXME: FLYING BUG (lock Y value)
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = playerCamera.transform.rotation * moveDirection;
        playerController.Move(moveDirection * speed * Time.deltaTime);
    }


}
