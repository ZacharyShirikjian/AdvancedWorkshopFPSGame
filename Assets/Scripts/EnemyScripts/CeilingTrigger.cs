using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CeilingTrigger : MonoBehaviour
{



    private EnemyBasic baseEnemy;
    public GameObject enemy;
    public GameObject platform;

    public bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = enemy.GetComponent<EnemyBasic>();
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!triggered)
        {
            if (other.CompareTag("Player"))
            {
                platform.SetActive(false);
                StartCoroutine(ActivateEnemy());
                triggered = true;

            }
        }


    }

    IEnumerator ActivateEnemy()
    {
        yield return new WaitForSeconds(3.0f);

        NavMeshAgent enemyNMA_ = enemy.AddComponent<NavMeshAgent>();

        baseEnemy.EnableAttack();

    }

}
