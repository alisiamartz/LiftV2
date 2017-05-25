using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFetch : MonoBehaviour {

	// attached to camera rig
	// contains functions used in AI to check the player state
	// ie. proximity to objects

//	public GameObject[] respondings;
	public GameObject spawn1;
	public GameObject spawn2; 

	public GameObject controller1;
	public GameObject controller2;

	// Use this for initialization
	void Start () {
		controller1 = GameObject.FindGameObjectWithTag ("rightControl");
		controller2 = GameObject.FindGameObjectWithTag ("leftControl");
	}
		
	void Update() {

		if (controller1 == null)
			controller1 = GameObject.FindGameObjectWithTag ("rightControl");
		if (controller2 == null)
			controller2 = GameObject.FindGameObjectWithTag ("leftControl");

		//if (respondings.Length < 2) 
		//	respondings = GameObject.FindGameObjectsWithTag ("responding");	

		// test code to see if it works
		//	if (Input.GetKeyDown (KeyCode.A)) 
		//		waitingForGesture ();
		//	if (Input.GetKeyDown (KeyCode.S))
		//		stopWaitingGesture ();

	}

	// player has collided hand with lever
	public bool nearLever() {
		if (LeverRange.nearLever)
			return true;
		return false;
	}

	// player has collided hand with door handle
	public bool nearDoor() {
		if (doorInteraction.nearDoor)
			return true;
		return false;
	}

	//Called from AI to tell the player that now is the time for a gesture
	public void waitingForGesture() {
        Debug.Log("WAITING FOR A GESTURE");
		// turn on hand haptic 
		// play a tiny particle system
		// TODO: make it good i guess
		controller1.GetComponent<ParticleSystem>().Play();
		controller2.GetComponent<ParticleSystem>().Play();
	}

	public void stopWaitingGesture() {
        Debug.Log("NO GESTURES PLS");
        // turn off that particle system now!!!!!!! yeah
		controller1.GetComponent<ParticleSystem>().Stop();
		controller2.GetComponent<ParticleSystem>().Stop();
	}


	// this is where we spawn the id and hat in the tutorial interaction
	// boss 1.1 --> spawn when timed 
	// this spawns the items on the shelf

	public void spawnHatId() {
		GameObject.Instantiate(Resources.Load("Objects/id"), spawn1.transform);
		GameObject.Instantiate(Resources.Load("Objects/hat"), spawn2.transform );
	}
}
