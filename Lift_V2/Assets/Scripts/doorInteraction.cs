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
	public GameObject[] holds;
	public GameObject doorOpen;

	bool collided;
	bool lifting;
	bool closing;

	float currY;
	float initX;
	float initY;
	float initZ;

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
		if (!door)
			door = GameObject.FindGameObjectWithTag ("door");

		holds = GameObject.FindGameObjectsWithTag ("doorHold");

		doorOpen = GameObject.FindGameObjectWithTag ("openDoor");

		initX = door.transform.position.x;
		initY = door.transform.position.y;
		initZ = door.transform.position.z;

		if (!grabPoint)
			grabPoint = GameObject.FindGameObjectWithTag ("grabPoint");
	}
	
	// Update is called once per frame
	void Update () {

		currY = door.transform.position.y;

		//controllerTip = trackedObj.transform.position - trackedObj.transform.up * .11f + trackedObj.transform.forward.normalized * .06f;

		if (device.GetPress (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && collided) {
			foreach (GameObject hold in holds) {
				if (Vector3.Distance (hold.transform.position, grabPoint.transform.position) < hold.transform.localScale.x) {
					Debug.Log ("hell yeah get ready to lift");
					lifting = true;
				
					//door.transform.SetParent (trackedObj.transform);
					// Move the door according to the current y position of the controller
					door.transform.position = new Vector3 (initX, (door.transform.position.y - hold.transform.position.y)+trackedObj.transform.position.y, initZ);
					// Once the door reaches a certain height
					// it goes all the way up automatically


				}
			}
		}

		if (lifting) {
			// if past certain point
			if (door.transform.position.y > 2f) {
				door.transform.position = Vector3.Lerp (door.transform.position, doorOpen.transform.position, Time.deltaTime);
				Debug.Log ("Attempt to lerP");
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		// if collided with doorHold
		if (col.gameObject.tag == "doorHold") {
				collided = true;
			Debug.Log (collided);
		}
	}

	void OnTriggerExit(Collider col) {
		// no longer touching doorhold
		if (col.gameObject.tag == "doorHold") {
				collided = false;
		}
	}
}
