using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{

    public GameObject pointA;
    public GameObject pointB;

    public bool patrol;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrol = true;

        agent.destination = pointA.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (patrol)
        {
            if (other.name == "PointA")
            {
                agent.destination = pointB.transform.position;
            }

            if (other.name == "PointB")
            {
                agent.destination = pointA.transform.position;
            }
        }

    }

}
