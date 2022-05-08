using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    public GameObject audioCanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (audioCanvas.active)
            {
                audioCanvas.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                audioCanvas.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }
}
