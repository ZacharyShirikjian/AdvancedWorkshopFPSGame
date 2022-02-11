using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/*
 * This script is used for the Title Screen, 
 * Including methods for Loading the Main Game Scene (For now, ZachTestScene2),
 * And Quitting the game.
 * More methods can be added later as necessary.
 * -Zach
 */
public class TitleScreen : MonoBehaviour
{

    //REFERENCE TO THE "PRESS ANY BUTTON TO START" TEXT
    [SerializeField] private TextMeshProUGUI promptText;

    //Checks if player pressed a button on title screen yet, once it = true, the press any button text goes away and stays away
    [SerializeField] private bool buttonPressed = false;

    //Reference to Menu Buttons 
    public GameObject Buttons;

    //// Start is called before the first frame update
    void Start()
    {
        buttonPressed = false;
        promptText.SetText("Press Any Button");
        Buttons.SetActive(false);

    }

    //// Update is called once per frame
    void Update()
    {
        //If you press any key, open up the options buttons
        if(Input.anyKeyDown && buttonPressed == false)
        {
            Buttons.SetActive(true);
            promptText.SetText("");
            buttonPressed = true;
        }

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
