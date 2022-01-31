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

        //Reference to the Bullets GameObject
        public GameObject BulletObject;

        //List of GameObjects for Bullets
        List<GameObject> Bullets = new List<GameObject>();

        //Reference to the Pause Menu panel (used for pausing/Game Over)
        public GameObject pausePanel;

    //VARIABLES//

        //HEALTH//
            //The current health which the player has
            public int curHealth;

            //The maximum health which the player can have (same as the max value on the slider)
            public int maxHealth;

            //The value of the HealthSlider
            private int healthSliderValue;

    //AMMO//
        //The current amount of bullets which the player can shoot 
        public int curBullets;

        //The maximum amount of bullets which the player can have in their cylinder,
        //Which decreases by 2 every time they reload their gun
        public int maxBullets;
        
        //Build index of the current scene (will be more important once more scenes are added)
        private int curSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = 30;
        maxHealth = 30;
        curBullets = 6;
        maxBullets = 6;
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        healthSliderValue = (int) healthSlider.value;
        curStateText.SetText("");
        pausePanel.SetActive(false);
        for(int i = 0; i < BulletObject.transform.childCount -1; i++)
        {
            Bullets.Add(BulletObject.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Test method for updating the ammo counter UI;
         * If the player Left Clicks,
         * They shoot a bullet, so hide/remove the bottom-most child of the Bullet GameObjects.
         * If the player Right-Clicks,
         * They reload their gun, 
         * 
         * If the player is out of ammo, 
         * The curStateText UI lets them know they've run out of ammo
         */
        if(Input.GetMouseButtonDown(0))
        {
            if(curBullets <=0)
            {
                curStateText.SetText("Out of Ammo");
            }

            else if(curBullets > 0)
            {
                curBullets--;
                //For each child in the Bullets GameObject,
                //Hide the bottom-most element when a bullet is shot
                for(int i = 0; i < maxBullets - 1; i++)
                {

                }
                //if(curBullets == 0)
                //{
                //    Reload();
                //}
            }
        }

        //After right-clicking, call the Reload() method to reload your weapon (cooldown/animation will be added later)
        else if(Input.GetMouseButtonDown(1) && (curBullets < maxBullets))
        {
            Reload();
        }
        /*
         *  Test method for lowering the health UI slider, will get adjusted once damage is implemented;
         *  If the player presses the X key,
         *  The player's health decreases by 10
         *  If the player loses all of their health, the game is over 
         */
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

    //This method gets called when the player Right-Clicks with their weapon,
    //Or when they've run out of ammo in a single cylinder.
    //Reload the ammo cylinder, reducing the max bullets they can hold by 2.
    //1st Reload = 4 Max Bullets, 2nd Reload = 2 Max Bullets
    //Add/subtract the Bullet sprites accordingly
    public void Reload()
    {
        curStateText.SetText("");
        curBullets = maxBullets - 2;
        maxBullets = maxBullets - 2;
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
