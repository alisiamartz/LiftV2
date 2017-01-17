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
 */ 

public class Lever : MonoBehaviour {

	public GameObject lever;
	public GameObject handle;



	public float currValue;

	// Use this for initialization
	void Start () {
		if (!lever) lever = GameObject.FindGameObjectWithTag ("lever");
		if (!handle) handle = GameObject.FindGameObjectWithTag ("handle");

	}
	
	// Update is called once per frame
	void Update () {

		currValue = handle.GetComponent<NVRLever> ().CurrentValue;

		if (currValue < .5f) {
			// value is going down
			Debug.Log("Going down! " + currValue);
		} else if (currValue > .5f) {
			// value is going up
			Debug.Log("Going up! " + currValue);

		} else {
			Debug.Log("Stopped! " + currValue);

		}

	}
}
