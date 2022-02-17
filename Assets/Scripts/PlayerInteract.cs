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

        if (Input.GetKeyDown(KeyCode.Space) && currentInteractable && canInteract == true)
        {
            canInteract = false;
            interactableScript.interactedBefore = true;
            //currentInteractable.GetComponent<TestInteractableScript>().enabled = true;
            uiRef.UpdateInteractPromptUI("");
        }

        else if (Input.GetKeyDown(KeyCode.Space) && curJukebox && canInteract == true)
        {
            canInteract = false;
            curJukeboxScript.interactedBefore = true;
            curJukeboxScript.enabled = true;
            uiRef.JukeboxUI();
            uiRef.UpdateInteractPromptUI("");
        }
    }


    //If player enters trigger zone of interactable object
    //Call UITest's InteractPrompt method,
    //And update that text to be what that interactable is
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Debug.Log("test");
            if(other.gameObject.GetComponent<TestInteractableScript>().interactedBefore == false)
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
            if(other.gameObject == currentInteractable)
            {
                currentInteractable = null;
                interactableScript = null;
            }
        }
    }
}
