
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
  
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        {
            curCursorPos = new Vector2(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).position.x, EventSystem.current.currentSelectedGameObject.transform.position.y);
            gunCursor.transform.position = curCursorPos;
        }

    }
}