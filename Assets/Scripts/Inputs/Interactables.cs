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
    // Start is called before the first frame update
    void Start()
    {
        doorOpen = false;
        actionPrompt = "Open Door";
        interactedBefore = false;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Door" && doorOpen == true)
        {
            StartCoroutine(OpenDoor());
        }

    }

    IEnumerator OpenDoor()
    {
        StaticGameClass.pause = true;
        //fade to black - lerp
        float fadeTime = 2.0f;
        //GetComponent<AudioSource>().PlayOneShot();
            for (float i = 0; i < 1.0f; i += Time.deltaTime / fadeTime)
            {
                //i = opacity, slowly decrease opacity over time for 1 second
                Color alphaColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, i));
                blackImage.color = alphaColor;
                yield return null;
            }
        yield return new WaitForSeconds(2.5f);
        player.transform.position = pointA.transform.position;
        //fade back in, then fade to black again
        for (float i = 0; i < 1.0f; i += Time.deltaTime / fadeTime)
        {
            Debug.Log("osweahjifoeswfhjsdaofides");
            //i = opacity, slowly decrease opacity over time for 1 second
            Color alphaColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, i));
            blackImage.color = alphaColor;
            yield return null;
        }
        for (float i = 0; i < 1.0f; i += Time.deltaTime / fadeTime)
        {
            Debug.Log("ppppppppppppppp1");
            //i = opacity, slowly decrease opacity over time for 1 second
            Color alphaColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, i));
            blackImage.color = alphaColor;
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);
        player.transform.position = pointB.transform.position;
        //fade back in
        for (float i = 0; i < 1.0f; i += Time.deltaTime / fadeTime)
        {
            Debug.Log("aSDSASdasdsa");
            //i = opacity, slowly decrease opacity over time for 1 second
            Color alphaColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, i));
            blackImage.color = alphaColor;
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);

        StaticGameClass.pause = false;
    }


}
