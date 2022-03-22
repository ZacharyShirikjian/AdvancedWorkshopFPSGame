using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
#pragma warning disable 649

    public int health;                //holds player current health
    public int maxHealth = 100;             //holds player max health

    public Camera playerCam;                                //holds main camera(playerCamera)
    [SerializeField] CharacterController controller;        //pulls character controller component from player
    [SerializeField] float speed = 11f;                     //speed variable for character movement
    Vector2 inputVector;

    //public bool isGrounded; //holds variable for if player is collided on ground object
    public bool zoom;
    public bool shoot;      //holds bool for shoot mechanic, if left click is pressed
    public bool crouch;     //holds bool for crouch
    public bool reload;     //holds bool for reload state
    public bool playerDeath;


    public Vector3 standPosition;       //holds stand position
    public Vector3 crouchPosition;      //holds crouch position
    public Vector3 gunStanding;
    public Vector3 gunCrouching;
    public float crouchHeight = 1.5f;   //crouchheight for character
    public float standHeight = 3.0f;    //standheight for character
    public float smooth = 5.0f;         //value for smooth object transform during crouch

    //[SerializeField] float gravity = -30f;
    //Vector3 verticalVelocity = Vector3.zero;
    //[SerializeField] LayerMask groundMask;

    public RaycastShoot rayShoot;       //script that runs raycast hitscan
    private float nextFire;             //float to delay gun fire
    public float fireRate = 0.25f;      //how often the gun can shoot

    public float ammo;              //amount of ammo when full
    public float maxAmmo;
    public GameObject gun;              //gun game object

    public Animator animator;

    //REFERENCE TO UI SCRIPT//
    private UITest uiRef;

    public void Awake()
    {
        ammo = 6;
        maxAmmo = 6;

        rayShoot = GetComponentInChildren<RaycastShoot>();
        animator = GetComponent<Animator>();
        controller.height = standHeight;

        health = maxHealth;

        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();

    }


    public void Update()
    {

        //check if player is on a "ground" tagged object
        //isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);

        if (shoot && Time.time > nextFire && ammo > 0)
        {
            animator.SetTrigger("Shoot");

            ammo--;

            // Update the time when our player can fire next
            nextFire = Time.time + fireRate;
            rayShoot.Shoot();


            //play audio here
            //include animation

            shoot = false;
            

            Debug.Log("pew pew");

            //CALL UI METHOD TO UPDATE UI WHEN SHOOTING//
            uiRef.UpdateAmmoUI();

        }

        if (crouch)
        {
            //Debug.Log("Crouch activated");

            controller.height = crouchHeight;

            crouchPosition = new Vector3(transform.localPosition.x, crouchHeight, transform.localPosition.z);

            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, crouchPosition, Time.deltaTime * smooth);

            gunCrouching = new Vector3(gun.transform.position.x, 0.5f, gun.transform.position.z);

            gun.transform.position = Vector3.Lerp(gun.transform.position, gunCrouching, Time.deltaTime * smooth);

        }
        else if (!crouch)
        {
            controller.height = standHeight;

            standPosition = new Vector3(transform.localPosition.x, standHeight, transform.localPosition.z);
            
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, standPosition, Time.deltaTime * smooth);

            gunStanding = new Vector3(gun.transform.position.x, 2.0f, gun.transform.position.z);

            gun.transform.position = Vector3.Lerp(gun.transform.position, gunStanding, Time.deltaTime * smooth);

        }
  


        //FIXME:: lineRenderer activates when reloading
        
        if (reload)
        {

            animator.SetTrigger("Reload");

            //play animation here
            //add audio
            shoot = false;

            ammo = maxAmmo - 2;
            maxAmmo = ammo;
            if(ammo <= 0)
            {
                ammo = 0;
                maxAmmo = 0;
            }


            //CALL UI SCRIPT METHOD TO UPDATE AMMO UI W/ CORRECT AMMO 
            reload = false;
            if(maxAmmo > 0)
            {
                uiRef.Reload();
            }

        }

        Vector3 horzVel = (transform.right * inputVector.x + transform.forward * inputVector.y) * speed;
        controller.Move(horzVel * Time.deltaTime);

        //verticalVelocity.y += gravity * Time.deltaTime;
        //controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _groundMovement)
    {
        inputVector = _groundMovement;   
    }


    public void OnShootPressed()
    {
        shoot = true;
    }

    public void OnCrouchPressed()
    {
        Debug.Log("Crouch");
        crouch = true;

    }

    public void OnCrouchUnpressed()
    {
        crouch = false;
    }

    public void OnZoomPressed()
    {
        zoom = true;
    }

    public void OnZoomUnpressed()
    {
        zoom = false;
    }



    public void OnReloadPressed()
    {
        reload = true;
    }

    public void TakeDamage(int dmg)
    {

        if (health > 0 && dmg <= health)
        {
            health = health - dmg;
        }

        else if (health > 0 && dmg > health)
        {
            health = 0;
            playerDeath = true;
            PlayerDeath();
        }

    }


    public void Heal(int healPack)
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


    public void PlayerDeath()
    {
        if(playerDeath)
        {

        }
    }


}
