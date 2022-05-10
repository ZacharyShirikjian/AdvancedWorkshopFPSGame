using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactables : MonoBehaviour
{

    public bool interactedBefore;
    public bool doorOpen;

    public GameObject player;
    public GameObject door;
    //public Camera playerCam;
    //public CharacterController charController;

    //public Animator pAnimator;
    //public Animator d1Animator;
    //public Animator d2Animator;

    public Vector3 startPoint;
    public Vector3 finishPoint;

    public string actionPrompt;

    public float smooth = 5.0f;         //value for smooth object transform during crouch
    public float traverse = 6.0f;


    //REFERENCE TO DOOR//
    [SerializeField] private Image blackImage;

    //Reference to Canvas SFX source//
    private AudioSource source;
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip closeDoor;

    private trainCarsScr trainCarsScript; 
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
        actionPrompt = "Open Door";
        interactedBefore = false;
        player = GameObject.FindGameObjectWithTag("Player");
        door = GameObject.FindGameObjectWithTag("Door");
        finishPoint = door.transform.Find("finishPoint").gameObject.transform.position;
        trainCarsScript = GameObject.Find("TrainStart").GetComponent<trainCarsScr>();
        //playerCam = player.GetComponentInChildren<Camera>();
        //charController = player.GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
        //animator = player.GetComponentInChildren<Animator>();
        blackImage.color = new Color(1, 1, 1, 0); //reset the alpha in the beginning 

        //pAnimator = player.GetComponentInChildren<Animator>();
        //d1Animator = door.transform.Find("Door1").GetComponent<Animator>();
        //d2Animator = door.transform.Find("Door2").GetComponent<Animator>();

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
            trainCarsScript.NextLevel();
            player.transform.position = finishPoint;
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

    /*
    IEnumerator OpenDoor()
    {
        StaticGameClass.pause = true;

        startPoint = playerCam.transform.position;

        yield return new WaitForSeconds(0.5f);
        pAnimator.SetTrigger("OpenTheDoor");
        d1Animator.SetTrigger("OpenTheDoor");
        d2Animator.SetTrigger("OpenTheDoor");

        float time = 0;
        while(time < traverse)
        {
            playerCam.transform.position = Vector3.Lerp(startPoint, finishPoint, time / traverse);
            time += Time.deltaTime;
            yield return null;
        }

        charController.enabled = false;
        player.transform.position = finishPoint;
        charController.enabled = true;

        StaticGameClass.pause = false;
    }
    */

}
