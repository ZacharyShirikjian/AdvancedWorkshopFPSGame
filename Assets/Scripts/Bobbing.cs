using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{

    public float hoverForce;
    public float hoverHeight;
    public Rigidbody rb;
    private string tag;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        tag = gameObject.tag;
        switch (tag)
        {
            case "Coin":
                hoverHeight = 2;
                hoverForce = 5;
                break;
            case "Enemy":
                hoverHeight = 4;
                hoverForce = 50;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (isGrounded)
        {
            if(rb.position.y < hoverHeight)
            {
                rb.AddForce(Vector3.up * hoverForce);
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}
