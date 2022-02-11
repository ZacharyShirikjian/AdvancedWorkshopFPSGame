using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachPlayerController : MonoBehaviour
{

    public float health;

    public float maxHealth;

    public Gun gun;

    private Vector3 moveDirection = Vector3.zero;

    private Camera playerCamera;
    private CharacterController playerController;

    public float speed = 5.0f;

    //REFERENCE TO THE UI SCRIPT//
    /*
     * Tina, I had to add in this reference in order to correctly update the UI
     * When the player takes damage or heal. -Zach
     */

    public UITest uiRef;

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
        if(uiRef.gameOver == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                gun.Shoot();
                uiRef.UpdateAmmoUI();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                gun.Reload();
                uiRef.Reload();
            }
        }
    }


    public void TakeDamage(float dmg)
    {

        if(health > 0 && dmg <= health)
        {
            health = health - dmg;
            uiRef.UpdateHealthUI(); //cals the method in the UI reference to update the health UI 
        }

        else if (health > 0 && dmg > health)
        {
            health = 0;
            uiRef.UpdateHealthUI();
            //end game or respawn
        }

        
    }


    public void Heal(float healPack)
    {
        if (health < maxHealth && (health + healPack <= maxHealth))
        {
            health = health + healPack;
            uiRef.UpdateHealthUI();
        }

        else if (health + healPack > maxHealth)
        {
            health = maxHealth;
            uiRef.UpdateHealthUI();
        }

    }

}
