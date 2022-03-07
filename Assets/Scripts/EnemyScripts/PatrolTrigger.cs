using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolTrigger : MonoBehaviour
{

    public EnemyPatrol patrol;
    
    private EnemyAttack attack;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        patrol = enemy.GetComponent<EnemyPatrol>();
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
            
            patrol.patrol = false;
            attack.spitting = true;
            //attack.tracking = true;
            StartCoroutine(attack.SpitAttack());


        }
    }

}
