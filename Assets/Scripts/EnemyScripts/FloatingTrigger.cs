using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FloatingTrigger : Trigger
{
    private Vector3 spawnPoint;
    private Vector3 playerPoint;
    public GameObject player;
    public Rigidbody rb;
    public float journeyTime = 3.0f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = enemy.GetComponent<EnemyBasic>();
        triggerBox = gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = enemy.transform.position;
        rb = enemy.GetComponent<Rigidbody>();
    }


    public override void ActivateTriggerAction()
    {
        playerPoint = new Vector3(player.transform.position.x - 5, player.transform.position.y, player.transform.position.z);
        enemy.SetActive(true);
        startTime = Time.time;
        StartCoroutine(Floating(journeyTime));
    }

    IEnumerator Floating(float time)
    {

        float i = 0;
        float rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            enemy.transform.position = Vector3.Lerp(spawnPoint, playerPoint, i);

            yield return 0;
        }

        rb.useGravity = true;

        StartCoroutine(ActivateEnemy());

    }


    IEnumerator ActivateEnemy()
    {
        yield return new WaitForSeconds(1.5f);

        if (baseEnemy.health > 0)
        {
            NavMeshAgent enemyNMA_ = enemy.AddComponent<NavMeshAgent>();
        }


        baseEnemy.EnableAttack();

    }

}