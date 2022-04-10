using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticGameClass
{

    static bool pause;

    static int mobCap = 2;
    static int activeEnemies = 0;

    public static void PauseGame()
    {
        pause = true;
    }


    public static void UnPauseGame()
    {
        pause = false;
    }

    //public static void QueueEnemy(GameObject enemy)
    //{
    //    backlog.Enqueue(enemy);
    //}

    //public static GameObject DequeueEnemy()
    //{
    //    if (backlog.Peek() != null)
    //    {
    //        return backlog.Dequeue();
    //    }
    //}

    //public static void ClearQueue()
    //{
    //    backlog.Clear();
    //}

    //public static int GetActiveEnemies()
    //{
    //    return activeEnemies;
    //}

    //public static void PlusActiveEnemies(GameObject enemy)
    //{
    //    if (activeEnemies < mobCap)
    //    {
    //        activeEnemies += 1;
    //        SetActiveEnemy(enemy);
    //    }

    //    else
    //    {
    //        QueueEnemy(enemy);
    //    }
    //}

    //public static void LessActiveEnemies()
    //{
    //    if (activeEnemies > 0)
    //    {
    //        if (backlog.Peek() == null)
    //        {
    //            activeEnemies -= 1;
    //        }

    //        else
    //        {
    //            SetActiveEnemy(DequeueEnemy());
    //        }

    //    }

    //}

    //public static void ResetActiveEnemies()
    //{
    //    activeEnemies = 0;
    //}

    //public static void SetActiveEnemy(GameObject enemy)
    //{
    //    enemy.GetComponent<EnemyBasic>().active = true;
    //}

}