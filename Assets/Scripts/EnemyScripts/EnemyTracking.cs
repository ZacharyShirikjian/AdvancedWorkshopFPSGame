using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTracking : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    public bool tracking;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        while (tracking)
        {
            agent.destination = player.transform.position;
        }
    }
}
