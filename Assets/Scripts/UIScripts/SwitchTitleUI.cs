using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchTitleUI : MonoBehaviour
{
    [SerializeField] private Image uiImage;
    [SerializeField] private Sprite keyboardImage;
    [SerializeField] private Sprite controllerImage;
    // Start is called before the first frame update

    //Based on method written by Peter Gnomes//
    void Start()
    {
        //uiImage = GetComponent<Image>();
        ChangeInputImage();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeInputImage();
    }

    //CHANGE IMAGES BASED ON TYPE OF UI USED//
    private void ChangeInputImage()
    {
        if(TitleScreen.instance.currentControlScheme == TitleScreen.CurrentController.KEYBOARD)
        {
            uiImage.sprite = keyboardImage;
        }

        else if (TitleScreen.instance.currentControlScheme == TitleScreen.CurrentController.GAMEPAD)
        {
            uiImage.sprite = controllerImage;
        }
    }
}
