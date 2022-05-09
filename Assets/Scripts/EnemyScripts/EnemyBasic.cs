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
    public AudioClip[] enemyDeathSounds = new AudioClip[4];
     private AudioSource source;

    //REFERENCE TO UI SCRIPT//
    private UITest uiRef;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        source = GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        tracking = false;
        active = false;
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
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
            source.PlayOneShot(enemyHit);
        }
        if (health <= 0)
        {
            source.clip = enemyDeathSounds[Random.Range(0, enemyDeathSounds.Length)]; //play random clip from the list of death sounds
            AudioSource.PlayClipAtPoint(source.clip, gameObject.transform.position);
            spitting = false;
            Instantiate(coin, transform.position, transform.rotation);
            uiRef.numEnemies--;
            Invoke("KillEnemy", 0.75f);

        }
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
        StaticGameClass.LessActiveEnemies();
    }


    public void TrackPlayer()
    {
        if(StaticGameClass.pause == false && GetComponent<NavMeshAgent>() != null)
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
