using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8.0f;
    public float dmg;
    public Rigidbody rbBullet;

    public void Start()
    {
        rbBullet = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        rbBullet.velocity = transform.forward * speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyParent enemy = collision.gameObject.GetComponent<EnemyParent>();
            enemy.HitEnemy(dmg);
        }

        Destroy(gameObject);
    }


}
