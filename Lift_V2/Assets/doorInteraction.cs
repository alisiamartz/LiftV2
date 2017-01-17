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

	bool touching; 

	public Vector3 controllerTip;

	[SerializeField]
	SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device device { 
		get { 
			return SteamVR_Controller.Input((int)trackedObj.index); 
		} 
	}


	[SerializeField]
	SteamVR_TrackedObject trackedObj2;

	private SteamVR_Controller.Device device2{ 
		get { 
			return SteamVR_Controller.Input((int)trackedObj2.index); 
		} 
	}
		

	// Use this for initialization
	void Start () {
		if (!door) door = GameObject.FindGameObjectWithTag ("door");

		holds = GameObject.FindGameObjectsWithTag ("doorHold");
	}
	
	// Update is called once per frame
	void Update () {

		controllerTip = trackedObj.transform.position - trackedObj.transform.up * .11f + trackedObj.transform.forward.normalized * .06f;

		if (device.GetPressDown (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && touching) {
			foreach (GameObject hold in holds) {
					if (Vector3.Distance (hold.transform.position, controllerTip) < hold.transform.localScale.x) {
					Debug.Log ("hell yeah get ready to lift");

				}
			}

		}

	}

	void OnTriggerEnter(Collider col) {
		// if collided with doorHold
		if (col.gameObject.tag == "doorHold") {
				touching = true;
			Debug.Log (touching);
		}
	}

	void OnTriggerExit(Collider col) {
		// no longer touching doorhold
		if (col.gameObject.tag == "doorHold") {
				touching = false;
		}
	}

}
