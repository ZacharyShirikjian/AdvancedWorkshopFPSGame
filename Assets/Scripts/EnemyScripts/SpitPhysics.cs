using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitPhysics : MonoBehaviour
{

    public float speed = 8.0f;
    public int dmg = 30;
    public Rigidbody spit;
    public GameObject player;

    [SerializeField] Vector3 Point1;
    [SerializeField] Vector3 Point2;
    [SerializeField] Vector3 center;
    [SerializeField] Vector3 upRelative;
    [SerializeField] Vector3 downRelative;

    public float time = 20.0f;


    // Start is called before the first frame update
    void Start()
    {
        spit = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CalculateArc();
        StartCoroutine(Spit(time));
    }

    public void CalculateArc()
    {
        Point1 = transform.position;
        Point2 = player.transform.position;
        center = (Point1 + Point2) * 0.5f;
        center -= new Vector3(0, 1, 0);

        upRelative = Point1 - center;
        downRelative = Point2 - center;
    }

    IEnumerator Spit(float time)
    {
        float i = 0;
        float rate = 1 / time;

        while (i < 1)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Slerp(upRelative, downRelative, i);
            transform.position += center;

            yield return 0;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {

        Debug.Log("spit collided");
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(dmg);
        }

        //play animation here
        //play sound here
        Destroy(gameObject);

    }

}
