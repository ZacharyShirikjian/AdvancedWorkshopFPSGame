using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Temporary script for updating the UI when the player is near an interactable object.
//When the player enters the trigger zone of an interactable object, update the UI prompt by saying
//"Press [Space] to X", where X is the action defined in the OnTriggerMethod
public class TestInteractableScript : MonoBehaviour
{
    //Specfic action of interactable that gets prompted (eg press space to climb over)
    public string actionPrompt;

    //Checks to see if the player previously interacted with this object, if true, don't update UI
    public bool interactedBefore = false;

    ////Reference to Player
    //[SerializeField] private ZachPlayerController playerControlRef;


    void Start()
    {
        interactedBefore = false;
        //playerControlRef = GameObject.FindWithTag("Player").GetComponent<ZachPlayerController>();
    }

}
