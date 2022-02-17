using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] public static int volume = 100;
    [SerializeField] public static int sensitivity = 5;

    public static void ChangeVolume(int value)
    {
        volume = value;
    }

    public static void ChangeSensitivity(int value)
    {
        sensitivity = value;
    }
}
