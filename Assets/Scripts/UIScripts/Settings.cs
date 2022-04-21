using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] public static int volume = 100;
    [SerializeField] public static int musicVolume = 100;

    public static void ChangeVolume(int value)
    {
        volume = value;
    }

    public static void ChangeMusicVolume(int value)
    {
        musicVolume = value;
    }
}
