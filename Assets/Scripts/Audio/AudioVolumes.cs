using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//include the Audio library
using UnityEngine.Audio;

public class AudioVolumes : MonoBehaviour
{

    public AudioMixer myAM;

    public void SetMasterVolume (float masterLvl)
    {
        myAM.SetFloat("masterVol", masterLvl);
    }

    public void SetSFXVolume(float sfxLvl)
    {
        myAM.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicVolume(float musicLvl)
    {
        myAM.SetFloat("musicVol", musicLvl);
    }

    public void SetDiaVolume(float diaLvl)
    {
        myAM.SetFloat("diaVol", diaLvl);
    }

    public void SetAmbVolume(float ambLvl)
    {
        myAM.SetFloat("ambVol", ambLvl);
    }

}
