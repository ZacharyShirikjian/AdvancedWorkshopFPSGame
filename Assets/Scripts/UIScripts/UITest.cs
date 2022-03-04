using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
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
    //REFERENCE TO TINA'S PLAYER CONTROLLER SCRIPT//    
    private PlayerController playerRef;

    //REFERENCES//

    //REFEERENCE TO AMMO CYLINDER ANIMATION 
    [SerializeField] private Animator ammoAnimator;

        //Reference to the Health UI Slider
        public Slider healthSlider;

        //Reference to Game Over UI Text
        public TextMeshProUGUI curStateText;

        //Reference to the InteractPrompt Text
        public TextMeshProUGUI interactPromptText;

    //Reference to Coin UI text
    public TextMeshProUGUI coinText;
    public int numCoins;

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
    //public bool selected;
    public bool jukeboxOpen;

    //Event Systems
    public EventSystem eventSystem;

    //HEALTH//
        //The current health which the player has
        [SerializeField] private float curHealth;

        //The maximum health which the player can have (same as the max value on the slider)
        [SerializeField] private float maxHealth;

            //The value of the HealthSlider
            private float healthSliderValue;

    //AMMO//
        //The current amount of bullets which the player can shoot 
        [SerializeField] public int curBullets;

        //The maximum amount of bullets which the player can have in their cylinder,
        //Which decreases by 2 every time they reload their gun
        [SerializeField] public int maxBullets;
        
        //Build index of the current scene (will be more important once more scenes are added)
       [SerializeField] private int curSceneIndex;

    //Reference to reserve ammo (when players have > 6 bullets at a time) 
    [SerializeField] public int extraBullets;
    public TextMeshProUGUI extraAmmoUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        //eventSystem.firstSelectedGameObject = null;
        numCoins = 0;
        playerRef = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        curHealth = playerRef.health;
        maxHealth = playerRef.maxHealth;
        curBullets = (int) playerRef.ammo;
        maxBullets = (int) playerRef.maxAmmo;

        healthSlider.value = maxHealth;
        healthSliderValue = healthSlider.value;
        curStateText.SetText("");
        interactPromptText.SetText("");
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        pausePanel.SetActive(false);
        jukeboxMenu.SetActive(false);
        gameOver = false;
        paused = false;
        jukeboxOpen = false;
        for (int i = 0; i < 6; i++)
        {
            Bullets[i].GetComponentInChildren<Image>().enabled = true;
        }

        extraAmmoUI.SetText("0");
    }

    // Update is called once per frame
    void Update()
    {
        curBullets = (int) playerRef.ammo;
        maxBullets = (int)playerRef.maxAmmo;
        extraBullets = (int) playerRef.maxAmmo - 6;
        if(extraBullets <= 0)
        {
            extraBullets = 0;
        }
        extraAmmoUI.text = extraBullets.ToString();
        curHealth = playerRef.health;
        maxHealth = playerRef.maxHealth;
       //healthSliderValue = curHealth;
        healthSlider.value = curHealth;
        healthSlider.maxValue = maxHealth;
        coinText.SetText(numCoins.ToString());

        /*
        if (paused)
        {
            eventSystem.firstSelectedGameObject = pausePanel.transform.GetChild(0).gameObject;
            if (jukeboxMenu.activeSelf)
            {
                //jukeboxMenu.SetActive(false);
            }
        } 
        else if (!paused)
        {
            eventSystem.firstSelectedGameObject = jukeboxMenu.transform.GetChild(0).gameObject;

        }
        */
    }

    //For Pausing/Unpausing Game
    public void PauseGame()
    {
        if (paused)
        {
            Time.timeScale = 1f;
            paused = false;
            pausePanel.SetActive(false);
        }
        else if(!paused && jukeboxOpen == false)
        {

            Debug.Log("PAUSE BUTTONS ACTIVATED");
            //pausePress = true;
            paused = true;
            pausePanel.SetActive(true);
            eventSystem.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject);
            Debug.Log(eventSystem.currentSelectedGameObject);
            //eventSystem.firstSelectedGameObject = pausePanel.GetComponentInChildren<Button>().gameObject;
            //Time.timeScale = 0f;
        }

    }

    //FOR JUKEBOX UI//
    public void JukeboxUI()
    {
        if(jukeboxOpen)
        {
            UpdateInteractPromptUI("");
            jukeboxMenu.SetActive(true);
            //Time.timeScale = 0f;
            playerRef.enabled = false;
            eventSystem.SetSelectedGameObject(jukeboxMenu.transform.GetChild(0).gameObject);
            Debug.Log(eventSystem.currentSelectedGameObject);
            //DISABLE PLAYER MOVEMENT/PLAYER INPUT HERE/
            //eventSystem.firstSelectedGameObject = jukeboxMenu.transform.GetChild(0).gameObject;
        }

        //RENABLE PLAYER MOVEMENT ONCE JUKEBOX MENU IS CLOSED
        else if(!jukeboxOpen)
        {
            jukeboxMenu.SetActive(false);
            playerRef.enabled = true;
            //eventSystem.firstSelectedGameObject = pausePanel.transform.GetChild(0).gameObject;
        }

    }

    //public void CloseJukeboxUI()
    //{
    //    jukeboxMenu.SetActive(false);
    //    playerRef.enabled = true;
    //}

    //Update the InteractPrompt UI based on the action prompted from the Interactable (eg climb over, duck, etc)
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

        //IF JUKEBOX IS CLOSED OR PLAYER DIDN'T GET AMMO REFILL
        if(!jukeboxOpen)
        {
            if (curBullets <= 0)
            {
                curStateText.SetText("Out of Ammo");
            }

            else if (curBullets > 0)
            {
                curBullets--;

                if(curBullets < 6)
                {
                    //PLAY ANIMATION TO ROTATE AMMO CYLINDER BY 60 DEGREES//
                    ammoAnimator.SetTrigger("RotateBullet");
                    //For each child in the Bullets GameObject,
                    //Hide the bottom-most element when a bullet is shot
                    Bullets[curBullets].GetComponentInChildren<Image>().enabled = false;
                }

            }
        }

        else if(jukeboxOpen)
        {
            if(maxBullets > 6)
            {
                Debug.Log("TWO EXTRA BULLETS");
            }
        }

        //FOR REFILLING PLAYER AMMO BY 2 BULLETS (TEMP SOLUTION)
        else if(jukeboxOpen && (curBullets != maxBullets))
        {
            Debug.Log("REFILL AMMO UI");
            Bullets[curBullets + 1].GetComponentInChildren<Image>().enabled = true;
            Bullets[curBullets].GetComponentInChildren<Image>().enabled = true;
        }

    }
    //This method gets called from the PlayerController script, when the player Right-Clicks with their weapon,
    //Or when they've run out of ammo in a single cylinder.
    //Reload the ammo cylinder, reducing the max bullets they can hold by 2.
    //1st Reload = 4 Max Bullets, 2nd Reload = 2 Max Bullets
    //Add/subtract the Bullet sprites accordingly
    public void Reload()
    {
        //RELOAD ANIMATION PLAYS//
        Debug.Log("RELOADING...");
        ammoAnimator.SetTrigger("Reloading");
        Debug.Log(maxBullets);
        if (maxBullets == 6)
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log("6 Bullets");
                Bullets[i].GetComponentInChildren<Image>().enabled = true;
            }
        }

        else if (maxBullets <= 6)
        {
            for (int i = 0; i < maxBullets; i++)
            {
                Debug.Log("6 Bullets or Less");
                Bullets[i].GetComponentInChildren<Image>().enabled = true;
            }
        }

        //else if (maxBullets == 2)
        //{
        //    Debug.Log("2 Bullets");
        //    Bullets[0].GetComponentInChildren<Image>().enabled = true;
        //    Bullets[1].GetComponentInChildren<Image>().enabled = true;
        //}

        else if (maxBullets <= 0)
        {
            curStateText.SetText("Out of Ammo");
        }
        Invoke("ReloadDelay", 2f);
    }

    //UPDATE THE AMMO UI FOR RELOADING BULLETS W/ PLACEHOLDER DELAY//
    public void ReloadDelay()
    {
        Debug.Log("RELOADED");
        ammoAnimator.SetBool("Idle", true);
        curStateText.SetText("");
        //ADD A CANSHOOT BOOL SO PLAYER CAN'T SHOOT DURING RELOAD ANIMATION HERE//

        //PLACEHOLDER DELAY FOR UPDATING AMMO UI
        //TO-DO: MATCH THIS W/ TIMING OF RELOAD ANIMATION

        //Debug.Log(maxBullets);
        //if(maxBullets == 6)
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Debug.Log("6 Bullets");
        //        Bullets[i].GetComponentInChildren<Image>().enabled = true;
        //    }
        //}

        //else if (maxBullets <= 6)
        //{
        //    for (int i = 0; i < maxBullets; i++)
        //    {
        //        Debug.Log("6 Bullets or Less");
        //        Bullets[i].GetComponentInChildren<Image>().enabled = true;
        //    }
        //}

        ////else if (maxBullets == 2)
        ////{
        ////    Debug.Log("2 Bullets");
        ////    Bullets[0].GetComponentInChildren<Image>().enabled = true;
        ////    Bullets[1].GetComponentInChildren<Image>().enabled = true;
        ////}

        //else if (maxBullets <= 0)
        //{
        //    curStateText.SetText("Out of Ammo");
        //}
    }

    //TEST METHODS USED FOR THE BUTTONS ON THE PAUSE PANEL DURING A GAME OVER/PAUSE//

    //This method gets called when the player takes damage, from Tina's PlayerController script//
    //The player takes X damage, where X is based on the value when the method gets called//

    public void UpdateHealthUI()
    {
        //curHealth = playerRef.health;
        //healthSliderValue = curHealth;
        //healthSlider.value = healthSliderValue; 
      
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
