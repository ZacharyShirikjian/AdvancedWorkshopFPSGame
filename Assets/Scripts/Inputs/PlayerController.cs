using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
#pragma warning disable 649

    public int health;                //holds player current health
    public int maxHealth = 100;             //holds player max health

    public Camera playerCam;                                //holds main camera(playerCamera)
    public float defaultFOV = 90;
    public float defaultSenseX = 5f;
    public float defaultSenseY = 0.025f;
    public float zoomSenseX;
    public float zoomSenseY;
    public float zoomPercent = 1.5f;

    [SerializeField] CharacterController controller;        //pulls character controller component from player
    [SerializeField] float speed = 5f;                     //speed variable for character movement
    Vector2 inputVector;

    //public bool isGrounded; //holds variable for if player is collided on ground object
    public bool zoom;       //holds bool for zoom state
    public bool shoot;      //holds bool for shoot mechanic, if left click is pressed
    public bool crouch;     //holds bool for crouch
    public bool reload;     //holds bool for reload state
    public bool playerDeath;    //holds bool for player death state
    public bool canMove = true;


    public Vector3 standPosition;       //holds stand position
    public Vector3 crouchPosition;      //holds crouch position
    public Vector3 gunStanding;         //holds gun position while standing
    public Vector3 gunCrouching;        //holds gun position while crouching
    public float gunCrouchHeight;
    public float gunStandHeight;
    public float crouchHeight;   //crouchheight for character
    public float standHeight;    //standheight for character
    public float smooth = 5.0f;         //value for smooth object transform during crouch
    public float crouchPercent = 1.0f;         //holds value to transform positions on crouch

    //[SerializeField] float gravity = -30f;
    //Vector3 verticalVelocity = Vector3.zero;
    //[SerializeField] LayerMask groundMask;

    public RaycastShoot rayShoot;       //script that runs raycast hitscan
    private float nextFire;             //float to delay gun fire
    public float fireRate = 0.25f;      //how often the gun can shoot

    public float ammo;              //
    public float maxAmmo;           //
    public GameObject arms;              //arms model

    public Animator animator;

    //REFERENCE TO UI SCRIPT//
    private UITest uiRef;

    //REFERENCE to MouseLook script
    private MouseLook mouseLook;

    //REFERENCE TO PLAYER AUDIO SOURCE
    [SerializeField] private AudioClip shootGun;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip takeDamage;
    [SerializeField] private AudioClip playerDead;
    private AudioSource audioSource;

    public GameObject deleteLater;

    public void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        ammo = 6;
        maxAmmo = 6;

        rayShoot = GetComponentInChildren<RaycastShoot>();
        animator = GetComponentInChildren<Animator>();

        health = maxHealth;

        playerCam.fieldOfView = defaultFOV;
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
        mouseLook = GetComponent<MouseLook>();

        if (uiRef.currentControlScheme == UITest.CurrentController.KEYBOARD)
        {
            mouseLook.sensitivityX = defaultSenseX;
            mouseLook.sensitivityY = defaultSenseY;
        }


        else if (uiRef.currentControlScheme == UITest.CurrentController.GAMEPAD)
        {
            mouseLook.sensitivityX = 50;
            mouseLook.sensitivityY = 3;
        }

        //controller.height = standHeight;
        //crouchHeight = standHeight - crouchPercent;

        gunStandHeight = arms.transform.position.y;
        gunCrouchHeight = gunStandHeight - crouchPercent;

        zoomSenseX = defaultSenseX / 0.5f;
        zoomSenseY = defaultSenseY / 0.5f;
        audioSource = GetComponent<AudioSource>();
        Debug.Log(playerCam.transform.position);
    }


    public void Update()
    {

        if(StaticGameClass.pause == false)
        {
            if (zoom)
            {
                //Debug.Log("Zoom");
                animator.SetBool("Zoom", true);   //triggers zoomed in animations
                playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, (defaultFOV / zoomPercent), Time.deltaTime * smooth);
                mouseLook.sensitivityX = zoomSenseX;
                mouseLook.sensitivityY = zoomSenseY;
            }
            if (!zoom)
            {
                //Debug.Log("UnZoom");
                animator.SetBool("Zoom", false);    //turns off zoomed in animations
                playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, defaultFOV, Time.deltaTime * smooth);
                mouseLook.sensitivityX = defaultSenseX;
                mouseLook.sensitivityY = defaultSenseY;
            }

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
                audioSource.PlayOneShot(shootGun);
                //include animation

                shoot = false;

                Debug.Log("pew pew");

                //CALL UI METHOD TO UPDATE UI WHEN SHOOTING//
                uiRef.UpdateAmmoUI(false);

            }
            /*
            if (crouch)
            {
                //Debug.Log("Crouch activated");

                //controller.height = crouchHeight;

                crouchPosition = new Vector3(transform.localPosition.x, crouchHeight, transform.localPosition.z);

                playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, crouchPosition, Time.deltaTime * smooth);

                gunCrouching = new Vector3(arms.transform.position.x, gunCrouchHeight, arms.transform.position.z);

                arms.transform.position = Vector3.Lerp(arms.transform.position, gunCrouching, Time.deltaTime * smooth);

                //playerCam.transform.position = deleteLater.transform.position;
                //transform.position = gunStanding;

            }
            else if (!crouch)
            {
                //controller.height = standHeight;

                standPosition = new Vector3(transform.localPosition.x, standHeight, transform.localPosition.z);

                //TEMP METHOD FOR TESTING PLAYER IN FINALLEVEL SCENE, WILL DELETE LATER//

                //playerCam.transform.position = deleteLater.transform.position;
                //transform.position = gunStanding;
                playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, standPosition, Time.deltaTime * smooth);

                gunStanding = new Vector3(arms.transform.position.x, gunStandHeight, arms.transform.position.z);

                arms.transform.position = Vector3.Lerp(arms.transform.position, gunStanding, Time.deltaTime * smooth);

            }
            */
            //reload ammo to maxAmmo for gun
            if (reload)
            {

                animator.SetTrigger("Reload");
                //add audio
                audioSource.PlayOneShot(reloadClip);
                shoot = false;

                ammo = maxAmmo - 2;
                maxAmmo = ammo;

                if (ammo <= 0)
                {
                    ammo = 0;
                    maxAmmo = 0;
                    audioSource.Play();
                }

                //CALL UI SCRIPT METHOD TO UPDATE AMMO UI W/ CORRECT AMMO 
                reload = false;
                if (maxAmmo > 0)
                {
                    uiRef.Reload();
                }

            }

            if(canMove)
            {
                Vector3 horzVel = (transform.right * inputVector.x + transform.forward * inputVector.y) * speed;
                controller.Move(horzVel * Time.deltaTime);
            }
            //wasd movement


            //verticalVelocity.y += gravity * Time.deltaTime;
            //controller.Move(verticalVelocity * Time.deltaTime);
        }

    }

    //accepts wasd movement
    public void ReceiveInput(Vector2 _groundMovement)
    {
        inputVector = _groundMovement;
    }

    //shoot triggered from input system
    public void OnShootPressed()
    {
        shoot = true;
    }

    //crouch triggered from input system
    public void OnCrouchPressed()
    {
        Debug.Log("Crouch");
        crouch = true;

    }

    //crouch UNtriggered from input system
    public void OnCrouchUnpressed()
    {
        crouch = false;
    }

    //zoom triggered from input system
    public void OnZoomPressed()
    {
        zoom = true;
    }

    //zoom UNtriggered from input system
    public void OnZoomUnpressed()
    {
        zoom = false;
    }

    //reload triggered from input system
    public void OnReloadPressed()
    {
        reload = true;
    }

    //lowers player health when called, if health runs out, triggers PlayerDeath method
    //takes int for damage amount
    public void TakeDamage(int dmg)
    {

        if (health > 0 && dmg <= health)
        {
            health = health - dmg;
            audioSource.PlayOneShot(takeDamage);
            uiRef.UpdateHealthUI();
        }

        else if (health > 0 && dmg > health)
        {
            health = 0;
            playerDeath = true;
            audioSource.PlayOneShot(playerDead);
            PlayerDeath();
        }

    }

    //adds health to player when called
    //takes int for health regen amount
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


    //triggers static state and end game screen

    public void PlayerDeath()
    {
        if (playerDeath)
        {
            uiRef.GameOver();
        }
    }


}