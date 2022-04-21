using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchUI : MonoBehaviour
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
        Debug.Log(UITest.instance.currentControlScheme);
        if(UITest.instance.currentControlScheme == UITest.CurrentController.KEYBOARD)
        {
            uiImage.sprite = keyboardImage;
        }

        else if (UITest.instance.currentControlScheme == UITest.CurrentController.GAMEPAD)
        {
            uiImage.sprite = controllerImage;
        }
    }
}
