using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

/*
 * This script is attached to the Manager
 * It keeps track of the value of the lever.
 * 
 * >.5 means that it is going up 
 * =.5 means that it is in the middle
 * <.5 means that it is going down
 * 
 * When grabbed
 * 
 * 
 */ 

public class Lever : MonoBehaviour {

	public GameObject lever;
	public GameObject handle;
	public GameObject grabPoint;

	public Vector3 contPos;
	public Vector3 lookDir;
	public Quaternion deltaRot;
	public Quaternion leverRotation;

	bool raising;
	bool collided;
	bool grabbing;

	public Rigidbody rb;

	public float currValue;



	[SerializeField]
	SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device device { 
		get { 
			return SteamVR_Controller.Input((int)trackedObj.index); 
		} 
	}

	// Use this for initialization
	void Start () {
		if (!lever) lever = GameObject.FindGameObjectWithTag ("lever");
		if (!handle) handle = GameObject.FindGameObjectWithTag ("handle");



		//rb = lever.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

		contPos = trackedObj.transform.position;
		leverRotation = lever.transform.rotation;
		//currValue = handle.GetComponent<NVRLever> ().CurrentValue;

		leverRotation = Quaternion.LookRotation(lookDir);


		lookDir = grabPoint.transform.position-handle.transform.position;

		if (grabbing) {

			//lever.transform.LookAt(trackedObj.transform);
			//lookDir = grabPoint.transform.position-handle.transform.position;
			//Debug.Log ("grab point "+grabPoint.transform.position);
			//Debug.Log ("lever "+ lever.transform.position);

			//lookDir.y = 0;
			// vector3 (0, y, 0) 
			//leverRotation = Quaternion.LookRotation(lookDir);
			//rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
			Debug.Log ("We are in grab mode");
			//leverRotation.x = 0f;
			//leverRotation.z = 0f;
			Debug.Log ("leverrotations "+leverRotation);

			lever.transform.rotation = new Quaternion(0.0f, leverRotation.y, 0.0f, leverRotation.z);
			//lever.transform.rotation = Quaternion.Slerp(lever.transform.rotation, leverRotation, Time.deltaTime);


			//Debug.Log (lever.transform.localEulerAngles.y);
			//Debug.Log (lever.transform.localRotation.eulerAngles.y);
			//Debug.Log (lever.transform.rotation.y);
			//if (lever.transform.rotation.y > -50f && lever.transform.rotation.y < 50) {
//				Debug.Log (rb.rotation.eulerAngles.y);
			//	deltaRot = Quaternion.Euler (contPos * Time.deltaTime);
			//	rb.MoveRotation (rb.rotation * deltaRot);
			//} else {s
		//		rb.constraints = RigidbodyConstraints.FreezeAll;
		//	}
	//	} else {
	//		Debug.Log ("We are NOT!!!! grab mode");
		//	rb.constraints = RigidbodyConstraints.FreezeAll;
		}

		if (currValue < .4f) {
			// value is going down
			//Debug.Log("Going down! " + currValue);
		} else if (currValue > .6f) {
			// value is going up
			//Debug.Log("Going up! " + currValue);
		} else {
			//Debug.Log("Stopped! " + currValue);
		}

		if (device.GetPress (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)&& collided) {
			grabbing = true;

		} else if (device.GetPressUp (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
			grabbing = false;
		}

		//		if (Vector3.Distance (hold.transform.position, grabPoint.transform.position) < hold.transform.localScale.x) {

	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "grabPoint") {
			collided = true;
			Debug.Log (collided);
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == "grabPoint") {
			collided = false;			
			Debug.Log (collided);

		}
	}
}

