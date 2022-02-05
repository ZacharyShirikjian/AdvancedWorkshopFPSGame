using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyParent : MonoBehaviour
{
    //both variables set by child classes
    public float health;
    public float dmg;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = player.transform.position;
    }


    public void HitEnemy(float dmg)
    {
        if(health > 0)
        {
            health -= dmg;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeDamage(dmg);
        }
    }

}
