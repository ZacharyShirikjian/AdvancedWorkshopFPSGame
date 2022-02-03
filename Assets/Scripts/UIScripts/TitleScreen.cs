using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * This script is used for the Title Screen, 
 * Including methods for Loading the Main Game Scene (For now, ZachTestScene2),
 * And Quitting the game.
 * More methods can be added later as necessary.
 * -Zach
 */
public class TitleScreen : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //This method is used for loading the main scene of the game,
    //ZachTestScene2 (which may get changed later).
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

    /*
     * Additional methods to quickly load other people's scenes from the Title Screen,
     * Which may be temporary.
     * These methods get called when clicking on the appropriate button,
     * Eg, to load David's scene, click on the "David's Scene" button,
     * Which calls the LoadDavidScene method.
     */

    public void LoadDavidScene()
    {
        SceneManager.LoadScene("DavidTestScene");
    }

    public void LoadKalynnScene()
    {
        SceneManager.LoadScene("KalynnTestScene");
    }

    public void LoadTinaScene()
    {
        SceneManager.LoadScene("TinaTestScene");
    }

    public void LoadSeanScene()
    {
        SceneManager.LoadScene("SeanTestScene");
    }
}
