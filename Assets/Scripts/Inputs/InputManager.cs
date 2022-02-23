using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Controls controls;
    Controls.MovementActions movement;
    Controls.MenusActions menu;
    private PlayerController playCon;
    private MouseLook mouseLook;
    private UITest uiScript;

    Vector2 inputVector;
    Vector2 mouseInput;

    private void Start()
    {
        playCon = GameObject.Find("PlayerObject").GetComponent<PlayerController>();
        mouseLook = GameObject.Find("PlayerObject").GetComponentInChildren<MouseLook>();
        uiScript = this.gameObject.GetComponent<UITest>();
    }

    private void Awake()
    {
        controls = new Controls();
        movement = controls.Movement;
        menu = controls.Menus;

        movement.Move.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        movement.Crouch.performed += _ => playCon.OnCrouchPressed();
        movement.Shoot.performed += _ => playCon.OnShootPressed();
        movement.Reload.performed += _ => playCon.OnReloadPressed();


        //movement.Jump.performed += _ => playCon.OnJumpPressed();

        movement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        movement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        //MENU//
        //menu.Select.performed += _ => uiScript.OnSelectPressed();
        menu.Pause.performed += _ => uiScript.PauseGame();

    }

    private void Update()
    {
        playCon.ReceiveInput(inputVector);
        mouseLook.ReceiveInput(mouseInput);
        //uiScript.ReceiveInput();
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
