using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    //Reference to this Audio Source
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip titleTrack;
    [SerializeField] private AudioClip gameplayTrack;

    //An instance of this AudioManager script, which is used for the DontDestroyOnLoad method. 
    private static AudioManager instance;

    private void Awake()
    {
        //If there is already an instance of this script in the current scene, then delete this duplicated instance 
        if (instance != null)
        {
            Destroy(gameObject);
        }

        //Otherwise, if there is no other instances of this script in the current scene, then make sure that it doesn't get deleted with DontDestroyOnLoad()
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject); //DontDestroyOnLoad is applied to the current gameObject with the AudioManager script on it 
        }
    }

    //Switch between title screen and gameplay music 
    public void SwitchSong(string songName)
    {
        if(songName == "Title")
        {
            source.clip = titleTrack;
            source.Play();
        }

        else if(songName == "Gameplay")
        {
            source.clip = gameplayTrack;
            source.Play();
        }    
    }
}

