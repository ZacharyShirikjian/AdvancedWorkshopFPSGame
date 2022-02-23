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
    public GameObject Canvas;
    //GraphicRaycaster Raycaster;
    //PointerEventData clickData;
    //List<RaycastResult> clickResults;

    //REFERENCE TO CREDITS PANEL IN SCENE//
    private GameObject creditsPanel;

    //REFERENCE TO OPTIONS PANEL IN SCENE//
    private GameObject optionsPanel;

    //REFERENCE TO THE "PRESS ANY BUTTON TO START" TEXT
    [SerializeField] private TextMeshProUGUI promptText;

    //Checks if player pressed a button on title screen yet, once it = true, the press any button text goes away and stays away
    [SerializeField] private bool buttonPressed = false;

    //Reference to Menu Buttons 
    public GameObject Buttons;

    //// Start is called before the first frame update
    void Start()
    {
        creditsPanel = GameObject.Find("CreditsPanel");
        optionsPanel = GameObject.Find("OptionsPanel");
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        buttonPressed = false;
        promptText.SetText("Press Any Button");
        Buttons.SetActive(false);

        //Raycaster = Canvas.GetComponent<GraphicRaycaster>();
        //clickData = new Pointer(EventSystem.current);
    }

    //// Update is called once per frame
    void Update()
    {
        //FIX THIS METHOD TO WORK W/ NEW INPUT SYSTEM LATER?
        //If you press any key, open up the options buttons
        if(Input.anyKeyDown && buttonPressed == false)
        {
            Buttons.SetActive(true);
            promptText.SetText("");
            buttonPressed = true;
        }

        //if(Mouse.current.leftButton.wasReleasedThisFrame)
        //{
        //    GetUIElementsClicked();
        //}

    }

    //void GetUIElementsClicked()
    //{

    //}

    //This method is used for opening up the Credits Panel in the Title Screen.
    //Temporarily hide the other menu elements, and bring them back once the Credits Panel is closed.
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        Buttons.SetActive(false);
    }

    //Called when clicking on the Back/Close button on the Credits panel when it's opened.
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        Buttons.SetActive(true);
    }

    //This method is used for opening up the Options Panel in the Title Screen.
    //Temporarily hide the other menu elements, and bring them back once the Options Panel is closed.
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        Buttons.SetActive(false);
    }

    //Called when clicking on the Back/Close button on the Options panel when it's opened.
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        Buttons.SetActive(true);
    }

    //This method is used for loading the main scene of the game,
    //ZachTestScene3 (which may get changed later).
    //Which has a build index of 1 in the Project's Build Settings.
    //For now, the TitleScreen scene (ZachTestScene) has a build index of 0.
    public void PlayGame()
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
