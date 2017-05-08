using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronMovement : MonoBehaviour {

    private GameObject targetWaypoint;
    private GameObject hotelManager;
    private GameObject playerHead;

    private GameObject rotateTarget;

    private GameObject leverRotator;

    private GameObject musicSource;

    //For new waypoint system
    private int waypointNumber = 2;

    [Range(0.5f, 5)]
    public float walkSpeed = 0.5f;
    [Range(1, 5)]
    public float rotationSpeed = 5f;

    public bool moving = false;
    public bool rotating = false;
    public bool waiting = false;
    public string state = "";

	Animator anim;

    //TIMER
    private float timer = 0;


	// Use this for initialization
	void Start () {
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
        playerHead = GameObject.FindGameObjectWithTag("MainCamera");
        leverRotator = GameObject.FindGameObjectWithTag("lever");
        musicSource = GameObject.FindGameObjectWithTag("musicControl");

		anim = GetComponent<Animator> ();
    }
	
	// Update is called once per frame
	void Update () {

        //TIMER
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            anim.SetBool("talking", false);
        }

        if (moving)
        {
            //If the floor has changed since getting off the elevator
            if(targetWaypoint.transform.parent.gameObject.activeSelf == false) {
                despawnPatron();
                return;
            }

            float step = walkSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.transform.position, step);

            if (transform.position == targetWaypoint.transform.position)
            {
                //We've reached our destination
                if(state == "entering"){
                    moving = false;

					// if the NPC hasnt already reached the waypoint in the elevator
					// aka walked inside
					if (!anim.GetBool ("reachedWaypoint")) {
						anim.SetBool("reachedWaypoint", true);
					} else {	// this means they are now going to the waypoint outside of the elevator
						anim.SetBool("reachedWaypoint2", true);
					}
                    turnTowardsPlayer();
                }

                else if (state == "leaving")
                {
                    //Move to a second + n waypoint and then despawn
                    var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
                    //Check if there's another waypoint in the path
                    if (currentFloor != -1) {
                        if (hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint2(currentFloor, waypointNumber) != null) {
                            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint2(currentFloor, waypointNumber);
                            turnTowardsWaypoint(targetWaypoint);
                            waypointNumber++;
                        }
                        else {
                            despawnPatron();
                        }
                    }
                }
            }
        }

        if (rotating)
        {
            var targetRotation = Quaternion.LookRotation(rotateTarget.transform.position - transform.position);
            targetRotation.x = transform.rotation.x;
            targetRotation.z = transform.rotation.z;
            var str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);

            if(transform.rotation == targetRotation)
            {
                //We're looking at player, stop looking
                
                //rotating = false;
            }
        }
    }

    public bool enterElevator()
    {
        if (hotelManager.GetComponent<FloorManager>().doorOpen == true)
        {
            //Debug.Log("yes yes yes ");

            //Turn off light here
            leverRotator.GetComponent<patronWaiting>().lightOff();
            // turn on floor goal light here
            leverRotator.GetComponent<patronWaiting>().lightGoal(this.gameObject.GetComponent<Agent>().getGoal());    // goal of json 


            //Turn on theme music
            var musicID = 3;
            if(gameObject.tag == "Boss") { musicID = 1; }
            if(gameObject.tag == "Business") { musicID = 2; }
            if(gameObject.tag == "Tourist") { musicID = 3; }
            musicSource.GetComponent<musicController>().playCharacterTheme(musicID);

			anim.SetBool ("elevatorHere", true);
            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchElevatorWaypoint();
            moving = true;
            state = "entering";

            transform.parent = null;

            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool leaveElevator()
    {
        if (hotelManager.GetComponent<FloorManager>().doorOpen == true)
        {
            anim.SetBool("walkOut", true);

            // turn off light goal light
             leverRotator.GetComponent<patronWaiting>().lightOff();

            var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint(currentFloor);
            moving = true;

            state = "leaving";


            turnTowardsWaypoint(targetWaypoint);

            //transform.parent = hotelManager.GetComponent<FloorManager>().floors[currentFloor].transform;
            // GetComponent<Animator>().SetBool("reachedWaypoint", false);

            //Resume the elevator music
            musicSource.GetComponent<musicController>().characterExit();

            return true;
        }
        else
        {
            return false;
        }
    }

    public void despawnPatron() {
        hotelManager.GetComponent<DayManager>().nextPatron();
        Debug.Log("Called despawn");
        Destroy(this.gameObject);
    }

    public void wait()
    {
        waiting = true;
    }

    public void turnTowardsPlayer()
    {
        rotateTarget = playerHead;
        rotating = true;
    }
    public void turnTowardsWaypoint(GameObject waypoint)
    {
        rotateTarget = waypoint;
        rotating = true;
    }

    public void talk()
    {
        timer = 10;
        anim.SetBool("talking", true);
    }
    
    public void stopTalking() {
        anim.SetBool("talking", false);
    }

    //Called from AI to tell the player that now is the time for a gesture
    public void waitingForGesture() {
        // turn on hand haptic 

        // play a tiny particle system?

    }

    public void stopWaitingGesture() {

    }

    //Called from AI whenever the mood is changed i indicates by how much
    public void moodChanged(int i) {

    }
}
