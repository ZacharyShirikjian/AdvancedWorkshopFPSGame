using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WindowTrigger : MonoBehaviour
{


    private EnemyAttack attack;
    public GameObject enemy;

    public GameObject PointA;
    public GameObject PointB;

    [SerializeField] Vector3 Point1;
    [SerializeField] Vector3 Point2;
    [SerializeField] Vector3 center;
    [SerializeField] Vector3 upRelative;
    [SerializeField] Vector3 downRelative;

    public float journeyTime = 3.0f;
    private float startTime;

    public bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        attack = enemy.GetComponent<EnemyAttack>();
        triggered = false;
        CalculateArc();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (!triggered)
        {
            if (other.CompareTag("Player"))
            {
                startTime = Time.time;

                StartCoroutine(Window(journeyTime));

                triggered = true;
            }
        }


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


    //IEnumerator Climb(float time)
    //{
    //    float i = 0;
    //    float rate = 1 / time;

    //    while (i < 1)
    //    {
    //        i += Time.deltaTime * rate;
    //        enemy.transform.position = Vector3.Slerp(Point1, Point2, i);
    //        yield return 0;
    //    }
    //    //StartCoroutine(ActivateEnemy());
    //}




    IEnumerator ActivateEnemy()
    {
        yield return new WaitForSeconds(3.0f);
       
        NavMeshAgent enemyNMA_ = enemy.AddComponent<NavMeshAgent>();

        attack.spitting = true;
        //attack.tracking = true;
        StartCoroutine(attack.SpitAttack());

    }


}
