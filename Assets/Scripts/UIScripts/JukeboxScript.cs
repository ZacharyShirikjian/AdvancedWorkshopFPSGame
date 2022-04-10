using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

//Script used to handle the jukebox
public class JukeboxScript : MonoBehaviour //required for OnSelect
{
    //ALGORITHM//
    //Algorithm

    //When button is clicked, set upgrade header => upgrade's header & desc => upgrade desc

    //Change the SELECT text to be "yes or no"

    //If enter is pressed, disable all the other buttons(set interactable = false)

    //After 2-3 seconds, disable the jukebox panel and apply changes(eg if max ammo increases, display that in game & UI)


    //Checks to see if the player previously interacted with this object, if true, don't update UI
    public bool interactedBefore = false;

    //Checks if button has been selected or not, if true then press space to close out of jukebox menu
    public bool selected = false;

    //public bool buttonUsed = false;

    //ATTACHED TO EACH BUTTON IN JUKEBOX,ref to current button selected
    public GameObject currentButton;

    public GameObject exitButton;
    [SerializeField] private TextMeshProUGUI jukeboxHeaderText;
    [SerializeField] private TextMeshProUGUI jukeboxDescText;
    [SerializeField] private TextMeshProUGUI selectPromptText;

    //Reference to Jukebox Panel
    private UITest uiRef;

    //Reference to Canvas group to make all elements not interactable
    //private CanvasGroup canvasGroup;

    //REFERNECE TO PLAYER
    private PlayerController playRef;

    //LIST OF BUTTONS//
    public GameObject[] jukeboxButtons = new GameObject[4];

    void Start()
    {
        playRef = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
        interactedBefore = false;
        jukeboxHeaderText.SetText("");
        jukeboxDescText.SetText("");
        selectPromptText.SetText("SELECT");
        //canvasGroup = GameObject.Find("JukeboxMenu").GetComponent<CanvasGroup>();
        //canvasGroup.interactable = true;
        currentButton = null;
    }

    private void Update()
    {
        jukeboxHeaderText.text = EventSystem.current.currentSelectedGameObject.GetComponent<JukeboxButton>().upgradeName;
        jukeboxDescText.text = EventSystem.current.currentSelectedGameObject.GetComponent<JukeboxButton>().upgradeDescription;
        //UPDATE THE JUKEBOX HEADER/SUBHEADER TEXT WHEN HOVERING OVER A BUTTON//

        /*
        if (currentButton != null && currentButton != exitButton)
        {
            Debug.Log("test");
            jukeboxHeaderText.text = EventSystem.current.currentSelectedGameObject.GetComponent<JukeboxButton>().upgradeName;
            jukeboxDescText.text = EventSystem.current.currentSelectedGameObject.GetComponent<JukeboxButton>().upgradeDescription;
        }

        else if (currentButton == null)
        {
            jukeboxHeaderText.SetText("");
            jukeboxDescText.SetText("");
        }
        */

        //Debug.Log("HEADER NAME IS: " + jukeboxHeaderText.text);

    }

    //FOR CANCELING OUT AN OPTION IN THE JUKEBOX
    public void CancelOption()
    {
        currentButton = null;
        selected = false;
        selectPromptText.SetText("SELECT");
        jukeboxHeaderText.SetText("");
        for (int i = 0; i < 4; i++)
        {
            jukeboxButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    //FOR WHEN A BUTTON IN JUKEBOX MENU IS SELECTED
    public void DisableButtons()
    {
        //EventSystem.current.currentSelectedGameObject => currently selected button in the game scene
        //currentButton = EventSystem.current.currentSelectedGameObject;
        //Debug.Log(EventSystem.current.currentSelectedGameObject);
        //Debug.Log(currentButton);
        if (currentButton.GetComponent<JukeboxButton>().buttonUsed == false && selected == true)
        {
            //DISABLE EVERY OTHER BUTTON THAT IS NOT CURRENTLY SELECTED//
            for (int i = 0; i < 4; i++)
            {
                if (jukeboxButtons[i] != currentButton)
                {
                    jukeboxButtons[i].GetComponent<Button>().interactable = false;
                }

                else
                {
                    jukeboxButtons[i].GetComponent<Button>().interactable = true;
                }

            }
        }

        else if(currentButton.GetComponent<JukeboxButton>().buttonUsed == true)
        {
            currentButton = null;
            Debug.Log("BUTTON USED");
            for (int i = 0; i < 4; i++)
            {
                if (jukeboxButtons[i] != currentButton)
                {
                    //If button's not been used before, make sure that button is interactable
                    if(jukeboxButtons[i].GetComponent<JukeboxButton>().buttonUsed == false)
                    {
                        jukeboxButtons[i].GetComponent<Button>().interactable = true;
                    }

                    //IF button's been used, disable that (make multiple disabled at once)
                    else if (jukeboxButtons[i].GetComponent<JukeboxButton>().buttonUsed == true) 
                    {
                        jukeboxButtons[i].GetComponent<Button>().interactable = false;
                    }
                }

                else if (jukeboxButtons[i] == currentButton)
                {
                    jukeboxButtons[i].GetComponent<Button>().interactable = false;
                }

            }

        }

       
    }

    //MODS//

    //TO DO: MODDING OF WEAPON WILL BE ADDED LATER//
    //THESE ARE PLACEHOLDER MODS TO ENSURE BUTTONS WILL WORK//

    //CHANGE EFFECT OF PLAYER'S GUN TO BE LIKE SHOTGUN (ADD LATER)
    public void ShotgunMod()
    {
        if (selected == true)
        {
            Debug.Log("SHOTGUN MOD ADDED");
            uiRef.modIcon.sprite = uiRef.modIcons[2].sprite;
            JukeboxButtonSelected();
        }

        else if (selected == false)
        {
            JukeboxButtonSelected();
        }

    }

    //SLOWS DOWN ENEMY SPEED WHEN SHOOTING BULLETS
    public void SlowSpeedMod()
    {
        if (selected == true)
        {
            Debug.Log("ELECTRICITY MOD ADDED");
            uiRef.modIcon.sprite = uiRef.modIcons[1].sprite;
            JukeboxButtonSelected();
        }

        else if (selected == false)
        {
            JukeboxButtonSelected();
        }
    }


    //SLOWS DOWN ENEMY SPEED WHEN SHOOTING BULLETS
    public void FreezeEnemyMod()
    {
        if (selected == true)
        {
            Debug.Log("FREEZE MOD ADDED");
            uiRef.modIcon.sprite = uiRef.modIcons[0].sprite;
            JukeboxButtonSelected();
        }

        else if (selected == false)
        {
            JukeboxButtonSelected();
        }
    }

    //SLOWS DOWN ENEMY SPEED WHEN SHOOTING BULLETS
    public void FireballMod()
    {
        if (selected == true)
        {
            Debug.Log("FIRE AMMO MOD ADDED");
            uiRef.modIcon.sprite = uiRef.modIcons[3].sprite;
            JukeboxButtonSelected();
        }

        else if (selected == false)
        {
            JukeboxButtonSelected();
        }
    }
    ///UPGRADES/REFILLS///

    //CALL HEALTH PACK METHOD OF PLAYER TO HEAL PLAYER (VALUE TBD, LET'S SAY 50% FOR NOW)
    public void HealPlayer()
        {
            if(selected == true)
            {
                playRef.Heal(playRef.maxHealth);
                uiRef.UpdateHealthUI();
                JukeboxButtonSelected();
            }

            else if(selected == false)
            {
                JukeboxButtonSelected();
            }
        }

    //RELOAD 2 BULLETS (IF NOT AT 6 ALREADY)
    public void ReloadAmmo()
    {
        if (selected == true)
        {
            playRef.ammo = playRef.ammo + 2;

            if (playRef.ammo > playRef.maxAmmo)
            {
                playRef.ammo = playRef.maxAmmo;
            }

            if(playRef.ammo >= 6)
            {
                if (playRef.maxAmmo == 6)
                {
                    uiRef.backupBullets = 6;

                }
                else if (playRef.maxAmmo == 4)
                {
                    uiRef.backupBullets = 2;
                }

                else if(playRef.maxAmmo == 2)
                {
                    uiRef.backupBullets = 0;
                }
            }
            uiRef.UpdateAmmoUI();
            JukeboxButtonSelected();
        }

        else if (selected == false)
        {
            JukeboxButtonSelected();
        }

    }
        
    public void MaxHealthUp()
    {

        if (selected == true)
        {
            playRef.maxHealth += 100;
            playRef.health = playRef.maxHealth;
            //When upgrading health, increase size of max Health UI to be 0.25f more than before
            uiRef.healthSlider.transform.localScale += new Vector3(0.25f, 0f, 0f);
            uiRef.UpdateHealthUI();
            JukeboxButtonSelected();
        }

        else if (selected == false)
        {
            JukeboxButtonSelected();
        }
    }

     public void MaxAmmoUp()
     {
        if(selected == true)
        {
            playRef.maxAmmo += 2;
            playRef.ammo = playRef.maxAmmo;
            uiRef.extraBullets = (int) playRef.maxAmmo - 6;
            uiRef.backupBullets += 2;
            uiRef.Reload();
            JukeboxButtonSelected();
        }

        else if (selected == false)
        {
            JukeboxButtonSelected();
        }
     }


    /// MODS//// (ADD LATER ONCE WE HAVE MODS)

    //This method gets called when jukebox is active & button in jukebox is pressed
    public void JukeboxButtonSelected()
    {
        Debug.Log("SDCVASDCASCS");
        if (selected == false)
        {
            currentButton = EventSystem.current.currentSelectedGameObject;
            Debug.Log(EventSystem.current.currentSelectedGameObject);
            Debug.Log(currentButton);
            if (uiRef.numCoins >= currentButton.GetComponent<JukeboxButton>().cost)
            {
                selectPromptText.SetText("Is this okay?");
                selected = true;
            }
            else if(uiRef.numCoins < currentButton.GetComponent<JukeboxButton>().cost)
            {
                selectPromptText.SetText("Not enough coins");
            }


        }

        else if (selected == true)
        {

            //EventSystem.current.currentSelectedGameObject = exitButton;
            //Debug.Log(EventSystem.current.currentSelectedGameObject);
            //Debug.Log(currentButton);
            selectPromptText.SetText("SELECTED");
            uiRef.numCoins = uiRef.numCoins - currentButton.GetComponent<JukeboxButton>().cost;
            //currentButton.GetComponent<Button>().interactable = false;

            //If you selected a mod button, disable the other mod buttons
            if(currentButton.CompareTag("ModButton"))
            {
                for (int i = 0; i < 4; i++)
                {
                    jukeboxButtons[i].GetComponent<Button>().interactable = false;
                }
            }

            //If you selected an upgrade/refill button, just disable that current button
            else if(currentButton.CompareTag("RefillButton"))
            {
                currentButton.GetComponent<Button>().interactable = false;
            }
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(exitButton);

            currentButton.GetComponent<JukeboxButton>().buttonUsed = true;
            selected = false;

            //CHANGES TEXT FROM SELECTED TO SELECT IN 1 SECOND//
            Invoke("ChangeSelectPromptText", 1f);

            //canvasGroup.interactable = false;
            //TO DO: ADD CHA-CHING SFX 
            //Time.timeScale = 1f;
            ////canvasGroup.interactable = false;
            //closeJukeboxMenu();
        }


    }

    public void ChangeSelectPromptText()
    {
        selectPromptText.SetText("SELECT UPGRADE");

        //TO-DO: CHANGE TO "SELECT MODS" ONCE MOD SCREEN POPS UP
    }

    //Closes out of the Jukebox after 1 second
    public void closeJukeboxMenu()
    {
        selectPromptText.SetText("CLOSING...");
        Invoke("closeJukeboxMenuDelay", 1f);
    }

    public void closeJukeboxMenuDelay()
    {
        //DISABLE EVERY BUTTON IN THE JUKEBOX MENU TEMPORARILY//
        selectPromptText.SetText("SELECT");
        currentButton = null;
        selected = false;
        uiRef.paused = false;

        jukeboxHeaderText.SetText("");
        for (int i = 0; i < 4; i++)
        {
            jukeboxButtons[i].GetComponent<Button>().interactable = true;
        }
        uiRef.jukeboxOpen = false;
        uiRef.JukeboxUI();
        Debug.Log("Jukebox menu closing...");
    }

}
