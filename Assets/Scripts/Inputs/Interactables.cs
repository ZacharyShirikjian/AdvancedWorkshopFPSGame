using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactables : MonoBehaviour
{

    public bool interactedBefore;
    public bool doorOpen;

    public GameObject player;

    public Animator animator;

    public string actionPrompt;

    //REFERENCE TO DOOR//
    [SerializeField] private Image blackImage;

    //Reference to Canvas SFX source//
    private AudioSource source;
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip closeDoor;

    public Transform pointB;
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
        actionPrompt = "Open Door";
        interactedBefore = false;
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        animator = player.GetComponentInChildren<Animator>();
        blackImage.color = new Color(1, 1, 1, 0); //reset the alpha in the beginning 
    }


    public void TriggerDoor()
    {
        //StartCoroutine("OpenDoor");
        StartFadeCoroutine();
    }

    public void StartFadeCoroutine()
    {
        if (gameObject.tag == "Door" && doorOpen == true)
        {
            Debug.Log("FADING IN.....");
            StartCoroutine(FadeToBlack(0.25f));
            StartCoroutine(FadeIn(4f));

        }
    }
    ////Fade to Black
    ////Idea for delay: takatok's post on https://forum.unity.com/threads/invoke-with-a-coroutine.439046/
    public IEnumerator FadeToBlack(float delay = 0.0f)
    {

        //animator.SetTrigger("OpenTheDoor");
        if (delay != 0)
        {
            player.GetComponent<PlayerController>().canMove = false;
            yield return new WaitForSeconds(delay);
            float fadeTime = 1.0f;
            Debug.Log("FADE IN SOFAOUFDSAFOSHNFO");
            //animator.SetTrigger("Idle");
            source.PlayOneShot(openDoor);
            for (float i = 0; i < 1.01f; i += Time.deltaTime / fadeTime)
            {
                //i = opacity, slowly decrease opacity over time for 1 second
                Color alphaColor = new Color(1, 1, 1, i);
                blackImage.color = alphaColor;
                yield return null;
            }
        }

    }

    ////Fade In
    public IEnumerator FadeIn(float delay = 0.0f)
    {
      if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
            float fadeTime = 1.0f;
            Debug.Log("FADE OUT OAWEFOSNFDSOF");
            source.PlayOneShot(closeDoor);
            player.transform.position = pointB.transform.position;
            for (float i = 1; i > 0f; i -= Time.deltaTime / fadeTime)
            {
                //i = opacity, slowly decrease opacity over time for 1 second
                Color alphaColor = new Color(1, 1, 1, i);
                blackImage.color = alphaColor;
                yield return null;
            }
            player.GetComponent<PlayerController>().canMove = true;
        }

    }

    IEnumerator OpenDoor()
    {
        StaticGameClass.pause = true;


        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("OpenTheDoor");
        yield return new WaitForSeconds(6.0f);

        StaticGameClass.pause = false;
    }


}
