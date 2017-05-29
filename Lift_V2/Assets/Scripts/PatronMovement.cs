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

    [Header("Footsteps")]
    public string footstepSound;
    public float footstepGap;
    private float footstepTimer;

	Animator anim;

    //TIMER
    private float timer = 0;

    //Adultress Only
    private GameObject dateBoi;


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
            if(gameObject.tag == "Adultress") {
                dateBoi.GetComponent<dateMovement>().talk(5);
            }
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

            //Make footstep noise
            footstepTimer += Time.deltaTime;
            if(footstepTimer >= footstepGap) {
                footstepSound.PlaySound(transform.position);
                footstepTimer = 0;
            }

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
            //If Adultress on day 1 or 2, bring their date boi too
            if(gameObject.tag == "Adultress") {
                var date1 = transform.Find("Date1");
                var date2 = transform.Find("Date2");
                if(date1.gameObject.activeSelf == true) {
                    date1.GetComponent<dateMovement>().enterElevator();
                    dateBoi = date1.gameObject;
                    date1.transform.parent = null;
                }
                else if(date2.gameObject.activeSelf == true) {
                    date2.GetComponent<dateMovement>().enterElevator();
                    dateBoi = date2.gameObject;
                    date2.transform.parent = null;
                }
            }

            //Turn off light here
            leverRotator.GetComponent<patronWaiting>().lightOff();
            // turn on floor goal light here
            leverRotator.GetComponent<patronWaiting>().lightGoal(this.gameObject.GetComponent<Agent>().getGoal());    // goal of json 


            //Turn on theme music
            var musicID = 3;
            if(gameObject.tag == "Boss") { musicID = 1; }
            if(gameObject.tag == "Business") { musicID = 2; }
            if(gameObject.tag == "Tourist") { musicID = 3; }
            if (gameObject.tag == "Server") { musicID = 4; }
            if (gameObject.tag == "Adultress") { musicID = 5; }
            if (gameObject.tag == "Artist") { musicID = 6; }
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
            //If Adultress on day 1 or 2, bring their date boi too
            if (gameObject.tag == "Adultress") {
                dateBoi.GetComponent<dateMovement>().leaveElevator();
            }

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
        if (gameObject.tag == "Adultress"){
            Destroy(dateBoi.gameObject);
        }
        Destroy(this.gameObject);
    }

    public void wait()
    {
        waiting = true;
    }

    public void turnTowardsPlayer()
    {
        if (gameObject.tag == "Adultress") {
            rotateTarget = dateBoi;
        }
        else {
            rotateTarget = playerHead;
            rotating = true;
        }
    }

    public void turnTowardsWaypoint(GameObject waypoint)
    {
        rotateTarget = waypoint;
        rotating = true;
    }

    public void talk(float time) {
        timer = time;
        anim.SetBool("talking", true);
    }
    
    public void stopTalking() {
        anim.SetBool("talking", false);
    }

    //Called from AI whenever the mood is changed i indicates by how much
    public void moodChanged(int i) {
        Debug.Log("Mood Changed by: " + i);
        if(i < 0) {
            //Play Angry Animation
            transform.Find("Moods").GetComponent<MoodParticles>().angryAnimation();
            
        }
        else if(i > 0) {
            //Play Happy Animation
            transform.Find("Moods").GetComponent<MoodParticles>().happyAnimation(i);
 
        }
        /*
        else {
            //Play Confused Animation
            transform.Find("Moods").GetComponent<MoodParticles>().neutralAnimation();

        }
        */
    }
    /*
    IEnumerator StopParticle(float time, string ParticleName) {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        transform.Find(ParticleName).gameObject.SetActive(false);
    }
    */
}
