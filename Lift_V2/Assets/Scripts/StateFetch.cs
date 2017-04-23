using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFetch : MonoBehaviour {

	// attached to camera rig
	// contains functions used in AI to check the player state
	// ie. proximity to objects

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
}
