using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT IS BASED ON THE CODE FROM "How to Use Lerp (Unity Tutorial) by Ketra Games on YouTube
//https://www.youtube.com/watch?v=MyVY-y_jK1I

//Modified into coroutine from TreyH's code on the "Help using Lerp inside of a coroutine" forum on Unity Answers
//https://answers.unity.com/questions/1502190/help-using-lerp-inside-of-a-coroutine.html 
public class CameraLerpMovement : MonoBehaviour
{
    //FOR CAMERA ZOOM IN//

    //Reference player
    [SerializeField] private GameObject player;
    private Vector3 cameraEndPos; 
    private Vector3 startPos;
    //Hold desired duration of moving from startPos to cameraEndPos
    private float desiredDuration = 1f; //amount of time you want to move between 2 points for 
    private float elapsedTime; //amount of time that's passed since start of the game 
    public bool zoomingIn; //if true, call method to zoom in, else, don't call the method in update
    // Start is called before the first frame update
    void Start()
    {
        //This makes sure the start position stays the same while lerp is being used
        startPos = transform.position;
        cameraEndPos = new Vector3(player.transform.position.x, 3, player.transform.position.z + 3);
        zoomingIn = false;

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public IEnumerator ZoomCamera()
    {
        elapsedTime = 0;
        //Get the % of the time that's pased, before it's reached 1 second
        float percentageComplete = elapsedTime / desiredDuration; //percentage of how much time has passed
        while (elapsedTime < desiredDuration)
        {
            transform.position = Vector3.Lerp(startPos, cameraEndPos, percentageComplete); //Linearly moves from start to the end linearly over 1 second
            elapsedTime += Time.deltaTime;
        }
        transform.position = cameraEndPos;
        yield return null;
    }
}
