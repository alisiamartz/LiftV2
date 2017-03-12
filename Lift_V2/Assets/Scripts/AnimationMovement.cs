using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMovement : MonoBehaviour {

	private GameObject targetWaypoint;
	private GameObject hotelManager;

	[Range(0.5f, 5)]
	public float walkSpeed = 10f;

	public bool moving = false;

	public GameObject testWP;

	// Use this for initialization
	void Start () {
		hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
	}

	// Update is called once per frame
	void Update () {

			float step = 1.5f * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, testWP.transform.position, Time.deltaTime);

			//if (transform.position == targetWaypoint.transform.position)
			if (transform.position == testWP.transform.position)
			{
				//We've reached our destination
				//Change animation to stop
			Debug.Log("did this happen");
				//GetComponent<Animator>().SetTrigger("stop_walking");
				GetComponent<Animator> ().SetBool ("reachedWaypoint", true);

				GetComponent<PatronManager>().destinationReached();
				moving = false;
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
	}

	public void wait()
	{

	}
}
