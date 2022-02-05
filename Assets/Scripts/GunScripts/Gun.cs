using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int ammo = 6;
    public int remainingAmmo;

    public GameObject bullet; //prefab

    public enum ShootState
    {
        Ready,
        Shooting,
        Reloading
    }

    private ShootState shootState = ShootState.Ready;

    private Vector3 spawnPoint;
    private float shotInterval = 1.0f;
    private float reloadTime = 5.0f;

    private float nextShootTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        remainingAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextShootTime)
        {
            if (shootState == ShootState.Shooting || shootState == ShootState.Reloading)
            {
                shootState = ShootState.Ready;
            }
        }

    }

    public void Shoot()
    {
        if(remainingAmmo > 0)
        {
            Debug.Log("Bullet shot");
            Instantiate(bullet, new Vector3(0,1,0), transform.rotation);        //FIXME: all stand in spawn variables
            nextShootTime = Time.time + shotInterval;
            shootState = ShootState.Shooting;
            remainingAmmo--;
        }
        

        //else
        //FIXME
        //what happens if ammo is empty?
    }

    public void Reload()
    {
        if(shootState == ShootState.Ready)
        {

            remainingAmmo = ammo;
            nextShootTime = Time.time + reloadTime;
            shootState = ShootState.Reloading;

        }
    }
}
