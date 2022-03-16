using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasic : MonoBehaviour
{

    private Vector3 spawnPoint;
    public GameObject player;
    public PlayerController playerController;

    private NavMeshAgent agent;

    public Transform spitObject;
    public GameObject spitPrefab;

    public bool spitting;
    public bool tracking;

    public float health = 2;
    public GameObject coin;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        tracking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        spitObject = transform.Find("spitPoint");

        if (health == 0)
        {
            StopCoroutine(SpitAttack());
        }
    }


    public void EnableAttack()
    {
        if (health > 0)
        {
            spitting = true;

            if (GetComponent<NavMeshAgent>() != null)
            {
                agent = GetComponent<NavMeshAgent>();
            }

            InvokeRepeating("TrackPlayer", 0, 0.25f);

            StartCoroutine(SpitAttack());
        }
    }


    public void HitEnemy(float dmg)
    {
        if (health > 0)
        {
            health -= dmg;
        }
        if (health <= 0)
        {
            spitting = false;
            Instantiate(coin, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }



    public void TrackPlayer()
    {
        agent.destination = player.transform.position;
    }


    public IEnumerator SpitAttack()
    {


       // agent = GetComponent<NavMeshAgent>();
        while (spitting)
        {

            

            spawnPoint = spitObject.transform.position;
            Instantiate(spitPrefab, spawnPoint, spitObject.transform.rotation);
            

            //delay coroutine with random delaytime between attacks
            float delayTime = Random.Range(2.0f, 5.0f);

            yield return new WaitForSeconds(delayTime);
        }


    }


}
