using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CeilingTrigger : MonoBehaviour
{



    private EnemyAttack attack;
    public GameObject enemy;
    public GameObject platform;


    // Start is called before the first frame update
    void Start()
    {
        attack = enemy.GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            platform.SetActive(false);
            StartCoroutine(ActivateEnemy());
           

        }
    }

    IEnumerator ActivateEnemy()
    {
        yield return new WaitForSeconds(3.0f);

        NavMeshAgent enemyNMA_ = enemy.AddComponent<NavMeshAgent>();

        attack.spitting = true;
        //attack.tracking = true;
        StartCoroutine(attack.SpitAttack());

    }

}
