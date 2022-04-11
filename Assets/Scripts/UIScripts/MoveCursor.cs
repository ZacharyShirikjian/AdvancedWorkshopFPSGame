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
        curCursorPos = gunCursor.transform.position;
        if (SceneManager.GetActiveScene().name == "_TitleScreenScene")
        {
            gunCursor.GetComponent<Animator>().SetBool("Title", true);
        }
        else
        {
            gunCursor.GetComponent<Animator>().SetBool("Title", false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        curCursorPos = new Vector2(curCursorPos.x, EventSystem.current.currentSelectedGameObject.transform.position.y);
        gunCursor.transform.position = curCursorPos;
    }
}