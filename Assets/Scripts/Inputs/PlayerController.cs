using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
#pragma warning disable 649

    public float health;

    public float maxHealth;

    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 11f;
    Vector2 inputVector;

    //[SerializeField] float jumpHeight = 3.5f;

    //public bool jump;
    public bool shoot;
    public bool crouch;

    [SerializeField] float gravity = -30f;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;

    public bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);


        //if (isGrounded)
        //{
        //    verticalVelocity.y = 0;
        //}

        //if (jump)
        //{
        //    Debug.Log("jumped");
        //    if (isGrounded)
        //    {
        //        verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
        //    }
        //    jump = false;
        //}

        if (shoot)
        {
            Debug.Log("pew pew");
        }

        if (crouch)
        {
            Debug.Log("Crouch activated");
        }



        Vector3 horzVel = (transform.right * inputVector.x + transform.forward * inputVector.y) * speed;
        controller.Move(horzVel * Time.deltaTime);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
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
        crouch = true;
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
