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
    public bool active;

    public float health = 2;
    public GameObject coin;

    public Animator animator;

    public float journeyTime = 3.0f;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip enemyDeath; 
     private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        tracking = false;
        active = false;
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
            AudioSource.PlayClipAtPoint(enemyHit, gameObject.transform.position);
        }
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(enemyDeath, gameObject.transform.position);
            spitting = false;
            Instantiate(coin, transform.position, transform.rotation);
            Destroy(gameObject);
            StaticGameClass.LessActiveEnemies();
        }
    }


    public void TrackPlayer()
    {
        if(StaticGameClass.pause == false)
        {
            agent.destination = player.transform.position;
        }
    }


    public IEnumerator SpitAttack()
    {

        while (spitting && StaticGameClass.pause == false)
        {

            spawnPoint = spitObject.transform.position;
            Instantiate(spitPrefab, spawnPoint, spitObject.transform.rotation);
            

            //delay coroutine with random delaytime between attacks
            float delayTime = Random.Range(2.0f, 5.0f);

            yield return new WaitForSeconds(delayTime);
        }

    }


}
