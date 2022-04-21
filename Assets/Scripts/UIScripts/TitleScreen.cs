using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/*
 * This script is used for the Title Screen, 
 * Including methods for Loading the Main Game Scene (For now, ZachTestScene2),
 * And Quitting the game.
 * More methods can be added later as necessary.
 * -Zach
 */
public class TitleScreen : MonoBehaviour
{
    //Reference to music GameObject
    [SerializeField] private GameObject musicManager;
    //Controls controls;
    //Controls.MenusActions menu;
    //REFERENCE TO AUDIOSOURCE//
    [SerializeField] private AudioSource source; 
    public EventSystem eventSystem;
    public GameObject Canvas;

    //REFERENCE TO CONTROLS PANEL//
    //REFERENCE TO CREDITS PANEL IN SCENE//
    //REFERENCE TO OPTIONS PANEL IN SCENE//

    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject curPanel;

    [SerializeField] private Image keyboardIcon;
    [SerializeField] private Image controllerIcon;

    //Keyboard Input,Controls Menu
    [SerializeField] private GameObject keyboardInput;

        //Controller Input,Controls Menu
        [SerializeField] private GameObject controllerInput;
    //REFERENCE TO Menu Prompts ANIM in Credits
    [SerializeField] private Animator menuPromptsAnimator; 

    //REFERENCE TO CANCEL BUTTON ANIM in Credits
    [SerializeField] private Animator cancelAnim;

    //REFERENCE TO THE "PRESS ANY BUTTON TO START" TEXT
    [SerializeField] private TextMeshProUGUI promptText;

    //Checks if player pressed a button on title screen yet, once it = true, the press any button text goes away and stays away
    [SerializeField] private bool buttonPressed = false;

    //Reference to Menu Buttons 
    public GameObject Buttons;

    //HOLDS ALL POSSIBLE STATES OF CONTROLLER
    //ints are individual specific states
    //0 = none
    //1 = keyboard
    //2 = controller
    public enum CurrentController { NONE, KEYBOARD, GAMEPAD };
    public CurrentController currentControlScheme = CurrentController.KEYBOARD;
    [SerializeField] private PlayerInput playerInput;

    //public static = doesn't change for instance of the class, can be seen anywhere 
    public static TitleScreen instance;
    //// Start is called before the first frame update
    void Awake()
    {
        //controls = new Controls();
        //menu = controls.Menus;
        instance = this;
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        curPanel = keyboardInput;

        //optionsPanel.SetActive(false);
        buttonPressed = false;
        promptText.SetText("Press          to Start");
        Buttons.SetActive(false);

        //Set volume of the Canvas AudioSource to be = game volume 
        Canvas.GetComponent<AudioSource>().volume = Settings.volume;
        musicManager.GetComponent<AudioManager>().SwitchSong("Title");
    }

    //// Update is called once per frame
    void Update()
    {

    }

    //Based on method written by Peter Gomes//
    //Switches current input depending on whether players uses a controller or keyboard
    public void OnControlsChanged(PlayerInput context)
    {
        //Print out current control scheme player is using
        Debug.Log("Control Scheme: " + context.currentControlScheme);
        Debug.Log("CHANGING");

        //Prevents unexpected Null Ref Exceptions when Switching 
        if (context != null && UITest.instance != null)
        {
            if (TitleScreen.instance.currentControlScheme == CurrentController.GAMEPAD)
            {
                TitleScreen.instance.currentControlScheme = CurrentController.KEYBOARD;
                Debug.Log("NOW IS KEYBOARD");
            }

            else if (TitleScreen.instance.currentControlScheme == CurrentController.KEYBOARD)
            {
                TitleScreen.instance.currentControlScheme = CurrentController.GAMEPAD;
                Debug.Log("NOW IS GAMEPAD");
            }
        }

    }
    public void OpenMenu()
    {
        if (buttonPressed == false)
        {
            //eventSystem.SetSelectedGameObject(Buttons.transform.GetChild(1).gameObject);
            Buttons.SetActive(true);
            promptText.gameObject.SetActive(false);
            buttonPressed = true;
            menuPromptsAnimator.SetTrigger("Pausing");
        }
    }

    public void BackToMenu()
    {
        //If any panel Object is active, turn them off, return to Main Menu
        if(creditsPanel.activeSelf == true || controlsPanel.activeSelf == true || optionsPanel.activeSelf == true)
        {
            optionsPanel.SetActive(false);
            creditsPanel.SetActive(false);
            controlsPanel.SetActive(false);
            Buttons.SetActive(true);
            eventSystem.SetSelectedGameObject(Buttons.transform.GetChild(4).gameObject);
        }
       
    }
    //This method is used for opening up the Credits Panel in the Title Screen.
    //Temporarily hide the other menu elements, and bring them back once the Credits Panel is closed.
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        cancelAnim.SetTrigger("Credits");
        Buttons.SetActive(false);
    }

    //Called when clicking on the Back/Close button on the Credits panel when it's opened.
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        Buttons.SetActive(true);
    }

    //CONTROLS PANEL//

    public void OpenControlsPanel()
    {
        controlsPanel.SetActive(true);
        cancelAnim.SetTrigger("Controls");
        keyboardInput.SetActive(true);
        controllerInput.SetActive(false);
        controllerIcon.GetComponent<Image>().color = new Color(0.45f, 0.45f, 0.45f);
        Buttons.SetActive(false);
    }

    //Gets called when player presses TAB on TitleScreen.
    public void SwitchInputPage()
    {
        if(curPanel == keyboardInput)
        {
            curPanel = controllerInput;
            keyboardInput.gameObject.SetActive(false);
            controllerInput.gameObject.SetActive(true);
            controllerIcon.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            keyboardIcon.GetComponent<Image>().color = new Color(0.45f, 0.45f, 0.45f);
        }

        else if (curPanel == controllerInput)
        {
            curPanel = keyboardInput;
            keyboardInput.gameObject.SetActive(true);
            controllerInput.gameObject.SetActive(false);
            keyboardIcon.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            controllerIcon.GetComponent<Image>().color = new Color(0.45f, 0.45f, 0.45f);
        }
    }

    ////This method is used for opening up the Options Panel in the Title Screen.
    ////Temporarily hide the other menu elements, and bring them back once the Options Panel is closed.
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        cancelAnim.SetTrigger("Credits");
        Buttons.SetActive(false);
        eventSystem.SetSelectedGameObject(optionsPanel.transform.GetChild(3).gameObject); //this is the Volume Slider GObject
    }

    //Called when clicking on the Back/Close button on the Options panel when it's opened.
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        cancelAnim.SetTrigger("Credits");
        Buttons.SetActive(true);
    }

    //This method is used for loading the main scene of the game,
    //Which has a build index of 1 in the Project's Build Settings.
    //For now, the TitleScreen scene has a build index of 0.
    public void PlayGame()
    {
        Invoke("PlayAfterDelay", 0.5f);
    }

    public void PlayAfterDelay()
    {
        SceneManager.LoadScene(1);
    }
    
    //This method is used for quitting the game.
    //It gets called when the player clicks on the QUIT button on the Title Screen.
    public void QuitGame()
    {
        Application.Quit();
    }
}
