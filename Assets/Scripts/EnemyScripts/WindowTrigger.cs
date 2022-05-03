using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WindowTrigger : Trigger
{

    public GameObject PointA;
    public GameObject PointB;

    [SerializeField] Vector3 Point1;
    [SerializeField] Vector3 Point2;
    [SerializeField] Vector3 center;
    [SerializeField] Vector3 upRelative;
    [SerializeField] Vector3 downRelative;

    public float journeyTime = 0.5f;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        CalculateArc();
        baseEnemy = enemy.GetComponent<EnemyBasic>();
        triggerBox = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ActivateTriggerAction()
    {
        startTime = Time.time;

        StartCoroutine(Window(journeyTime));
    }

    public void CalculateArc()
    {
        Point1 = PointA.transform.position;
        Point2 = PointB.transform.position;
        center = (Point1 + Point2) * 0.5f;
        center -= new Vector3(0, 1, 0);

        upRelative = Point1 - center;
        downRelative = Point2 - center;
    }


    IEnumerator Window(float time)
    {
        float i = 0;
        float rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            enemy.transform.position = Vector3.Slerp(upRelative, downRelative, i);
            enemy.transform.position += center;

            yield return 0;
        }

        StartCoroutine(ActivateEnemy());
    }


    IEnumerator ActivateEnemy()
    {
        yield return new WaitForSeconds(2.0f);

        if (baseEnemy.health > 0)
        {
            NavMeshAgent enemyNMA_ = enemy.AddComponent<NavMeshAgent>();
        }

        baseEnemy.EnableAttack();
    }


}
