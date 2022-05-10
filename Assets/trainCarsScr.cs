using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainCarsScr : MonoBehaviour
{
    [SerializeField] private GameObject[] AllLevels;
    public int currentLevel;


    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
    }

    public void NextLevel()
    {
        if(currentLevel == 1)
        {
            AllLevels[currentLevel + 2].gameObject.SetActive(true);
        }
        else
        {
            AllLevels[currentLevel - 1].gameObject.SetActive(false);
            AllLevels[currentLevel + 2].gameObject.SetActive(true);
        }
        currentLevel++;
    }
    
}
