using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronMovement : MonoBehaviour {

    private GameObject targetWaypoint;
    private GameObject hotelManager;
    private GameObject playerHead;

    private GameObject rotateTarget;

    [Range(0.5f, 5)]
    public float walkSpeed = 0.5f;
    [Range(1, 5)]
    public float rotationSpeed = 5f;

    public bool moving = false;
    public bool rotating = false;
    public bool waiting = false;
    public string state = "";

	Animator anim;

	// Use this for initialization
	void Start () {
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
        playerHead = GameObject.FindGameObjectWithTag("MainCamera");

		anim = GetComponent<Animator> ();
    }
	
	// Update is called once per frame
	void Update () {

        if (moving)
        {
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
                    //Move to a second waypoint and then despawn
                    var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
                    targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint2(currentFloor);
                    turnTowardsWaypoint(targetWaypoint);
                    state = "leaving2";
                }
                else if(state == "leaving2")
                {
                    //Basic Despawmn

                    //Tell the manager that we've finished a character
                    hotelManager.GetComponent<DayManager>().nextPatron();
                    Debug.Log("Called despawn");
                    Destroy(this.gameObject);
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
            Debug.Log("yes yes yes ");
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

            var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint(currentFloor);
            moving = true;

            state = "leaving";


            turnTowardsWaypoint(targetWaypoint);

            transform.parent = hotelManager.GetComponent<FloorManager>().floors[currentFloor].transform;
           // GetComponent<Animator>().SetBool("reachedWaypoint", false);

            return true;
        }
        else
        {
            return false;
        }
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
}
