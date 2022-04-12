using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    public EnemyBasic baseEnemy;
    public GameObject enemy;

    public bool triggered;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
    
        triggered = false;
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.CompareTag("Player"))
            {
                ActivateTriggerAction();
                triggered = true;
            }
        }
    }

    public abstract void ActivateTriggerAction();


}
