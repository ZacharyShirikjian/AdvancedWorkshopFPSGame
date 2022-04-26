using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{

    public bool interactedBefore;
    public bool doorOpen;

    public GameObject pointA;
    public GameObject pointB;

    public GameObject player;

    public string actionPrompt;
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
        yield return new WaitForSeconds(2.5f);
        player.transform.position = pointA.transform.position;
        //fade back in, then fade to black again
        yield return new WaitForSeconds(2.5f);
        player.transform.position = pointB.transform.position;
        //fade back in
        yield return new WaitForSeconds(2.5f);

        StaticGameClass.pause = false;
    }


}
