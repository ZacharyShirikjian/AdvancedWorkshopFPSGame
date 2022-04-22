using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

//Script used to handle the jukebox
public class JukeboxScript : MonoBehaviour //required for OnSelect
{
    //REFERENCE TO AUDIO SOURCE//
    [SerializeField] private AudioSource audiSource;
    [SerializeField] private AudioClip openMenu;
    [SerializeField] private AudioClip closeMenu;
    [SerializeField] private AudioClip selectSFX;
    [SerializeField] private AudioClip areYouSureSFX;
    [SerializeField] private AudioClip cantAfford;
    [SerializeField] private AudioClip cancelSFX;

    //Checks to see if the player previously interacted with this object, if true, don't update UI
    public bool interactedBefore = false;

    //Checks if button has been selected or not, if true then press space to close out of jukebox menu
    public bool selected = false;

    //Did player press Enter/A on an option once? If so, set = true (only time backspace can be used)
    public bool purchasing = false;

    //ATTACHED TO EACH BUTTON IN JUKEBOX,ref to current button selected
    public GameObject currentButton;

    public GameObject exitButton;
    [SerializeField] private TextMeshProUGUI jukeboxHeaderText;
    [SerializeField] private TextMeshProUGUI jukeboxDescText;
    [SerializeField] private TextMeshProUGUI selectPromptText;

    //Reference to Jukebox Panel
    private UITest uiRef;

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
        currentButton = null;
        audiSource.PlayOneShot(openMenu);
    }

    private void Update()
    {
        jukeboxHeaderText.text = EventSystem.current.currentSelectedGameObject.GetComponent<JukeboxButton>().upgradeName;
        jukeboxDescText.text = EventSystem.current.currentSelectedGameObject.GetComponent<JukeboxButton>().upgradeDescription;
        //UPDATE THE JUKEBOX HEADER/SUBHEADER TEXT WHEN HOVERING OVER A BUTTON//
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
        audiSource.PlayOneShot(cancelSFX);
    }

    //FOR WHEN A BUTTON IN JUKEBOX MENU IS SELECTED
    public void DisableButtons()
    {
        //EventSystem.current.currentSelectedGameObject => currently selected button in the game scene
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
                if(playRef.health >= playRef.maxHealth)
                {
                    selectPromptText.SetText("Health Already Full");
                    audiSource.PlayOneShot(cantAfford);
                    selected = false;
                    Invoke("ChangeSelectPromptText", 1f);
                }

                else if(playRef.health < playRef.maxHealth)
                {
                    JukeboxButtonSelected();
                }

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
            if (playRef.ammo != 0 && playRef.ammo >= playRef.maxAmmo)
            {
                selectPromptText.SetText("Ammo Maxed Out");
                audiSource.PlayOneShot(cantAfford);
                selected = false;
                Invoke("ChangeSelectPromptText", 1f);
            }

            else if (playRef.ammo < playRef.maxAmmo)
            {
                JukeboxButtonSelected();
            }
        }
    }
        
    public void MaxHealthUp()
    {
        if (selected == true)
        {
            playRef.maxHealth += 100;
            playRef.health = playRef.maxHealth;
            Debug.Log(playRef.health);
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
                audiSource.PlayOneShot(areYouSureSFX);
                purchasing = true;
                selected = true;
                DisableButtons();
            }
            else if(uiRef.numCoins < currentButton.GetComponent<JukeboxButton>().cost)
            {
                selectPromptText.SetText("Not enough coins");
                audiSource.PlayOneShot(cantAfford);
                Invoke("ChangeSelectPromptText", 1f);
            }


        }

        else if (selected == true)
        {
            selectPromptText.SetText("SELECTED");
            uiRef.numCoins = uiRef.numCoins - currentButton.GetComponent<JukeboxButton>().cost;
            uiRef.coinText.SetText(uiRef.numCoins.ToString());
            currentButton.GetComponent<Button>().interactable = false;
            audiSource.PlayOneShot(selectSFX);
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(exitButton);

            currentButton.GetComponent<JukeboxButton>().buttonUsed = true;
            selected = false;
            purchasing = false;
            interactedBefore = true;
            DisableButtons();
            //CHANGES TEXT FROM SELECTED TO SELECT IN 1 SECOND//
            Invoke("ChangeSelectPromptText", 0.5f);
        }


    }

    public void ChangeSelectPromptText()
    {
        selectPromptText.SetText("SELECT UPGRADE");
    }

    //Closes out of the Jukebox after 2 seconds
    public void closeJukeboxMenu()
    {
        selectPromptText.SetText("CLOSING...");
        audiSource.PlayOneShot(closeMenu);
        Invoke("closeJukeboxMenuDelay", 2f);
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
