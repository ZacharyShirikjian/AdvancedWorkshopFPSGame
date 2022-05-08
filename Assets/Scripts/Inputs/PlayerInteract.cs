using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    //for audio fading in
    public bool alreadyPlaying = false;

    public GameObject currentInteractable = null;
    public Interactables interactableScript = null;
    public GameObject curJukebox = null;
    public JukeboxScript curJukeboxScript = null;
    public bool canInteract;
    //Reference to UI
    [SerializeField] private UITest uiRef;
    //Reference to Camera's Script
    [SerializeField] private CameraLerpMovement cameraScript;

    //REFERENCE TO COMBAT TRACK
    [SerializeField] private AudioClip combatTrack;
    [SerializeField] private AudioClip exploreTrack;
    [SerializeField] private AudioClip combatIntro;
    // Start is called before the first frame update
    void Start()
    {
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
    }

    // Update is called once per frame
    void Update()
    {

        ////CHANGE TO NEW INPUT SYSTEM
        //if (Input.GetKeyDown(KeyCode.Space) && currentInteractable && canInteract == true)
        //{
        //    canInteract = false;
        //    interactableScript.interactedBefore = true;
        //    //currentInteractable.GetComponent<TestInteractableScript>().enabled = true;
        //    uiRef.UpdateInteractPromptUI("");
        //}
    }

    //CALLED W/ INPUT MANAGER TO INTERACT OR BRING UP JUKEBOX MENU/
    public void Interact()
    {
        //FOR OTHER INTERACTABLES, ETC
        if(canInteract && currentInteractable != null)
        {
            Debug.Log("[DOOR]");
            canInteract = false;
            interactableScript.interactedBefore = true;
            interactableScript.enabled = true;
            interactableScript.doorOpen = true;
            //interactableScript.StartFadeCoroutine();
            interactableScript.TriggerDoor();
            uiRef.UpdateInteractPromptUI("");
            Invoke("CloseDoorAfterDelay", 5f);
            //TO DO: call the specific interactable script to do specific action (eg climb, heal, etc)
        }

        //FOR JUKEBOX MENU//
        //Open up Jukebox Menu, disable player movement
        else if(canInteract && curJukebox != null)
        {
            Debug.Log("[JUKEBOX]");
            canInteract = false;
            //curJukeboxScript.interactedBefore = true;
            cameraScript.StartCoroutine(cameraScript.ZoomCamera());
            curJukebox.GetComponent<AudioSource>().Play();
            Invoke("OpenJukeboxAfterDelay", 1f);

        }
    }

    public void CloseDoorAfterDelay()
    {
        interactableScript.doorOpen = false;
    }

    public void OpenJukeboxAfterDelay()
    {
        //cameraScript.enabled = false;
        cameraScript.zoomingIn = false;
        curJukeboxScript.enabled = true;
        uiRef.jukeboxOpen = true;
        uiRef.JukeboxUI();
    }
    //If player enters trigger zone of interactable object
    //Call UITest's InteractPrompt method,
    //And update that text to be what that interactable is

    //RE USE SAME METHOD FOR INTERACTING W/ JUKEBOXES//
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Exploration")
        {
            Debug.Log("FADING AUDIO IN");
            AudioSource otherAudio = other.gameObject.GetComponent<AudioSource>();
            StartCoroutine(AudioTools.FadeInToVol(otherAudio, 2.0f, 1.0f));
            otherAudio.Play();
        }

        //ADD COMBAT TRIGGER ONCE NUM ENEMIES VARIABLE IS CREATED//
        if (other.gameObject.tag == "Combat" && uiRef.numEnemies > 0) 
        {
            Debug.Log("FADING COMBAT IN");
            AudioSource otherAudio = other.gameObject.GetComponent<AudioSource>();
            otherAudio.PlayOneShot(combatIntro);
            otherAudio.clip = combatTrack;
            StartCoroutine(AudioTools.FadeInToVol(otherAudio, 2.0f, 1.0f));
            otherAudio.Play();

        }
        if (other.gameObject.CompareTag("Coin"))
        {
            Debug.Log("COIN PICKUP");
            //other.gameObject.GetComponent<AudioSource>().Play();
        }

        if (other.gameObject.CompareTag("Door"))
        {
            Debug.Log("[DOOR]");
            if (other.gameObject.GetComponent<Interactables>().interactedBefore == false)
            {
                currentInteractable = other.gameObject;
                interactableScript = currentInteractable.GetComponent<Interactables>();
                canInteract = true;
                uiRef.UpdateInteractPromptUI(other.gameObject.GetComponent<Interactables>().actionPrompt);
            }

            if (other.gameObject.GetComponent<Interactables>().interactedBefore == true)
            {
                currentInteractable = other.gameObject;
                interactableScript = currentInteractable.GetComponent<Interactables>();
                canInteract = false;
            }
        }

        //if (other.gameObject.CompareTag("Ladder"))
        //{
        //    Debug.Log("[LADDER]");
        //    if (other.gameObject.GetComponent<TestInteractableScript>().interactedBefore == false)
        //    {
        //        currentInteractable = other.gameObject;
        //        interactableScript = currentInteractable.GetComponent<Interactables>();
        //        canInteract = true;
        //        uiRef.UpdateInteractPromptUI(other.gameObject.GetComponent<TestInteractableScript>().actionPrompt);
        //    }

        //    if (other.gameObject.GetComponent<TestInteractableScript>().interactedBefore == true)
        //    {
        //        currentInteractable = other.gameObject;
        //        interactableScript = currentInteractable.GetComponent<Interactables>();
        //        canInteract = false;
        //    }
        //}
        if (other.gameObject.CompareTag("Jukebox"))
        {
            Debug.Log("[JUKEBOX]");
            if (other.gameObject.GetComponent<JukeboxScript>().interactedBefore == false)
            {
                curJukebox = other.gameObject;
                uiRef.curJukebox = curJukebox;
                curJukeboxScript = curJukebox.GetComponent<JukeboxScript>();

                canInteract = true;
                uiRef.UpdateInteractPromptUI("Use Jukebox");
            }

            else if (other.gameObject.GetComponent<JukeboxScript>().interactedBefore == true)
            {
                curJukebox = other.gameObject;
                uiRef.curJukebox = curJukebox;
                curJukeboxScript = curJukebox.GetComponent<JukeboxScript>();
                canInteract = false;
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Combat" && uiRef.numEnemies <= 0 && alreadyPlaying == false)
        {
            Debug.Log("FADING BACK TO EXPLORATION TRACK");
            AudioSource otherAudio = other.gameObject.GetComponent<AudioSource>();

            //StartCoroutine(AudioTools.FadeOut(otherAudio, 2.0f));
            otherAudio.clip = exploreTrack;
            StartCoroutine(AudioTools.FadeIn(otherAudio, 2.0f));
            otherAudio.Play();
            alreadyPlaying = true;
        }
    }
    //If player leaves trigger zone of interactable object
    //Call UITest's InteractPrompt method,
    //And update that text to be blank
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Combat")
        {
            Debug.Log("FADING COMBAT IN");
            alreadyPlaying = false;
        }

        if (other.gameObject.tag == "Exploration")
        {
            Debug.Log("FADING AUDIO OUT");
            AudioSource otherAudio = other.gameObject.GetComponent<AudioSource>();

            StartCoroutine(AudioTools.FadeOut(otherAudio, 2.0f));
        }

        if (other.CompareTag("Door"))
        {
            canInteract = false;
            uiRef.UpdateInteractPromptUI("");
            if (other.gameObject == currentInteractable)
            {
                currentInteractable = null;
                interactableScript = null;
            }
        }

        //if (other.CompareTag("Ladder"))
        //{
        //    canInteract = false;
        //    uiRef.UpdateInteractPromptUI("");
        //    if (other.gameObject == currentInteractable)
        //    {
        //        currentInteractable = null;
        //        interactableScript = null;
        //    }
        //}

        if (other.CompareTag("Jukebox"))
        {
            canInteract = false;
            uiRef.UpdateInteractPromptUI("");
            if (other.gameObject == curJukebox)
            {
                Debug.Log("CUR STATE " + curJukebox);
                curJukebox = null;
                curJukeboxScript = null;
            }
        }
    }
}
