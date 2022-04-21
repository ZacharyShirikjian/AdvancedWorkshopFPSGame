using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleInputManager : MonoBehaviour
{
    Controls controls;
    Controls.MenusActions menu;
    //Controls.MovementActions movement;
    private TitleScreen titleScreen;

    Vector2 inputVector;
    //Vector2 mouseInput;

    private void Start()
    {
        titleScreen = GameObject.Find("Canvas").GetComponent<TitleScreen>();
    }

    private void Awake()
    {
        controls = new Controls();
        menu = controls.Menus;

        //movement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        //movement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        InputSystem.DisableDevice(Mouse.current);
        InputSystem.EnableDevice(Mouse.current);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //MENU//
        menu.Start.performed += _ => titleScreen.OpenMenu();
        menu.SwitchPage.performed += _ => titleScreen.SwitchInputPage();
        menu.Navigate.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        menu.Cancel.performed += _ => titleScreen.BackToMenu();

    }

    private void Update()
    {
        //playCon.ReceiveInput(inputVector);
        //mouseLook.ReceiveInput(mouseInput);
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
