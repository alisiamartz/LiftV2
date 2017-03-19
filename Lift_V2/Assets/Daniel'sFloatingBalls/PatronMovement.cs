using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronMovement : MonoBehaviour {

    private GameObject targetWaypoint;
    private GameObject hotelManager;
    private GameObject playerHead;

    [Range(0.5f, 5)]
    public float walkSpeed = 0.5f;
    [Range(1, 5)]
    public float rotationSpeed = 5f;

    public bool moving = false;
    public bool rotating = false;
    public bool waiting = false;
    public string state = "";

	// Use this for initialization
	void Start () {
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
        playerHead = GameObject.FindGameObjectWithTag("MainCamera");
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

                    GetComponent<Animator>().SetBool("reachedWaypoint", true);
                    turnTowardsPlayer();
                }

                else if (state == "leaving")
                {
                    //Move to a second waypoint and then despawn
                    var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
                    targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint2(currentFloor);

                    state = "leaving2";
                }
                else if(state == "leaving2")
                {
                    //Basic Despawmn
                    Destroy(this.gameObject);
                }
            }
        }

        if (rotating)
        {
            var targetRotation = Quaternion.LookRotation(playerHead.transform.position - transform.position);
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
            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchElevatorWaypoint();
            moving = true;
            state = "entering";

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
            var currentFloor = hotelManager.GetComponent<FloorManager>().floorPos;
            targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint(currentFloor);
            moving = true;
            state = "leaving";

            rotating = false;

            transform.parent = hotelManager.GetComponent<FloorManager>().floors[currentFloor].transform;
            GetComponent<Animator>().SetBool("reachedWaypoint", false);

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
        rotating = true;
    }
}
