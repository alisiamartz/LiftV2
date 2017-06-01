using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;

public class DisableGesture : MonoBehaviour {


    // Get the VR Gesture Settings component on camera rig
    // Enable and disable component

    void Start() {
        //turnOff(this.gameObject);
    }

	void Update() {
		
	}

	public void turnOff(GameObject camRig) {
	//	Debug.Log ("turned off?");
		if (GetComponent<VRGestureRig> ().enabled == true) {
			GetComponent<VRGestureRig> ().enabled = false;
		}
	}

	public void turnOn(GameObject camRig) {
	//    Debug.Log ("turned on?");
		if (!LeverRange.inRange && !doorInteraction.handInRange && !Interactable.inRange) {
			GetComponent<VRGestureRig> ().enabled = true;
		}
	}

	public bool isComponentEnabled(GameObject camRig) {
		if (GetComponent<VRGestureRig> ().enabled) {
			return true;
		}
		return false;
	}

}
