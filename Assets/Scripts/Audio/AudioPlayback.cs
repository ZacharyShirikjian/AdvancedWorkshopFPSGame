using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayback : MonoBehaviour
{
	AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
		myAudioSource = GetComponent<AudioSource>();
    }

   
	private void OnCollisionEnter(Collision collision)
	{
        if (myAudioSource.volume == 0.0f)
        {
            myAudioSource.volume = 1.0f;
        }
        else
        {
            myAudioSource.volume = 0.0f;
        }

    }
}
