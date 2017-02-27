using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

    public bool doorOpen;

    public GameObject[]floors;
    private int activeFloorIndex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called from elevator movement when new floor is reached
    public void loadNewFloor(int targetFloor) {
        if (floors[activeFloorIndex] != floors[targetFloor])
        {
            //Turn off the previously active floor
            floors[activeFloorIndex].SetActive(false);
            //Turn on the next floor
            floors[targetFloor].SetActive(true);
        }

        activeFloorIndex = targetFloor;
    }
}
