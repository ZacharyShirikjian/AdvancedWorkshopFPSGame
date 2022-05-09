//LMSC-281 Basic Audio Concepts
//Berklee College of Music
//Fall 2018 - Jeanine Cowen

using UnityEngine;
using System.Collections;

public static class AudioTools {

	public static IEnumerator FadeOut (AudioSource audioSource, float fadeTime) {

		float startVolume = audioSource.volume;

		while (audioSource.volume > 0)
		{
			audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

			yield return null;
		}

		audioSource.Stop ();
		//can also use audioSource.Pause();
		audioSource.volume = startVolume;
	}

	public static IEnumerator FadeIn (AudioSource audioSource, float fadeTime) {
		float targetVolume = audioSource.volume;
		float startVolume = targetVolume/fadeTime;
		audioSource.volume = startVolume;

		audioSource.Play ();

		while (audioSource.volume < targetVolume)
		{
			audioSource.volume += startVolume * Time.deltaTime / fadeTime;

			yield return null;
		}
	}

    //public static IEnumerator FadeInToVol(AudioSource audioSource, float fadeTime, float audioVolume)
    //{

    //	float targetVolume = audioVolume;
    //	float startVolume = 0.01f;

    //	while (audioSource.volume < targetVolume)
    //	{
    //		audioSource.volume += startVolume * Time.deltaTime / fadeTime;

    //		yield return null;
    //	}

    public static IEnumerator FadeInToVol(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}