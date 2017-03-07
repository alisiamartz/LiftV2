using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronManager : MonoBehaviour {

    [SerializeField]
    private GameObject elevatorManager;

    [SerializeField]
    private GameObject hotelManager;

    public float destinationFloor;

    public string status = "waiting";
	
	// Update is called once per frame
	void Update () {
        var doorOpen = elevatorManager.GetComponent<ElevatorMovement>().doorOpen;
        if (status == "waiting" && doorOpen)
        {
            GetComponent<PatronMovement>().enterElevator(hotelManager.GetComponent<Waypoints>().elevatorWaypoint);
            status = "movingIn";
        }
        if(status == "riding" && destinationFloor == elevatorManager.GetComponent<ElevatorMovement>().floorPos && doorOpen)
        {
            GetComponent<PatronMovement>().leaveElevator(hotelManager.GetComponent<Waypoints>().floorWaypoints[(int) destinationFloor]);
            status = "movingOut";
        }

	}

    public void destinationReached()
    {
        //We were moving in, so now we're riding
        if(status == "movingIn")
        {
            status = "riding";
        }
        if(status == "movingOut")
        {
            status = "finished";
        }
    }
}
