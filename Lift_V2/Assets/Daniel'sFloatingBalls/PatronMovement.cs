using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronMovement : MonoBehaviour {

    private GameObject targetWaypoint;

    [Range(0.5f, 5)]
    public float walkSpeed = 10f;

    public bool moving = false;

	// Use this for initialization
	void Start () {
        
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
    }

    public void enterElevator(GameObject elevatorWaypoint)
    {
        targetWaypoint = elevatorWaypoint;
        moving = true;
    }

    public void leaveElevator(GameObject hotelWaypoint)
    {
        targetWaypoint = hotelWaypoint;
        moving = true;
    }

    public void wait()
    {

    }
}
