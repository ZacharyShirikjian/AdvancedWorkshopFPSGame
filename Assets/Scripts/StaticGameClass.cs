using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameClass
{

    public static bool pause { get; set; }

    static int mobCap = 2;
    static int activeEnemies = 0;

    static Queue<GameObject> backlog;

    public static void QueueTrigger(GameObject trigger)
    {
        backlog.Enqueue(trigger);
    }

    public static GameObject DequeueTrigger()
    {
       return backlog.Dequeue();   
    }

    public static void ClearQueue()
    {
        backlog.Clear();
    }

    public static int GetActiveEnemies()
    {
        return activeEnemies;
    }

    public static void PlusActiveEnemies(GameObject trigger)
    {
        if (activeEnemies < mobCap)
        {
            Debug.Log("Activated");
            activeEnemies += 1;
            ActivateTrigger(trigger);
        }

        else
        {
            Debug.Log("Queued");
            QueueTrigger(trigger);
        }
    }

    public static void LessActiveEnemies()
    {
        if (activeEnemies > 0)
        {
            if (backlog.Peek() == null)
            {
                activeEnemies -= 1;
            }

            else
            {
                Debug.Log("Dequeued");
                ActivateTrigger(DequeueTrigger());
            }

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