using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference for getting muffle to work
//https://answers.unity.com/questions/263010/adjust-audio-based-on-proximity.html

//https://sound.stackexchange.com/questions/24641/how-to-create-the-muffled-sound-effect-like-hearing-through-walls

public class TrainMuffle : MonoBehaviour
{
    public Transform target; //reference to either Wall of Train Car
    //Reference to player
    private PlayerController player;

    //Reference to audio filter
    private AudioLowPassFilter muffleFilter;

    //Distance away from edge of train car 
    private Vector3 distanceAway;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        muffleFilter = GetComponent<AudioLowPassFilter>();
        muffleFilter.cutoffFrequency = 800;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceAway = Vector3.Distance(target.position, player.transform.position) * 2;
        muffleFilter.cutoffFrequency = (float) (distanceAway / 1) * 100;
        Debug.Log(muffleFilter.cutoffFrequency);

        //Full muffle if player is in middle of the train
        if(player.transform.position.x > -2 && player.transform.position.x < 2)
        {
            muffleFilter.cutoffFrequency = 800;
        }

        //Remove muffle completely if player touches walls of train
        else if(player.transform.position.x > 5 || player.transform.position.x < -5)
        {
            muffleFilter.cutoffFrequency = 10000;
        }
    }
}
