using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeline : MonoBehaviour {

    [SerializeField]
    private int dayNumber = 1;

    private int eventNumber = -1;

    public int[] startingFloors;
    public int[] targetFloors;


	// Use this for initialization
	void Start () {
        nextEvent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextEvent()
    {
        eventNumber++;
        if (eventNumber < startingFloors.Length)
        {
            GetComponent<PatronSpawner>().spawnPatron(startingFloors[eventNumber], targetFloors[eventNumber]);
        }
    }
}
