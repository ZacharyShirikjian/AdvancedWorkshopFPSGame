using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolTrigger : Trigger
{

    public EnemyPatrol patrol;
    

    // Start is called before the first frame update
    void Start()
    {
        patrol = enemy.GetComponent<EnemyPatrol>();
        baseEnemy = enemy.GetComponent<EnemyBasic>();
        triggerBox = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateTriggerAction()
    {
        patrol.patrol = false;

        baseEnemy.EnableAttack();

    }

}
