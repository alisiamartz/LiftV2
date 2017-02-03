using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	// This checks what the controller is colliding with 
	void OnTriggerEnter(Collider col) {
		// if collided with doorHold
		switch(col.gameObject.tag) {
			case "doorHold":
				doorInteraction.collided = true;
				Debug.Log ("should be true/door " +doorInteraction.collided);
				break;
			case "handle":
				Lever.collided = true;
				Debug.Log ("should be true/handle " +Lever.collided);
				break;
			default:
				break;
		}
	}

	void OnTriggerExit(Collider col) {
		// no longer touching doorhold
		switch(col.gameObject.tag) {
			case "doorHold":
				doorInteraction.collided = false;
				Debug.Log ("should be false/door " +doorInteraction.collided);
				break;
			case "handle":
				Lever.collided = true;
				Debug.Log ("should be false/handle " +Lever.collided);
				break;
			default:
				break;
		}
	}

}
