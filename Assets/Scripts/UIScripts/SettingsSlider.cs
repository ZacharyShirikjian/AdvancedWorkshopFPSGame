using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//UI Script to update the game settings based on what the player chooses
public class SettingsSlider : MonoBehaviour
{
    //VOLUME SLIDER
    public Slider volume_S;
    public TMPro.TMP_Text volumeNum;

    //SENSITIVITY SLIDER
    public Slider sens_S;
    public TMPro.TMP_Text sensNum;

    private void Awake()
    {
        volume_S.value = Settings.volume;
        sens_S.value = Settings.sensitivity;
        UpdateVolume();
        UpdateSensitivity();
    }

    public void UpdateVolume()
    {
        Settings.ChangeVolume((int)volume_S.value);
        volume_S.value = Settings.volume;
        volumeNum.text = volume_S.value.ToString();
    }

    public void UpdateSensitivity()
    {
        Settings.ChangeSensitivity((int)sens_S.value);
        sens_S.value = Settings.sensitivity;
        sensNum.text = sens_S.value.ToString();
    }
}
