using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float health;

    public float maxHealth;

    public Gun gun;

    private Vector3 moveDirection = Vector3.zero;

    private Camera playerCamera;
    private CharacterController playerController;

    public float speed = 5.0f;

    public void Start()
    {
        //Animator animator = GetComponent<Animator>();
        playerCamera = Camera.main;
        playerController = GetComponent<CharacterController>();
    }


    public void Update()
    {

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = playerCamera.transform.rotation * moveDirection;
        playerController.Move(moveDirection * speed * Time.deltaTime);

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
