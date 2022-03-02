using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //MENU//
        //menu.Select.performed += _ => uiScript.OnSelectPressed();
        menu.Navigate.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        menu.Cancel.performed += _ => titleScreen.CloseCredits();

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
