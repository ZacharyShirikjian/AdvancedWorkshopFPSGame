using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//UI Script to update the game settings based on what the player chooses
public class SettingsSlider : MonoBehaviour
{
    //SFX VOLUME SLIDER
    public Slider volume_S;
    public TMPro.TMP_Text volumeNum;

    //SFX VOLUME SLIDER
    public Slider musicVolume_S;
    public TMPro.TMP_Text musicVolumeNum;

    ////SENSITIVITY SLIDER
    //public Slider sens_S;
    //public TMPro.TMP_Text sensNum;

    public GameObject Canvas;

    [SerializeField] private GameObject musicManager;
    [SerializeField] private GameObject sfxManager;

    private void Awake()
    {
        volume_S.value = Settings.volume;
        musicVolume_S.value = Settings.musicVolume;
        UpdateSFXVolume();
        UpdateMusicVolume();
    }

    public void UpdateSFXVolume()
    {
        Settings.ChangeVolume((int)volume_S.value);
        volume_S.value = Settings.volume;
        volumeNum.text = volume_S.value.ToString();
        sfxManager.GetComponent<AudioSource>().volume = ((float)Settings.volume / 100);
        //AudioListener.volume = Settings.volume;
        //Canvas.GetComponent<AudioSource>().volume = ((float) Settings.volume / 100);
        //Debug.Log(AudioListener.volume);
    }

    public void UpdateMusicVolume()
    {
        Settings.ChangeMusicVolume((int)musicVolume_S.value);
        musicVolume_S.value = Settings.musicVolume;
        musicVolumeNum.text = musicVolume_S.value.ToString();
        musicManager.GetComponent<AudioSource>().volume = ((float) Settings.musicVolume / 200); //Divide by 200 to get 1/2 volume
    }

    //public void UpdateSensitivity()
    //{
    //    Settings.ChangeSensitivity((int)sens_S.value);
    //    sens_S.value = Settings.sensitivity;
    //    sensNum.text = sens_S.value.ToString();
    //}
}
