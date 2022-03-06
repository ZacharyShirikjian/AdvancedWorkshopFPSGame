using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitPhysics : MonoBehaviour
{

    public float speed = 8.0f;
    public int dmg = 5;
    public Rigidbody spit;


    // Start is called before the first frame update
    void Start()
    {
        spit = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        spit.velocity = transform.forward * speed;
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
