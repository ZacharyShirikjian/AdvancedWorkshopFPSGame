using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
#pragma warning disable 649

    public float health;
    public float maxHealth;

    public Camera playerCam;
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 inputVector;

    //public bool isGrounded; //holds variable for if player is collided on ground object
    public bool shoot;      //holds bool for shoot mechanic, if left click is pressed
    public bool crouch;     //holds bool for crouch
    public bool reload;     //holds bool for reload state

    public Vector3 standPosition;
    public Vector3 crouchPosition;
    public float crouchHeight = 1.5f;
    public float standHeight = 3.0f;
    public float smooth = 5.0f;

    //[SerializeField] float gravity = -30f;
    //Vector3 verticalVelocity = Vector3.zero;
    //[SerializeField] LayerMask groundMask;

    public RaycastShoot rayShoot;
    private float nextFire;
    public float fireRate = 0.25f;      //how often the gun can shoot
    public float ammo = 6;
    public GameObject gun;

    //REFERENCE TO UI SCRIPT//
    private UITest uiRef;

    public void Awake()
    {
        rayShoot = GetComponentInChildren<RaycastShoot>();
        controller.height = standHeight;
        uiRef = GameObject.Find("Canvas").GetComponent<UITest>();
    }


    public void Update()
    {

        //check if player is on a "ground" tagged object
        //isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);

        if (shoot && Time.time > nextFire && ammo > 0)
        {

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
            Debug.Log("Crouch activated");

            controller.height = crouchHeight;

            crouchPosition = new Vector3(transform.localPosition.x, crouchHeight, transform.localPosition.z);

            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, crouchPosition, Time.deltaTime * smooth);

            gun.transform.position = new Vector3(gun.transform.position.x, 0.5f, gun.transform.position.z);

        }
        else if (!crouch)
        {
            controller.height = standHeight;

            standPosition = new Vector3(transform.localPosition.x, standHeight, transform.localPosition.z);
            
            playerCam.transform.position = Vector3.Lerp(playerCam.transform.position, standPosition, Time.deltaTime * smooth);

            gun.transform.position = new Vector3(gun.transform.position.x, 1.5f, gun.transform.position.z);
        }
  


        //FIXME:: lineRenderer activates when reloading
        
        if (reload)
        {
            //play animation here
            //add audio
            shoot = false;

            if(ammo > 2)
            {
                ammo = ammo - 2;
            }

            reload = false;

            //CALL UI SCRIPT METHOD TO UPDATE AMMO UI W/ CORRECT AMMO 
            uiRef.Reload();

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
        if (!crouch)
        {
            crouch = true;
        }
        else
        {
            crouch = false;
        }
    }


    public void OnReloadPressed()
    {
        reload = true;
    }

    public void TakeDamage(float dmg)
    {

        if (health > 0 && dmg <= health)
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
