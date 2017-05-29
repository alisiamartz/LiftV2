using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dateMovement : MonoBehaviour {

    private GameObject targetWaypoint;
    private GameObject hotelManager;
    private GameObject playerHead;

    private GameObject rotateTarget;

    private GameObject leverRotator;

    private GameObject musicSource;

    //For new waypoint system
    private int waypointNumber = 2;

    [Range(0.5f, 5)]
    public float walkSpeed = 0.5f;
    [Range(1, 5)]
    public float rotationSpeed = 5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
