using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameClass
{

    public static bool pause { get; set; }

    static int activeEnemies = 0;


    public static int GetActiveEnemies()
    {
        return activeEnemies;
    }

    public static void PlusActiveEnemies(GameObject trigger)
    {
        activeEnemies += 1;
        ActivateTrigger(trigger);

    }

    public static void LessActiveEnemies()
    {
        if (activeEnemies > 0)
        {
                activeEnemies -= 1;
        }

    }

    public static void ResetActiveEnemies()
    {
        activeEnemies = 0;
    }

    public static void ActivateTrigger(GameObject trigger)
    {
        trigger.GetComponent<Trigger>().ActivateTriggerAction();
    }

}