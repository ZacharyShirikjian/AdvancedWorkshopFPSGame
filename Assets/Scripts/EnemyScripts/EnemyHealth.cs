using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyAttack attack;
    public float health = 2;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitEnemy(float dmg)
    {
        if (health > 0)
        {
            health -= dmg;
        }
        if (health <= 0)
        {
            Instantiate(coin, transform.position, transform.rotation);
            StopCoroutine(attack.SpitAttack());
            Destroy(gameObject);
        }
    }



}
