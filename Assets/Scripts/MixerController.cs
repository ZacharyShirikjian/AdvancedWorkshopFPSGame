using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//From SpeedTutor
//https://www.youtube.com/watch?v=C1gCOoDU29M&ab_channel=SpeedTutor
public class MixerController : MonoBehaviour
{
    //Reference to AudioMixer
    [SerializeField] private AudioMixer mainAudioMixer;

    public void SetSFXVolume(float sliderValue)
    {
        //Adjust volume of MasterVolume Mixer based on value of slider volume
        //Converts SliderValue to Logarithmic value of Base 10 (in DB)
        mainAudioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
        Debug.Log(Mathf.Log10(sliderValue) * 20);
    }
}
