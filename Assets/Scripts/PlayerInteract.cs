using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public GameObject currentInteractable = null;
    public TestInteractableScript interactableScript = null;

    public GameObject curJukebox = null;
    public JukeboxScript curJukeboxScript = null;

    public bool canInteract;

    //Reference to UI
    [SerializeField] private UITest uiRef;

    // Start is called before the first frame update
    void Start()
    {
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
    }

    // Update is called once per frame
    void Update()
    {
        //CHANGE TO NEW INPUT SYSTEM
        if (Input.GetKeyDown(KeyCode.Space) && currentInteractable && canInteract == true)
        {
            canInteract = false;
            interactableScript.interactedBefore = true;
            //currentInteractable.GetComponent<TestInteractableScript>().enabled = true;
            uiRef.UpdateInteractPromptUI("");
        }
    }

    //CALLED W/ INPUT MANAGER TO INTERACT OR BRING UP JUKEBOX MENU/
    public void Interact()
    {
        //FOR OTHER INTERACTABLES, ETC
        if(canInteract && currentInteractable != null)
        {
            Debug.Log("[OBJECT]");
            canInteract = false;
            interactableScript.interactedBefore = true;
            interactableScript.enabled = true;
            uiRef.UpdateInteractPromptUI("");
            //TO DO: call the specific interactable script to do specific action (eg climb, heal, etc)
        }

        //FOR JUKEBOX MENU//
        //Open up Jukebox Menu, disable player movement
        else if(canInteract && curJukebox != null)
        {
            Debug.Log("[JUKEBOX]");
            canInteract = false;
            //curJukeboxScript.interactedBefore = true;
            curJukeboxScript.enabled = true;
            uiRef.jukeboxOpen = true;
            uiRef.JukeboxUI();
        }
    }

    //If player enters trigger zone of interactable object
    //Call UITest's InteractPrompt method,
    //And update that text to be what that interactable is

    //RE USE SAME METHOD FOR INTERACTING W/ JUKEBOXES//
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("COIN PICKUP");
            uiRef.numCoins++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("test");
            if (other.gameObject.GetComponent<TestInteractableScript>().interactedBefore == false)
            {
                currentInteractable = other.gameObject;
                interactableScript = currentInteractable.GetComponent<TestInteractableScript>();
                canInteract = true;
                uiRef.UpdateInteractPromptUI(other.gameObject.GetComponent<TestInteractableScript>().actionPrompt);
            }

            if (other.gameObject.GetComponent<TestInteractableScript>().interactedBefore == true)
            {
                currentInteractable = other.gameObject;
                interactableScript = currentInteractable.GetComponent<TestInteractableScript>();
                canInteract = false;
            }
        }

        if (other.gameObject.CompareTag("Jukebox"))
        {
            Debug.Log("test");
            if (other.gameObject.GetComponent<JukeboxScript>().interactedBefore == false)
            {
                curJukebox = other.gameObject;
                curJukeboxScript = curJukebox.GetComponent<JukeboxScript>();
                canInteract = true;
                uiRef.UpdateInteractPromptUI("use Jukebox");
            }

            else if (other.gameObject.GetComponent<JukeboxScript>().interactedBefore == true)
            {
                curJukebox = other.gameObject;
                curJukeboxScript = curJukebox.GetComponent<JukeboxScript>();
                canInteract = false;
            }
        }


    }

    //If player leaves trigger zone of interactable object
    //Call UITest's InteractPrompt method,
    //And update that text to be blank
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = false;
            uiRef.UpdateInteractPromptUI("");
            if (other.gameObject == currentInteractable)
            {
                currentInteractable = null;
                interactableScript = null;
            }
        }

        if (other.CompareTag("Jukebox"))
        {
            canInteract = false;
            uiRef.UpdateInteractPromptUI("");
            if (other.gameObject == curJukebox)
            {
                curJukebox = null;
                curJukeboxScript = null;
            }
        }
    }
}
