using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFetch : MonoBehaviour {

	// attached to camera rig
	// contains functions used in AI to check the player state
	// ie. proximity to objects

	public GameObject[] respondings;
	public GameObject spawn1;
	public GameObject spawn2; 

	// Use this for initialization
	void Start () {
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
		// turn on hand haptic 
		// play a tiny particle system
		// TODO: make it good i guess
		foreach (GameObject g in respondings) {
			g.GetComponent<ParticleSystem> ().Play ();		}

	}

	public void stopWaitingGesture() {
		// turn off that particle system now!!!!!!! yeah
		foreach (GameObject g in respondings) {
			g.GetComponent<ParticleSystem> ().Stop ();
		}
	}

	void Update() {

		if (respondings.Length < 2) 
			respondings = GameObject.FindGameObjectsWithTag ("responding");	
		
		// test code to see if it works
		if (Input.GetKeyDown (KeyCode.A))
			spawnHatId ();
//		if (Input.GetKeyDown (KeyCode.S))
//			stopWaitingGesture ();

	}

	// this is where we spawn the id and hat in the tutorial interaction
	// boss 1.1 --> spawn when timed 
	// this spawns the items on the shelf

	public void spawnHatId() {
		GameObject.Instantiate(Resources.Load("Objects/id"), spawn1.transform);
		GameObject.Instantiate(Resources.Load("Objects/hat"), spawn2.transform );
	}
}
