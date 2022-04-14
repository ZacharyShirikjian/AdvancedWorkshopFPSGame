using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    //AudioClips
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip buttonHover;

    //Reference to the SFXManager's AudioSource
    private AudioSource audiSource;


    // Start is called before the first frame update
    void Start()
    {
        audiSource = this.GetComponent<AudioSource>();
    }

    //This method gets called when the player hovers their mouse over a UI button  (in the Pause Menu or Title Screen). 
    //Play the SFX for hovering over a UI button.
    public void PlayButtonHoverSFX()
    {
        audiSource.PlayOneShot(buttonHover);
    }


    //This method gets called when the player clicks on a UI button (in the Pause Menu or Title Screen). 
    //Play the SFX for clicking over a UI button.
    public void PlayButtonClickSFX()
    {
        audiSource.PlayOneShot(buttonClick);
    }
}
