using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolTrigger : MonoBehaviour
{

    public EnemyPatrol patrol;
    
    private EnemyBasic baseEnemy;
  
    public GameObject enemy;

    public bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        patrol = enemy.GetComponent<EnemyPatrol>();
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

                patrol.patrol = false;

                baseEnemy.EnableAttack();

                triggered = true;
            }
        }
        

    }

}
