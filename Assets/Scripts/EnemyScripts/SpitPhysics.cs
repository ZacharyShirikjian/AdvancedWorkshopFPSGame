using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitPhysics : MonoBehaviour
{

    public float speed = 8.0f;
    public int dmg = 10;
    public Rigidbody spit;
    public GameObject player;
    [SerializeField] Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        spit = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //spit.AddForce(SetPlayerTarget() * speed, ForceMode.Impulse);
        spit.velocity = transform.forward * speed;
    }


    public Vector3 SetPlayerTarget()
    {
        return direction = (player.transform.position - transform.position);
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
