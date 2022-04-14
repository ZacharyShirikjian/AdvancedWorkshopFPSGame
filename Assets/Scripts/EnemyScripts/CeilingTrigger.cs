using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CeilingTrigger : Trigger
{
    public GameObject platform;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = enemy.GetComponent<EnemyBasic>();
        triggerBox = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ActivateTriggerAction()
    {
        platform.SetActive(false);
        StartCoroutine(ActivateEnemy());
    }

    IEnumerator ActivateEnemy()
    {
        yield return new WaitForSeconds(3.0f);

        NavMeshAgent enemyNMA_ = enemy.AddComponent<NavMeshAgent>();

        baseEnemy.EnableAttack();

    }

}
