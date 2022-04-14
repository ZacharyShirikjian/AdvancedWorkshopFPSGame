using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    public EnemyBasic baseEnemy;
    public GameObject enemy;
    public GameObject triggerBox;

    public bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
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
                StaticGameClass.PlusActiveEnemies(triggerBox);
                triggered = true;
            }
        }
    }

    public abstract void ActivateTriggerAction();


}
