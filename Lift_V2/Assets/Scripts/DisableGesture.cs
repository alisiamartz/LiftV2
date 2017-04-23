using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR;

public class DisableGesture : MonoBehaviour {

	// Get the VR Gesture Settings component on camera rig
	// Enable and disable component

	public static void turnOff(GameObject camRig) {
		//Debug.Log ("turned off?");
		camRig.GetComponent<VRGestureRig> ().enabled = false;
	}
	public static void turnOn(GameObject camRig) {
		//Debug.Log ("turned on?");
		camRig.GetComponent<VRGestureRig> ().enabled = true;
	}
	public static bool isComponentEnabled(GameObject camRig) {
		if (camRig.GetComponent<VRGestureRig> ().enabled) {
			// yes enabled
			return true;
		}
		return false;
	}
}
