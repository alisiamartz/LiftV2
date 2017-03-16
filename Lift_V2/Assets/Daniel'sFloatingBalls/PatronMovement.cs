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
                GetComponent<PatronManager>().destinationReached();
                moving = false;
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

    public void enterElevator()
    {
        targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchElevatorWaypoint();
        moving = true;
    }

    public void leaveElevator(int currentFloor)
    {
        targetWaypoint = hotelManager.GetComponent<FloorManager>().fetchFloorWaypoint(currentFloor);
        moving = true;

        rotating = false;
    }

    public void wait()
    {

    }

    public void turnTowardsPlayer()
    {
        rotating = true;
    }
}
