using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Controls controls;
    Controls.MovementActions movement;
    Controls.MenusActions menu;
    private PlayerController playCon;
    private MouseLook mouseLook;
    private UITest uiScript;
    private JukeboxScript jukeboxRef;
    private PlayerInteract playInteract;

    Vector2 mousePos;

    Vector2 inputVector;
    Vector2 mouseInput;

    private void Start()
    {
        playCon = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playInteract = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerInteract>();
        mouseLook = GameObject.FindWithTag("Player").GetComponentInChildren<MouseLook>();
        uiScript = this.gameObject.GetComponent<UITest>();
        jukeboxRef = GameObject.FindWithTag("Jukebox").GetComponent<JukeboxScript>();
    }

    private void Awake()
    {
        controls = new Controls();
        movement = controls.Movement;
        menu = controls.Menus;

        movement.Move.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
        movement.Crouch.performed += _ => playCon.OnCrouchPressed();
        movement.Crouch.canceled += _ => playCon.OnCrouchUnpressed();
        movement.Shoot.performed += _ => playCon.OnShootPressed();
        movement.Reload.performed += _ => playCon.OnReloadPressed();

        movement.Zoom.performed += _ => playCon.OnZoomPressed();
        movement.Zoom.canceled += _ => playCon.OnZoomUnpressed();

        //movement.Jump.performed += _ => playCon.OnJumpPressed();

        movement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        movement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        //MENU//
        //menu.Select.performed += _ => uiScript.OnSelectPressed();
        menu.Back.performed += _ => uiScript.CloseControlsPanel();
        menu.Pause.performed += _ => uiScript.PauseGame();
        movement.Interact.performed += _ => playInteract.Interact();
        menu.Cancel.performed += _ => jukeboxRef.CancelOption();
        //menu.Point.performed += ctx 

    }

    private void Update()
    {
        playCon.ReceiveInput(inputVector);
        mouseLook.ReceiveInput(mouseInput);
        //uiScript.ReceiveInput();

        mousePos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

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
