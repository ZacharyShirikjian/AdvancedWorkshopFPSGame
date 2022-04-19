
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MoveCursor : MonoBehaviour
{
    [SerializeField] private GameObject gunCursor;
    [SerializeField] private EventSystem eventSystem;
    private Vector2 curCursorPos;

    // Start is called before the first frame update
    void Start()
    {
        //curCursorPos = gunCursor.transform.position;
        //if (SceneManager.GetActiveScene().name == "_TitleScreenScene")
        //{
        //    gunCursor.GetComponent<Animator>().SetBool("InGame", false);
        //    gunCursor.GetComponent<Animator>().SetBool("InTitle", true);
        //    //gunCursor.GetComponent<Animator>().SetBool("TEST", true);

        //}
        //else if (SceneManager.GetActiveScene().name == "_PrototypeDemoScene")
        //{
        //    gunCursor.GetComponent<Animator>().SetBool("InGame", true);
        //    gunCursor.GetComponent<Animator>().SetBool("InTitle", false);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        curCursorPos = new Vector2(EventSystem.current.currentSelectedGameObject.transform.position.x - 40, EventSystem.current.currentSelectedGameObject.transform.position.y + 5);
        gunCursor.transform.position = curCursorPos;
    }

    //public void SwitchCursorAnimation(string anim)
    //{
    //    if (anim.Equals("Quit"))
    //    {
    //        gunCursor.GetComponent<Animator>().SetBool("QuitPanel", true);
    //    }

    //    else if (anim.Equals("Jukebox"))
    //    {
    //        curCursorPos = new Vector2(curCursorPos.x, EventSystem.current.currentSelectedGameObject.transform.position.y);
    //        gunCursor.transform.position = curCursorPos;
    //        gunCursor.GetComponent<Animator>().SetBool("JukeboxOpen", true);
    //        gunCursor.GetComponent<Animator>().SetBool("QuitPanel", false);

    //    }

    //    else if (anim.Equals("Pause"))
    //    {
    //        gunCursor.GetComponent<Animator>().SetBool("JukeboxOpen", false);
    //        gunCursor.GetComponent<Animator>().SetBool("QuitPanel", false);
    //    }

    //}
}