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
    //CONTROLS PANEL REF//
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject curControlsPanel;

    [SerializeField] private Image keyboardIcon;
    [SerializeField] private Image controllerIcon;

    //Keyboard Input,Controls Menu
    [SerializeField] private GameObject keyboardInput;

    //Controller Input,Controls Menu
    [SerializeField] private GameObject controllerInput;

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
        
        //Space icon for interacting w/ objects
        public GameObject interactIcon;

    //R icon for reloading
    public GameObject rIcon;


    //Reference to Coin UI text
    public TextMeshProUGUI coinText;
    public int numCoins;

        //Reference to the Bullets GameObject
        public GameObject BulletObject;

        //Array of GameObjects for Bullets
        [SerializeField] private GameObject[] Bullets = new GameObject[6];

        //Reference to the Pause Menu panel (used for pausing)
        public GameObject pausePanel;

    //Reference to Game Over panel
    public GameObject gameOverPanel;

    //Reference to Pause panel animator
    private Animator pauseAnimator;

    //Reference to the Button prompt animator
    [SerializeField] private Animator buttonPromptAnimator;

    //Reference to the Quit panel;
    public GameObject quitPanel;

    //Animator for QuitPanel
    [SerializeField] private Animator quitAnimator;

    //REFERENCE TO JUKEBOX MENU PANEL
    [SerializeField] private GameObject jukeboxMenu;
    //public Image modIcon; //UI Icon for which mod the player currently has on their pistol

    //REFERENCE TO CURRENT JUKEBOX//
    [SerializeField] private GameObject curJukebox;
    //Referenece to PlayerInteract//
    [SerializeField] private PlayerInteract playInteract;

    ////LIST OF MOD IMAGES
    //public Image[] modIcons = new Image[4];

    //Panel for mods//
    [SerializeField] public GameObject currentPanel;
    [SerializeField] public GameObject refillPanel;

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


    //For Poison Effect (https://www.youtube.com/watch?v=5nWRrkaFpic)
    [SerializeField] private Image splatterImage;
    public bool inMist = false;    

    //AMMO//
        //The current amount of bullets which the player can shoot at a time  
        [SerializeField] public int curBullets;

        //The maximum amount of bullets which the player can have in their cylinder,
        //Which decreases by 2 every time they reload their gun
        [SerializeField] public int maxBullets;

        //The total amount of bullets which the player has left in their gun
        [SerializeField] public int backupBullets;
        
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
        extraBullets = 0;
        playerRef = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        curHealth = playerRef.health;
        maxHealth = playerRef.maxHealth;
        curBullets = (int) playerRef.ammo;
        maxBullets = (int) playerRef.maxAmmo;
        backupBullets = 6;

        healthSlider.value = maxHealth;
        healthSliderValue = healthSlider.value;
        healthSlider.transform.localScale = new Vector3(1f, 1f, 0f); //RESET health slider at start of game to represent 100
        curStateText.SetText("");
        interactPromptText.SetText("");
        interactIcon.SetActive(false);
        rIcon.SetActive(false);
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        pauseAnimator = pausePanel.GetComponentInChildren<Animator>();
        controlsPanel.SetActive(false);
        pausePanel.SetActive(false);
        quitPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        jukeboxMenu.SetActive(false);
        gameOver = false;
        paused = false;
        jukeboxOpen = false;
        currentPanel = refillPanel;
        curControlsPanel = keyboardInput;

        for (int i = 0; i < 6; i++)
        {
            Bullets[i].GetComponentInChildren<Image>().enabled = true;
        }

        extraAmmoUI.SetText("0");
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(maxBullets);
        curBullets = (int) playerRef.ammo;
        maxBullets = (int) playerRef.maxAmmo;

        //extraBullets = (int) playerRef.maxAmmo - 6;
        //if(extraBullets <= 0)
        //{
        //    extraBullets = 0;
        //}

        extraAmmoUI.SetText("+" + extraBullets.ToString());
        //curHealth = playerRef.health;
        //maxHealth = playerRef.maxHealth;
        //healthSlider.value = curHealth;
        //healthSlider.maxValue = maxHealth;
        coinText.SetText(numCoins.ToString());
    }

    //CONTROLS PANEL
    public void OpenControlsPanel()
    {
        controlsPanel.SetActive(true);
        controlsPanel.GetComponentInChildren<Animator>().SetTrigger("Controls");
        pausePanel.SetActive(false);
        keyboardInput.SetActive(true);
        controllerInput.SetActive(false);
        controllerIcon.GetComponent<Image>().color = new Color(0.45f, 0.45f, 0.45f);
    }

    //ONE BUTTON FOR CLOSING ANY MENU WHICH IS OPEN
    public void CloseMenu()
    {
        if(controlsPanel.activeSelf == true)
        {
            Debug.Log("CONTROLS ONSEFSdcfdsFDSDfsdfdsfdsfdfdfdfdfdfdfd");
            controlsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }

        else if(jukeboxMenu.activeSelf == true)
        {
            curJukebox = playInteract.curJukebox;
            curJukebox.GetComponent<JukeboxScript>().CancelOption();
        }
    }

    //Gets called when player presses TAB on TitleScreen.
    public void SwitchInputPage()
    {
        if (curControlsPanel == keyboardInput)
        {
            curControlsPanel = controllerInput;
            keyboardInput.gameObject.SetActive(false);
            controllerInput.gameObject.SetActive(true);
            controllerIcon.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            keyboardIcon.GetComponent<Image>().color = new Color(0.45f, 0.45f, 0.45f);
            Time.timeScale = 1f;
            paused = false;
            StaticGameClass.pause = false;
            pausePanel.SetActive(false);
        }

        else if (curControlsPanel == controllerInput)
        {
            curControlsPanel = keyboardInput;
            keyboardInput.gameObject.SetActive(true);
            controllerInput.gameObject.SetActive(false);
            keyboardIcon.GetComponent<Image>().color = new Color(1f, 1f, 1f);
            controllerIcon.GetComponent<Image>().color = new Color(0.45f, 0.45f, 0.45f);
        }
    }
    //For Pausing/Unpausing Game
    public void PauseGame()
    {
        if(gameOver == false)
        {
            if (paused)
            {
                Time.timeScale = 1f;
                paused = false;
                pausePanel.SetActive(false);
            }
            else if (!paused && jukeboxOpen == false)
            {
            Debug.Log("PAUSE BUTTONS ACTIVATED");
            //pausePress = true;
            paused = true;
            StaticGameClass.pause = true;
            pausePanel.SetActive(true);

                Debug.Log("PAUSE BUTTONS ACTIVATED");
                //pausePress = true;
                paused = true;
                pausePanel.SetActive(true);

                //pausePanel.transform.GetChild(1).gameObject is the Resume button child of the PausePanel Parent GOBject, GetChild (0) is the PauseHeader)
                pauseAnimator.SetTrigger("Pausing");
                buttonPromptAnimator.SetTrigger("Pausing");
                eventSystem.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject);
                Debug.Log(eventSystem.currentSelectedGameObject);
                //eventSystem.firstSelectedGameObject = pausePanel.GetComponentInChildren<Button>().gameObject;
                //Time.timeScale = 0f;
            }
        }


    }

    //FOR JUKEBOX UI//
    public void JukeboxUI()
    {
        if(jukeboxOpen)
        {
            UpdateInteractPromptUI("");
            jukeboxMenu.SetActive(true);
            playerRef.enabled = false;
            eventSystem.SetSelectedGameObject(jukeboxMenu.transform.GetChild(0).transform.GetChild(0).gameObject);
            Debug.Log(eventSystem.currentSelectedGameObject);
            paused = true;
            //DISABLE PLAYER MOVEMENT/PLAYER INPUT HERE/
            //eventSystem.firstSelectedGameObject = jukeboxMenu.transform.GetChild(0).gameObject;
        }

        //RENABLE PLAYER MOVEMENT ONCE JUKEBOX MENU IS CLOSED
        else if(!jukeboxOpen)
        {
            paused = false;
            jukeboxMenu.SetActive(false);
            playerRef.enabled = true;
            //eventSystem.firstSelectedGameObject = pausePanel.transform.GetChild(0).gameObject;
        }

    }

    ////SWITCH BETWEEN JUKEBOX PAGES
    //public void SwitchJukeboxPages()
    //{
    //    if(currentPanel == refillPanel)
    //    {
    //        currentPanel = modPanel;
    //        refillPanel.SetActive(false);
    //        modPanel.SetActive(true);
    //        NextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Refill & \nUpgrades \nPage";
    //    }
    //    else if (currentPanel == modPanel)
    //    {
    //        currentPanel = refillPanel;
    //        modPanel.SetActive(false);
    //        refillPanel.SetActive(true);
    //        NextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Mods \nPage";
    //    }
    //}

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
            interactIcon.SetActive(true);
            interactPromptText.SetText(prompt.ToString());
        }

        else
        {
            interactPromptText.SetText("");
            interactIcon.SetActive(false);
        }
    }
    //This method gets called when a player shoots a bullet, called from the PlayerControllerScript

    public void UpdateAmmoUI()
    {
        /*
            * Test method for updating the ammo counter UI;
            * If the player Left Clicks,
            * They shoot a bullet, so hide/remove the bottom-most child of the Bullet GameObjects.
            * If the player presses the R key,
            * They reload their gun, 
            * 
            * If the player is out of ammo, 
            * The curStateText UI lets them know they've run out of ammo
        */

        //IF JUKEBOX IS CLOSED OR PLAYER DIDN'T GET AMMO REFILL
        if(!jukeboxOpen)
        {
            //extraBullets = (int)playerRef.maxAmmo - 6;
            //extraAmmoUI.text = "+" + extraBullets.ToString();
            if (curBullets > 0)
            {
                if(curBullets > 6)
                {
                    extraBullets--;
                    //extraAmmoUI.text = "+" + extraBullets.ToString();
                    curBullets--;
                }

                else if (curBullets <= 6)
                {
                    //PLAY ANIMATION TO ROTATE AMMO CYLINDER BY 60 DEGREES//

                    ammoAnimator.SetTrigger("RotateBullet");
                    //For each child in the Bullets GameObject,
                    //Hide the bottom-most element when a bullet is shot
                    Bullets[curBullets -1].GetComponentInChildren<Image>().enabled = false;
                    curBullets--;
                    if (curBullets <= 0)
                    {
                        Debug.Log(curBullets);
                        Debug.Log("Out of Ammo");
                        curStateText.SetText("Out of Ammo");
                        if (maxBullets > 2)
                        {
                            rIcon.SetActive(true);
                        }

                        else if (maxBullets <= 2)
                        {
                            rIcon.SetActive(false);
                        }

                    }
                }


            }
            //Debug.Log(curBullets);
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
        rIcon.SetActive(false);
        ammoAnimator.SetBool("Idle", false);
        ammoAnimator.SetTrigger("Reloading");
        maxBullets = (int)playerRef.maxAmmo;
        Debug.Log(maxBullets);
        if(maxBullets > 6)
        {
            for (int i = 0; i < 6; i++)
            {
                Debug.Log("More than 6 bullets");
                curStateText.SetText("");
                Bullets[i].GetComponentInChildren<Image>().enabled = true;
            }
        }

        else if (maxBullets <= 6)
        {
            Debug.Log(maxBullets);
            for (int i = 0; i < maxBullets; i++)
            {
                Debug.Log("6 bullets or less");
                curStateText.SetText("");
                Bullets[i].GetComponentInChildren<Image>().enabled = true;
            }

            if(maxBullets == 4)
            {
                backupBullets = 2;
            }

            else if(maxBullets == 2)
            {
                backupBullets = 0;
            }
        }

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
    }

    //TEST METHODS USED FOR THE BUTTONS ON THE PAUSE PANEL DURING A GAME OVER/PAUSE//

    //This method gets called when the player takes damage, from Tina's PlayerController script//
    //The player takes X damage, where X is based on the value when the method gets called//

    public void UpdateHealthUI()
    {
        curHealth = playerRef.health;
        maxHealth = playerRef.maxHealth;
        healthSlider.value = curHealth;
        healthSlider.maxValue = maxHealth;
        if (curHealth > maxHealth)
        {
            curStateText.SetText("Health is Already Full.");
        }

        else if (curHealth <= 0)
        {
            GameOver();
        }
    }

    public void SplatterImage()
    {
        inMist = true;
        if(inMist == true)
        {
            splatterImage.color = new Color(1, 1, 1, 1);
        }
        //curHealth = playerRef.health;
        //healthSliderValue = curHealth;
        //healthSlider.value = healthSliderValue;


    }

    public void StartSplatterCoroutine()
    {
        StartCoroutine(FadeSplatterImage());
    }

    //REFERENCE FOR FADING OUT SPLATTER IMAGE USING COROUTINE
    //Coroutine needed instead of method b/c loop = instant, coroutine = delay 
    //Bunny83's response on this forum: https://answers.unity.com/questions/225438/slowly-fades-from-opaque-to-alpha.html 
    public IEnumerator FadeSplatterImage()
    {
        float fadeTime = 2.0f; 
        if (inMist == true)
        {
            for (float i = 0; i < 1.0f; i += Time.deltaTime / fadeTime)
            {
                //i = opacity, slowly decrease opacity over time for 1 second
                Color alphaColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, i));
                splatterImage.color = alphaColor;
                Debug.Log(splatterImage.color);
                Debug.Log(inMist);
                yield return null;
            }
            inMist = false;
            Debug.Log(inMist);
        }
    }
    //Called when player gets a GameOver//
    public void GameOver()
    {
        gameOver = true;

        //REPLACE THIS W/ TRIGGERING DEATH ANIMATION LATER (Once implemented)
        playerRef.enabled = false;

        //Activate the Game Over Panel during a Game Over 
        gameOverPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(gameOverPanel.transform.GetChild(1).gameObject);
        ////Freeze the game by setting timescale to 1 (temporary) 
        //Time.timeScale = 0f;
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

    //Opens up the Quit Confirmation Panel to ask players if they actually want to quit the game or not 
    public void OpenQuitPanel()
    {
        quitPanel.SetActive(true);
        pausePanel.SetActive(false);
        quitAnimator.SetTrigger("Quitting");
        eventSystem.SetSelectedGameObject(quitPanel.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject); //QuitButton is child of the QuitConfirmPanel object
    }

    //Closes the Quit Confirmation Panel to allow players to go back to the regular pause menu 
    public void CloseQuitPanel()
    {
        quitPanel.SetActive(false);
        pausePanel.SetActive(true);
        eventSystem.SetSelectedGameObject(pausePanel.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject);
    }

    //Called on the Quit button of the Pause menu,
    //Quits the entire game (only works in a Build of the game).
    public void QuitGame()
    {
        Application.Quit();
    }


}
