using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	string item;

	// Placed on objects that are interactable

	// Use this for initialization
	void Start () {
		item = this.tag;
	}
	
	// Update is called once per frame
	void Update () {

		switch (item) {
		case "ID":




			break;

		case "Hat":





			break;

		default:
			break;

		}

		
	}
}
