using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
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
    //REFERENCE TO TINA'S GUN SCRIPT//
    public Gun gunReference;

    //REFERENCE TO TINA'S PLAYER CONTROLLER SCRIPT//    
    public ZachPlayerController playerRef; 

    //REFERENCES//

        //Reference to the Health UI Slider
        public Slider healthSlider;

        //Reference to Game Over UI Text
        public TextMeshProUGUI curStateText;

        //Reference to the InteractPrompt Text
        public TextMeshProUGUI interactPromptText;

        //Reference to the Bullets GameObject
        public GameObject BulletObject;

        //Array of GameObjects for Bullets
        [SerializeField] private GameObject[] Bullets = new GameObject[6];

        //Reference to the Pause Menu panel (used for pausing/Game Over)
        public GameObject pausePanel;

    //REFERENCE TO JUKEBOX MENU PANEL
    [SerializeField] private GameObject jukeboxMenu;

    //VARIABLES//
    //Checks if Game Over is true, if true enemies can't track player anymore
    public bool gameOver = false;

    //Checks if game is paused 
    public bool paused;

    //FOR NEW INPUT SYSTEM TEST
    public bool selected;
    public bool pausePress;


    //HEALTH//
        //The current health which the player has
        [SerializeField] private float curHealth;

        //The maximum health which the player can have (same as the max value on the slider)
        [SerializeField] private float maxHealth;

            //The value of the HealthSlider
            private float healthSliderValue;

    //AMMO//
        //The current amount of bullets which the player can shoot 
        [SerializeField] private int curBullets;

        //The maximum amount of bullets which the player can have in their cylinder,
        //Which decreases by 2 every time they reload their gun
        [SerializeField] private int maxBullets;
        
        //Build index of the current scene (will be more important once more scenes are added)
       [SerializeField] private int curSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        curHealth = playerRef.health;
        maxHealth = playerRef.maxHealth;
        curBullets = gunReference.ammo;
        maxBullets = gunReference.ammo;
        healthSlider.value = maxHealth;
        healthSliderValue = healthSlider.value;
        curStateText.SetText("");
        interactPromptText.SetText("");
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        pausePanel.SetActive(false);
        jukeboxMenu.SetActive(false);
        gameOver = false;
        paused = false; 
        for(int i = 0; i < 6; i++)
        {
            Bullets[i].GetComponentInChildren<Image>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //////For Pausing
        //if (!paused)
        //{
        //    PauseGame();
        //}

        //else if (paused)
        //{
        //    UnPauseGame();
        //}
    }

    //FOR NEW INPUT SYSTEM SELECT//
    public void OnSelectPressed()
    {
        selected = true;
    }

    public void ReceiveInput()
    {

    }

    //For Pausing/Unpausing Game
    public void PauseGame()
    {
        if(paused)
        {
            Time.timeScale = 1f;
            paused = false;
            pausePanel.SetActive(false);
        }
        else if(!paused)
        {
            Debug.Log("testestets");
            //pausePress = true;
            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    //public void UnPauseGame()
    //{

    //}

    //FOR JUKEBOX UI//
    public void JukeboxUI()
    {
        jukeboxMenu.SetActive(true);
        //DISABLE PLAYER MOVEMENT/PLAYER INPUT HERE/
    }

    //TEMP? method; closes out jukebox menu after a second
    public void closeJukeboxUI()
    {
        jukeboxMenu.SetActive(false);
    }


    //Upadate the InteractPrompt UI based on the action prompted from the Interactable (eg climb over, duck, etc)
    public void UpdateInteractPromptUI(string prompt)
    {
        if(prompt != "")
        {
            interactPromptText.SetText("Press [SPACE] to " + prompt.ToString());
        }

        else
        {
            interactPromptText.SetText("");
        }
    }
    //This method gets called when a player shoots a bullet, called from the PlayerControllerScript

    public void UpdateAmmoUI()
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

        if (curBullets <= 0)
        {
            curStateText.SetText("Out of Ammo");
        }

        else if (curBullets > 0)
        {
            curBullets--;
            //For each child in the Bullets GameObject,
            //Hide the bottom-most element when a bullet is shot
            Bullets[curBullets].GetComponentInChildren<Image>().enabled = false;
            Debug.Log("Bullet has been fired");
        }
    }

    //This method gets called from the PlayerController script, when the player Right-Clicks with their weapon,
    //Or when they've run out of ammo in a single cylinder.
    //Reload the ammo cylinder, reducing the max bullets they can hold by 2.
    //1st Reload = 4 Max Bullets, 2nd Reload = 2 Max Bullets
    //Add/subtract the Bullet sprites accordingly
    public void Reload()
    {
        curStateText.SetText("");
        curBullets = maxBullets - 2;
        maxBullets = maxBullets - 2;

        if (maxBullets == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                Bullets[i].GetComponentInChildren<Image>().enabled = true;
            }
        }

        else if (maxBullets == 2)
        {
            Bullets[0].GetComponentInChildren<Image>().enabled = true;
            Bullets[1].GetComponentInChildren<Image>().enabled = true;
        }

        else if (maxBullets <= 0)
        {
            curStateText.SetText("Out of Ammo");
        }
    }
    //TEST METHODS USED FOR THE BUTTONS ON THE PAUSE PANEL DURING A GAME OVER/PAUSE//

    //This method gets called when the player takes damage, from Tina's PlayerController script//
    //The player takes X damage, where X is based on the value when the method gets called//

    public void UpdateHealthUI()
    {
        curHealth = playerRef.health;
        healthSliderValue = curHealth;
        healthSlider.value = healthSliderValue; 
      
        if (curHealth >= maxHealth)
        {
            curStateText.SetText("Health Maxed Out");
        }

        else if (curHealth <= 0)
        {
            GameOver();
        }
    }

    //Called when player gets a GameOver//
    public void GameOver()
    {
        gameOver = true;
        playerRef.gameObject.SetActive(false);
        BulletObject.SetActive(false);
        healthSlider.gameObject.SetActive(false);
        curStateText.SetText("GameOver");

        //Activate the Pause Panel during a Game Over
        pausePanel.SetActive(true);

        //Freeze the game by setting timescale to 1 (temporary) 
        Time.timeScale = 0f;
    }
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
