using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

//Script used to handle the jukebox
public class JukeboxScript : MonoBehaviour, ISelectHandler //required for OnSelect
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

    //ATTACHED TO EACH BUTTON IN JUKEBOX,ref to current button selected
    public GameObject currentButton;

    [SerializeField] private TextMeshProUGUI jukeboxHeaderText;
    [SerializeField] private TextMeshProUGUI selectPromptText;

    //Reference to Jukebox Panel
    private UITest uiRef;

    //Reference to Canvas group to make all elements not interactable
    private CanvasGroup canvasGroup;

    //REFERNECE TO PLAYER
    private PlayerController playRef;

    //LIST OF BUTTONS//
    public GameObject[] jukeboxButtons = new GameObject[4];

    void Start()
    {
        playRef = GameObject.Find("PlayerObject").GetComponent<PlayerController>();
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
        interactedBefore = false;
        jukeboxHeaderText.SetText("");
        selectPromptText.SetText("SELECT");
        canvasGroup = GameObject.Find("JukeboxMenu").GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        currentButton = null;
    }

    private void Update()
    {
    }

    //FOR WHEN A BUTTON IN JUKEBOX MENU IS SELECTED
    public void OnSelect(BaseEventData eventdata)
    {
        currentButton = EventSystem.current.currentSelectedGameObject;
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        currentButton = eventdata.selectedObject;
        Debug.Log(currentButton);
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

    ///UPGRADES/REFILLS///

    //CALL HEALTH PACK METHOD OF PLAYER TO HEAL PLAYER (VALUE TBD, LET'S SAY 50% FOR NOW)
    public void HealPlayer()
        {
            if(selected == true)
            {
                playRef.Heal(playRef.maxHealth / 2);
                uiRef.UpdateHealthUI();
                JukeboxButtonSelected();
            }

            else if(selected == false)
            {
                JukeboxButtonSelected();
                jukeboxHeaderText.SetText("REFILL HEALTH \nRefills 1/3 of your health.");
            }
        }

    //RELOAD 2 BULLETS (IF NOT AT 6 ALREADY)
    public void ReloadAmmo()
    {
        if (selected == true)
        {
            playRef.ammo = playRef.ammo + 2;

            //TO DO: CHANGE THIS WHEN RESERVE AMMO IS ADDED
            if (playRef.ammo > 6)
            {
                playRef.ammo = 6;
            }
            uiRef.UpdateAmmoUI();
            JukeboxButtonSelected();
        }

        else if (selected == false)
        {
            JukeboxButtonSelected();
            jukeboxHeaderText.SetText("REFILL AMMO \nReloads 2 bullets.");
        }

    }

        //VALUES TO BE CHANGED/DECIDED LATER!!!
        public void MaxHealthUp()
        {
            playRef.maxHealth += 10;
            playRef.health += 5;
            if (selected == false)
            {
                JukeboxButtonSelected();
                jukeboxHeaderText.SetText("UPGRADE HEALTH \nIncreases your maximum health by 10 points.");

            }
        }

        public void MaxAmmoUp()
        {
            playRef.ammo += 2;
            uiRef.curBullets += 2;
            uiRef.maxBullets += 2;
            if (selected == false)
            {
               
                JukeboxButtonSelected();
                jukeboxHeaderText.SetText("UPGRADE HEALTH \nIncreases your maximum ammo by 2.");
            }
        }


    /// MODS//// (ADD LATER ONCE WE HAVE MODS)

    //This method gets called when jukebox is active & button in jukebox is pressed
    public void JukeboxButtonSelected()
    {

        if (selected == false)
        {
            jukeboxHeaderText.SetText("");
            selectPromptText.SetText("Is this ok?");
            selected = true;

        }

        else if (selected == true)
        {
            selectPromptText.SetText("SELECTED");
            //canvasGroup.interactable = false;
            //TO DO: ADD CHA-CHING SFX 
            Time.timeScale = 1f;
            canvasGroup.interactable = false;
            StartCoroutine(closeJukeboxMenu());
        }


    }

    //Coroutine for closing out of jukebox menu after a second (temp)
    public IEnumerator closeJukeboxMenu()
    {
        yield return new WaitForSeconds(1.0f);
        //DISABLE EVERY BUTTON IN THE JUKEBOX MENU TEMPORARILY//
        uiRef.jukeboxOpen = false;
        uiRef.JukeboxUI();
        Debug.Log("Jukebox menu closing...");
    }

}
