using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{

    private Vector3 spawnPoint;
    public GameObject player;
    public PlayerController playerController;

    private NavMeshAgent agent;

    public Transform spitObject;
    public GameObject spitPrefab;

    public bool spitting;
    public bool tracking;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        //agent = GetComponent<NavMeshAgent>();
        tracking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        spitObject = transform.Find("spitPoint");


    }


    public IEnumerator SpitAttack()
    {
        agent = GetComponent<NavMeshAgent>();
        while (spitting)
        {

            agent.destination = player.transform.position;

            spawnPoint = spitObject.transform.position;
            Instantiate(spitPrefab, spawnPoint, transform.rotation);
            

            //delay coroutine with random delaytime between attacks
            float delayTime = Random.Range(2.0f, 5.0f);

            yield return new WaitForSeconds(delayTime);
        }


    }



}
