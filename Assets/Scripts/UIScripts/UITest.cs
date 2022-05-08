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
    //public static = doesn't change for instance of the class, can be seen anywhere 
    public static UITest instance;

    [SerializeField] private GameObject cursor;

    //AUDIO CLIPS//
    [SerializeField] private AudioClip buttonHover;
    [SerializeField] private AudioClip quitConfirm;
    [SerializeField] private AudioClip quitCancel;
    [SerializeField] private AudioClip pauseGame;
    [SerializeField] private AudioClip unPauseGame;
    [SerializeField] private AudioClip playerPoisoned;

    //CONTROLS PANEL REF//
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject curControlsPanel;

    [SerializeField] private Image keyboardIcon;
    [SerializeField] private Image controllerIcon;

    //Keyboard Input,Controls Menu
    [SerializeField] private GameObject keyboardInput;

    //Controller Input,Controls Menu
    [SerializeField] private GameObject controllerInput;

    //SETTINGS PANEL//
    [SerializeField] private GameObject settingsPanel;

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
    public GameObject curJukebox;

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
    [SerializeField] private TextMeshProUGUI healthText;
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

        //The amount of bullets which the player has left in their gun, without extra ammo
        [SerializeField] public int backupBullets;

        //The total amount of bullets which the player has left in their gun
        [SerializeField] public int totalBullets;

    //Build index of the current scene (will be more important once more scenes are added)
    [SerializeField] private int curSceneIndex;

    //Reference to reserve ammo (when players have > 6 bullets at a time) 
    [SerializeField] public int extraBullets;
    public TextMeshProUGUI extraAmmoUI;


    //REF TO AUDIOSOURCE
    private AudioSource canvasSource;

    //private GameObject musicManager;
    private GameObject sfxManager;

    //HOLDS ALL POSSIBLE STATES OF CONTROLLER
    //ints are individual specific states
    //0 = none
    //1 = keyboard
    //2 = controller
    public enum CurrentController {NONE, KEYBOARD, GAMEPAD};
    public CurrentController currentControlScheme = CurrentController.KEYBOARD;
    [SerializeField] private PlayerInput playerInput;

    //Reference to Menu Buttons 
    //public GameObject ButtonParent;
    public GameObject[] Buttons = new GameObject[5];


    //TEMP VARIABLE HOLDING # OF ENEMIES IN A LEVEL, INCREASES WHEN PLAYER ENTERS COMBAT TRIGGER COLLIDER
    public int numEnemies = 0;
    //Set the instance to be this class
    private void Awake()
    {
        instance = this;  
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        playerInput.onControlsChanged += OnControlsChanged;
        currentControlScheme = CurrentController.KEYBOARD;
        canvasSource = GetComponent<AudioSource>();
        cursor.SetActive(false);
        numCoins = 0;
        extraBullets = 0;
        playerRef = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        curHealth = playerRef.health;
        maxHealth = playerRef.maxHealth;
        healthText.text = "100";
        curBullets = (int) playerRef.ammo;
        maxBullets = (int) playerRef.maxAmmo;
        backupBullets = 6;
        totalBullets = 12;
        healthSlider.value = maxHealth;
        healthSliderValue = healthSlider.value;
        healthSlider.transform.localScale = new Vector3(1f, 1f, 0f); //RESET health slider at start of game to represent 100
        curStateText.SetText("");
        interactPromptText.SetText("");
        interactIcon.SetActive(false);
        rIcon.SetActive(false);
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        pauseAnimator = pausePanel.GetComponentInChildren<Animator>();
        settingsPanel.SetActive(false);
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
        //Debug.Log(AudioListener.volume);

        //sfxManager = GameObject.Find("SFXManager");
        //musicManager = GameObject.Find("MusicManager");
        //musicManager.GetComponent<AudioManager>().SwitchSong("Gameplay");
        //canvasSource.volume = Settings.volume;
        Debug.Log(Settings.volume);
        //musicManager.GetComponent<AudioSource>().volume = Settings.musicVolume;
        //sfxManager.GetComponent<AudioSource>().volume = Settings.volume;

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

        healthSlider.value = curHealth;
        healthSlider.maxValue = maxHealth;
        curHealth = playerRef.health;
        maxHealth = playerRef.maxHealth;
        healthText.text = curHealth.ToString();
    }

    //Based on method written by Peter Gomes//

    public void OnControlsChanged(PlayerInput context)
    {
        //Print out current control scheme player is using
        Debug.Log("Control Scheme: " + context.currentControlScheme);
        Debug.Log("CHANGING");
        //Prevents unexpected Null Ref Exceptions when Switching 
        if (context != null && UITest.instance != null)
        {
            if(UITest.instance.currentControlScheme == CurrentController.GAMEPAD)
            {
                UITest.instance.currentControlScheme = CurrentController.KEYBOARD;
                Debug.Log("NOW IS KEYBOARD");
            }

            else if (UITest.instance.currentControlScheme == CurrentController.KEYBOARD)
            {
                UITest.instance.currentControlScheme = CurrentController.GAMEPAD;
                Debug.Log("NOW IS GAMEPAD");
            }
        }

    }
    //CONTROLS PANEL
    public void OpenControlsPanel()
    {
        controlsPanel.SetActive(true);
        controlsPanel.GetComponentInChildren<Animator>().SetTrigger("Controls");
        for (int i = 0; i < 5; i++)
        {
            Buttons[i].GetComponent<Button>().interactable = false;
        }
        //pausePanel.SetActive(false);
        keyboardInput.SetActive(true);
        controllerInput.SetActive(false);
        controllerIcon.GetComponent<Image>().color = new Color(0.45f, 0.45f, 0.45f);
    }

    //Settings PANEL
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        settingsPanel.GetComponentInChildren<Animator>().SetTrigger("Options");
        for (int i = 0; i < 5; i++)
        {
            Buttons[i].GetComponent<Button>().interactable = false;
        }
        eventSystem.SetSelectedGameObject(settingsPanel.transform.GetChild(3).gameObject); //this is the Volume Slider GObject

    }
    //ONE BUTTON FOR CLOSING ANY MENU WHICH IS OPEN
    public void CloseMenu()
    {
        if (controlsPanel.activeSelf == true)
        {
            controlsPanel.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                Buttons[i].GetComponent<Button>().interactable = true;
            }
            eventSystem.SetSelectedGameObject(pausePanel.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject);
            //pausePanel.SetActive(true);
        }

        else if (jukeboxMenu.activeSelf == true)
        {
            curJukebox = playInteract.curJukebox.gameObject;
            if (curJukebox.GetComponent<JukeboxScript>().purchasing == true)
            {
                curJukebox.GetComponent<JukeboxScript>().CancelOption();
            }
        }

        else if (settingsPanel.activeSelf == true)
        {
            settingsPanel.SetActive(false);
            for (int i = 0; i < 5; i++)
            {
                Buttons[i].GetComponent<Button>().interactable = true;
            }
            eventSystem.SetSelectedGameObject(pausePanel.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject);
        }

        else if (pausePanel.activeSelf == true)
        {
            PauseGame();
        }
        else
        {
            Debug.Log("DO NOTHING");
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
        if (gameOver == false)
        {
            if (paused && (controlsPanel.activeSelf == false || settingsPanel.activeSelf == false))
            {
                GetComponent<AudioSource>().PlayOneShot(unPauseGame);
                paused = false;
                StaticGameClass.pause = false;
                pausePanel.SetActive(false);
                cursor.SetActive(false);
            }
            else if (!paused && jukeboxOpen == false)
            {
                Debug.Log("PAUSE BUTTONS ACTIVATED");
                paused = true;
                StaticGameClass.pause = true;
                pausePanel.SetActive(true);
                GetComponent<AudioSource>().PlayOneShot(pauseGame);
                Debug.Log("PAUSE BUTTONS ACTIVATED");
                cursor.SetActive(true);

                //pausePanel.transform.GetChild(1).gameObject is the Resume button child of the PausePanel Parent GOBject, GetChild (0) is the PauseHeader)
                pauseAnimator.SetTrigger("Pausing");
                buttonPromptAnimator.SetTrigger("Menu");
                eventSystem.SetSelectedGameObject(pausePanel.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject);
                Debug.Log(eventSystem.currentSelectedGameObject);
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
            cursor.SetActive(true);
            playerRef.enabled = false;
            eventSystem.SetSelectedGameObject(jukeboxMenu.transform.GetChild(0).transform.GetChild(0).gameObject);
            Debug.Log(eventSystem.currentSelectedGameObject);
            paused = true;
            StaticGameClass.pause = true;
        }

        //RENABLE PLAYER MOVEMENT ONCE JUKEBOX MENU IS CLOSED
        else if(!jukeboxOpen)
        {
            paused = false;
            StaticGameClass.pause = false;
            jukeboxMenu.SetActive(false);
            playerRef.enabled = true;
            cursor.SetActive(false);
        }

    }

    public void closeJukebox()
    {
        curJukebox.GetComponent<JukeboxScript>().closeJukeboxMenu();
    }

    public void JukeboxHealPlayer()
    {
        curJukebox.GetComponent<JukeboxScript>().HealPlayer();
    }

    public void JukeboxRefillAmmo()
    {
        curJukebox.GetComponent<JukeboxScript>().ReloadAmmo();
    }

    public void JukeboxHealthUp()
    {
        curJukebox.GetComponent<JukeboxScript>().MaxHealthUp();
    }

    public void JukeboxAmmoUp()
    {
        curJukebox.GetComponent<JukeboxScript>().MaxAmmoUp();
    }

    public void DisableJukeboxBUttons()
    {
        curJukebox.GetComponent<JukeboxScript>().DisableButtons();
    }
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

    public void UpdateAmmoUI(bool upgradedAmmo)
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
        if(upgradedAmmo == true)
        {
            Debug.Log("aoksERHFADSFOICHASDOI8HDSAFOI8DSAH");
            for (int i = 0; i < 6; i++)
            {
                Bullets[i].GetComponentInChildren<Image>().enabled = true;
            }
            upgradedAmmo = false;
        }

        if(upgradedAmmo == false)
        {
            if (!jukeboxOpen)
            {
                //extraBullets = (int)playerRef.maxAmmo - 6;
                //extraAmmoUI.text = "+" + extraBullets.ToString();
                if (curBullets > 0)
                {
                    if (curBullets > 6)
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
                        Bullets[curBullets - 1].GetComponentInChildren<Image>().enabled = false;
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
            }
        
        }

    }
    /*
    /This method gets called from the PlayerController script, when the player Right-Clicks with their weapon,
    *Or when they've run out of ammo in a single cylinder.
    *Reload the ammo cylinder, reducing the max bullets they can hold by 2.
    *1st Reload = 4 Max Bullets, 2nd Reload = 2 Max Bullets
    *Add/subtract the Bullet sprites accordingly
    */
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
        healthSlider.maxValue = playerRef.maxHealth;
        healthSlider.value = playerRef.health;
        curHealth = playerRef.health;
        maxHealth = playerRef.maxHealth;

        if (curHealth <= 0)
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
    }

    public void StartSplatterCoroutine()
    {
        if(gameOver == false && paused == false)
        {
            StartCoroutine(FadeSplatterImage());
        }
    }

    //REFERENCE FOR FADING OUT SPLATTER IMAGE USING COROUTINE
    //Coroutine needed instead of method b/c loop = instant, coroutine = delay 
    //Bunny83's response on this forum: https://answers.unity.com/questions/225438/slowly-fades-from-opaque-to-alpha.html 
    public IEnumerator FadeSplatterImage()
    {
        float fadeTime = 2.0f;
        GetComponent<AudioSource>().PlayOneShot(playerPoisoned);
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
        playerRef.enabled = false;

        //Activate the Game Over Panel during a Game Over 
        gameOverPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(gameOverPanel.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject);
        StaticGameClass.pause = true;
    }
    //Reloads the current scene the player is on 
    public void RestartGame()
    {
        Invoke("RestartAfterDelay", 1f);
    }

    void RestartAfterDelay()
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
        StaticGameClass.pause = true;
        pausePanel.SetActive(false);
        quitAnimator.SetTrigger("Quitting");
        GetComponent<AudioSource>().PlayOneShot(quitConfirm);
        eventSystem.SetSelectedGameObject(quitPanel.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject); //QuitButton is child of the QuitConfirmPanel object
    }

    //Closes the Quit Confirmation Panel to allow players to go back to the regular pause menu 
    public void CloseQuitPanel()
    {
        quitPanel.SetActive(false);
        if (pausePanel.activeSelf == false && gameOverPanel.activeSelf == false)
        {
            pausePanel.SetActive(true);
            eventSystem.SetSelectedGameObject(pausePanel.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject);
        }

        else if (gameOverPanel.activeSelf == true)
        {
            eventSystem.SetSelectedGameObject(gameOverPanel.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject);
        }
        GetComponent<AudioSource>().PlayOneShot(quitCancel);
    }

    //Called on the Quit button of the Pause menu,
    //Quits the entire game (only works in a Build of the game).
    public void QuitGame()
    {
        Application.Quit();
    }
}
