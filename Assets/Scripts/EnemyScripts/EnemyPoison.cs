using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoison : MonoBehaviour
{

    public float radius;
    public int dmg;
    public bool poison;

    public GameObject player;
    public PlayerController playCon;

    //REFERENCE TO UI SCRIPT TO GET SPLATTER TO WORK
    [SerializeField] private UITest uiScript;

    // Start is called before the first frame update
    void Start()
    {

        radius = 15.0f;
        dmg = 2;

        poison = false;

        player = GameObject.FindWithTag("Player");
        playCon = player.GetComponent<PlayerController>();

        InvokeRepeating("Poison", 0, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(StaticGameClass.pause == false)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= radius)
            {
                poison = true;
            }

            else if (Vector3.Distance(player.transform.position, transform.position) > radius)
            {
                poison = false;
            }

        }

    }


    public void Poison()
    {
        if (poison)
        {
            playCon.TakeDamage(dmg);
            uiScript.inMist = true;
            uiScript.StartSplatterCoroutine();
        }

    }


}
