using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{

    public bool interactedBefore;

    Controls.MovementActions movement;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        interactedBefore = false;
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Door")
        {
            StartCoroutine(OpenDoor());
        }

        if (gameObject.tag == "Ladder")
        {
            StartCoroutine(ClimbLadder());
        }

    }

    IEnumerator OpenDoor()
    {
        movement.Disable();
        animator.SetTrigger("OpenTheDoor");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        movement.Enable();
    }


    IEnumerator ClimbLadder()
    {
        movement.Disable();
        animator.SetTrigger("ClimbTheLadder");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        movement.Enable();
    }

}
