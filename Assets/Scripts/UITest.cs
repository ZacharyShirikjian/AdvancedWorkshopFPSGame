using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
/*
    *This is a temporary script for testing out the Basic UI functionality.
    *Once Tina's basic programming is implemented, 
    *I will copy this code to her scripts, 
    *And modify them to work accordingly with her scripts.
    *-Zach
*/
public class UITest : MonoBehaviour
{
    //REFERENCES//

        //Reference to the Health UI Slider
        public Slider healthSlider;

        //Reference to Game Over UI Text
        public TextMeshProUGUI curStateText;

        //Reference to the Pause Menu panel (used for pausing/Game Over)
        public GameObject pausePanel;

    //VARIABLES//

        //The current health which the player has
        public int curHealth;

        //The maximum health which the player can have (same as the max value on the slider)
        public int maxHealth;

        //The value of the HealthSlider
        private int healthSliderValue;

        //Build index of the current scene (will be more important once more scenes are added)
        private int curSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = 30;
        maxHealth = 30;
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        healthSliderValue = (int) healthSlider.value;
        curStateText.SetText("");
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Test method for lowering the health UI slider, will get adjusted once damage is implemented;
        //If the player presses the X key,
        //The player's health decreases by 10
        //If the player loses all of their health, the game is over 
        if(Input.GetKeyDown(KeyCode.X) && curHealth > 0)
        {
            curHealth -= 10;
            healthSliderValue = curHealth;
            healthSlider.value = (int)healthSliderValue;
            if (curHealth <= 0)
            {
                curStateText.SetText("GameOver");

                //Activate the Pause Panel during a Game Over
                pausePanel.SetActive(true);
            }
        }

        //Test method for increasing the health UI slider, will get adjusted once healing is implemented;
        //If the player presses the S key,
        //The player's health increases by 30
        //If the player has max health, they can't increase it any more
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (curHealth >= 30)
            {
                curStateText.SetText("Health Maxed Out");
            }

            else if (curHealth < 30)
            {
                curHealth += 10;
                healthSliderValue = curHealth;
                healthSlider.value = (int)healthSliderValue;
            }

        }
    }


    //TEST METHODS USED FOR THE BUTTONS ON THE PAUSE PANEL DURING A GAME OVER/PAUSE//

    //Reloads the current scene the player is on 
    public void RestartGame()
    {
        SceneManager.LoadScene(curSceneIndex);
    }

    //Brings players back to the TitleScreen scene with a build index of 0
    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    //Called on the Quit button of the Pause menu,
    //Quits the entire game (only works in a Build of the game).
    public void QuitGame()
    {
        Application.Quit();
    }

}
