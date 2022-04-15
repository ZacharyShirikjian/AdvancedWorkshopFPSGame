using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{


    public int gunDamage = 1;           //takes one hit point off enemy
  
    public float weaponRange = 50f;     //how far the gun shoots
    
    public Transform gunEnd;            //transform to tag the end of the gun

    public Camera playerCam;              //main camera
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f); 
    private LineRenderer laserLine;

    private Vector3 rayOrigin;


    void Start()
    {

        laserLine = GetComponent<LineRenderer>();

    }



    // Update is called once per frame
    void Update()
    {
        
    }


    public void Shoot()
    {
        Debug.Log("shoot");
        StartCoroutine(ShotEffect());
        rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // Declare a raycast hit to store information about what our raycast has hit
        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);

        // Check if our raycast has hit anything
        if (Physics.Raycast(rayOrigin, playerCam.transform.forward, out hit, weaponRange))
        {
            // Set the end position for our laser line 
            laserLine.SetPosition(1, hit.point);

            // Get a reference to a health script attached to the collider we hit
            EnemyBasic health = hit.collider.GetComponent<EnemyBasic>();

            
            if (health != null)
            {
                Debug.Log("damaged");
                health.HitEnemy(gunDamage);
            }

        }
        else
        {
            laserLine.SetPosition(1, rayOrigin + (playerCam.transform.forward * weaponRange));
        }


    }





    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

}
