using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactables : MonoBehaviour
{

    public bool interactedBefore;
    public bool doorOpen;

    public GameObject pointA;
    public GameObject pointB;

    public GameObject player;

    public string actionPrompt;

    //REFERENCE TO DOOR//
    [SerializeField] private Image blackImage;

    //Reference to Canvas SFX source//
    private AudioSource source;
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip closeDoor;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
        actionPrompt = "Open Door";
        interactedBefore = false;
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void StartFadeCoroutine()
    {
        if (gameObject.tag == "Door" && doorOpen == true)
        {
            StartCoroutine(FadeToBlack(1f));
            StartCoroutine(FadeIn(5f));

        }
    }
    //Fade to Black
    //Idea for delay: takatok's post on https://forum.unity.com/threads/invoke-with-a-coroutine.439046/
    public IEnumerator FadeToBlack(float delay = 0.0f)
    {
        if(delay != 0)
        {
            yield return new WaitForSeconds(delay);

            float fadeTime = 2.0f;
            Debug.Log("FADE IN SOFAOUFDSAFOSHNFO");
            source.PlayOneShot(openDoor);
            for (float i = 0; i < 1.0f; i += Time.deltaTime / fadeTime)
            {
                //i = opacity, slowly decrease opacity over time for 1 second
                Color alphaColor = new Color(1, 1, 1, i);
                blackImage.color = alphaColor;
                yield return null;
            }

            player.transform.position = pointA.transform.position;
        }

    }

    //Fade In
    public IEnumerator FadeIn(float delay = 0.0f)
    {
        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
            float fadeTime = 2.0f;
            Debug.Log("FADE OUT OAWEFOSNFDSOF");
            source.PlayOneShot(closeDoor);
            for (float i = 1; i > 0f; i -= Time.deltaTime / fadeTime)
            {
                //i = opacity, slowly decrease opacity over time for 1 second
                Color alphaColor = new Color(1, 1, 1, i);
                blackImage.color = alphaColor;
                yield return null;
            }
            player.transform.position = pointB.transform.position;
        }

    }

    IEnumerator OpenDoor()
    {
        StaticGameClass.pause = true;

        //fade back in, then fade to black again
        yield return new WaitForSeconds(2.5f);
        player.transform.position = pointB.transform.position;
        //fade back in
        yield return new WaitForSeconds(2.5f);

        StaticGameClass.pause = false;
    }


}
