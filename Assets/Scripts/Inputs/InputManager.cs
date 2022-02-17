using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controls controls;
    Controls.MovementActions movement;
    Controls.MainMenuActions mainMenu;
    [SerializeField] PlayerController playCon;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] UITest uiScript;

    Vector2 inputVector;
    Vector2 mouseInput;

    private void Awake()
    {
        controls = new Controls();
        movement = controls.Movement;
        mainMenu = controls.MainMenu;


        //MOVEMENT//
        movement.Move.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        movement.Crouch.performed += _ => playCon.OnCrouchPressed();
        movement.Shoot.performed += _ => playCon.OnShootPressed();


        //movement.Jump.performed += _ => playCon.OnJumpPressed();

        movement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        movement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        //MENU//
        mainMenu.Select.performed += _ => uiScript.OnSelect();
        mainMenu.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        mainMenu.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
    }

    private void Update()
    {
        playCon.ReceiveInput(inputVector);
        mouseLook.ReceiveInput(mouseInput);
        uiScript.ReceiveInput(mouseInput);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
