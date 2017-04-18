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
		
	public bool nearLever() {
		if (ObjectHighlight.nearLever)
			return true;
		return false;
	}

	public bool nearDoor() {
		if (ObjectHighlight.nearDoor)
			return true;
		return false;
	}
}
