using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyParent : MonoBehaviour
{
    //both variables set by child classes
    public float health = 2;
    public float dmg;

    public GameObject player;
    public PlayerController playerController;

    private NavMeshAgent agent;

    private float distance;
    [SerializeField] float attackRadius = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = player.transform.position;

        float distance = Vector3.Distance(this.transform.position, player.transform.position);

    }

    IEnumerator SpitAttack()
    {

        //run random int to decide if aim is accurate

        int aim = Random.Range(0, 3);

        //run animation here - animation plays whether attack hits or not

        //if aim == 0, run damage calculation
        //if aim != 0, no damage done; attack missed

        if (aim == 0)
        {
            //damage calculation here
            int dmg = Random.Range(3, 10);
            playerController.TakeDamage(dmg);

            Debug.Log("Dmg:" + dmg);
        }


        //delay coroutine with random delaytime between attacks
        float delayTime = Random.Range(2.0f, 5.0f);

        yield return new WaitForSeconds(delayTime);


    }


    public void HitEnemy(float dmg)
    {
        if (health > 0)
        {
            health -= dmg;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


}

