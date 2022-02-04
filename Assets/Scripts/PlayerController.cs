using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float health;

    public float maxHealth;

    public Gun gun;


    public void Start()
    {
        //Animator animator = GetComponent<Animator>();
    }


    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            gun.Reload();
        }
    }


    public void TakeDamage(float dmg)
    {

        if(health > 0 && dmg <= health)
        {
            health = health - dmg;
        }

        else if (health > 0 && dmg > health)
        {
            health = 0;
            //end game or respawn
        }

        
    }


    public void Heal(float healPack)
    {
        if (health < maxHealth && (health + healPack <= maxHealth))
        {
            health = health + healPack;
        }

        else if (health + healPack > maxHealth)
        {
            health = maxHealth;
        }

    }

}
