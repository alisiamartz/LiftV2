using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour {

	/*
	 * Attached to objects that are interactable, and will interact with 
	 * 
	 * 
	 */ 
	public float rad; 
	public Collider[] controllerColliders;

	// Use this for initialization
	void Start () {
		// 
	}
	
	// Update is called once per frame
	void Update () {
		withinInteract (this.transform.position, rad);
	}


	void withinInteract(Vector3 center, float radius) {
		// Collider[] controllerColliders;
		controllerColliders = Physics.OverlapSphere(center, rad); 
		//Debug.Log ("radius "+ rad + " center " + center);
		foreach (Collider obj in controllerColliders) {
			if (obj.gameObject.tag == "grabPoint") {
				// set the color of the object
				//Debug.Log ("changed tho");
			}
		}
		// this is where we set the color back to normal
	}

}
