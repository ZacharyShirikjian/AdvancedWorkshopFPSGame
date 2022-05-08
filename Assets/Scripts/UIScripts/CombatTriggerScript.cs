using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEMP SCRIPT FOR COUNTING # OF ENEMIES WHICH ENTER A COMBAT TRIGGER COLLIDER
//IF NUMENEMIES > 0, PLAY THE COMBAT TRACK
//ELSE, PLAY THE EXPLORATION TRACK 

public class CombatTriggerScript : MonoBehaviour
{
    //Ref to UIScript
    private UITest uiRef;
    // Start is called before the first frame update
    void Start()
    {
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            uiRef.numEnemies++;
        }
    }
}
