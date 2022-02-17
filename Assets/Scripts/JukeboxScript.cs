using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script used to handle the jukebox
public class JukeboxScript : MonoBehaviour
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

    [SerializeField] private TextMeshProUGUI jukeboxHeaderText;
    [SerializeField] private TextMeshProUGUI selectPromptText;

    //Reference to Jukebox Panel
    private UITest uiRef;

    ////Reference to Player
    //[SerializeField] private ZachPlayerController playerControlRef;


    void Start()
    {
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
        interactedBefore = false;
        jukeboxHeaderText.SetText("");
        selectPromptText.SetText("S E L E C T");
        //playerControlRef = GameObject.FindWithTag("Player").GetComponent<ZachPlayerController>();
    }

    private void Update()
    {
        //If enter is pressed, disable all of the other buttons//
        if(Input.GetKeyDown(KeyCode.Space) && selected == true)
        {
            selectPromptText.SetText("SELECTED");
            //TO DO: ADD CHA-CHING SFX 
            StartCoroutine(closeJukeboxMenu());
        }
    }

    //This method gets called when jukebox is active & button in jukebox is pressed
    public void JukeboxButtonSelected()
    {
        jukeboxHeaderText.SetText("");
        selectPromptText.SetText("Is this ok?");
        selected = true;
    }

    //Coroutine for closing out of jukebox menu after a second (temp)
    public IEnumerator closeJukeboxMenu()
    {
        yield return new WaitForSeconds(2.0f);
        //TO DO: FIND EVERY OTHER BUTTON THAT WASN'T SELECTED & SET INTERACTIVE = FALSE TO DISABLE THEM
        uiRef.closeJukeboxUI();
    }

}
