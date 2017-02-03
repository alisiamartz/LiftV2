using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the door
 * 
 * When the players controller comes in contact with the holds,
 * 	If the door is closed
 * 		the player raises it when it has trigger pressed and raises arm
 * 	If the door is open
 * 		the player closes door when it has trigger pressed and lowers arm
 * 
 */ 

public class doorInteraction : MonoBehaviour {

	public GameObject door;
	public GameObject hold;
	public GameObject doorOpen;

	public static bool collided;
	bool lifting;
	bool closing;

	float currY;
	float initX;
	float initY;
	float initZ;

	Vector3 frame1;
	Vector3 frame2;
	Vector3 frame3;

	public GameObject grabPoint;


	[SerializeField]
	SteamVR_TrackedObject trackedObj;
	[SerializeField]
	SteamVR_TrackedObject trackedObj2;


	private SteamVR_Controller.Device device { 
		get { 
			return SteamVR_Controller.Input((int)trackedObj.index); 
		} 
	}

	private SteamVR_Controller.Device device2{ 
		get { 
			return SteamVR_Controller.Input((int)trackedObj2.index); 
		} 
	}
		

	// Use this for initialization
	void Start () {
		collided = false; //setting false cause its static

		if (!door)
			door = GameObject.FindGameObjectWithTag ("door");
	
		hold = GameObject.FindGameObjectWithTag ("doorHold");

		doorOpen = GameObject.FindGameObjectWithTag ("openDoor");

		initX = door.transform.position.x;
		initY = door.transform.position.y;
		initZ = door.transform.position.z;

		if (!grabPoint)
			grabPoint = GameObject.FindGameObjectWithTag ("grabPoint");
	}
	
	// Update is called once per frame
	void Update () {

		//frame1;
		//frame2;
		//frame3;


		currY = door.transform.position.y;

		//controllerTip = trackedObj.transform.position - trackedObj.transform.up * .11f + trackedObj.transform.forward.normalized * .06f;

		if (device.GetPress (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && (collided || Vector3.Distance (hold.transform.position, grabPoint.transform.position) < (hold.transform.localScale.x * 6f))) {
			//if (Vector3.Distance (hold.transform.position, grabPoint.transform.position) < hold.transform.localScale.x * 2f) {
			Debug.Log ("hell yeah get ready to lift");
			lifting = true;

			// Move the door according to the current y position of the controller
			door.transform.position = new Vector3 (initX, (door.transform.position.y - hold.transform.position.y) + trackedObj.transform.position.y, initZ);
			// Once the door reaches a certain height
			// it goes all the way up automatically

		} else if(device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
			// if the door gets unhooked, lerp up just a bit 
		
		} else {
			// if door not at a certain point yet
			// it goes down
			if (door.transform.position.y < 2f && lifting) {
				//if (door.transform.position.y >= initY) { // while the door position is greater than the y position
				door.transform.position = Vector3.MoveTowards (door.transform.position, new Vector3 (initX, initY, initZ), 2f * Time.deltaTime);
				//}
			}

		}

		if (lifting) {
			// if past certain point
			if (door.transform.position.y > 2f) {
				door.transform.position = Vector3.Lerp (door.transform.position, doorOpen.transform.position, Time.deltaTime);
				Debug.Log ("Attempt to lerP");
			} else if(door.transform.position == doorOpen.transform.position) {
				lifting = false;
				Debug.Log ("I WANT THIS");
			}
		}
	}


}
