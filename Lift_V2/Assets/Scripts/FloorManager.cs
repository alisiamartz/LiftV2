using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public bool doorOpen;
    public int floorPos;

    public GameObject floorPanel;

    public GameObject[] floors;                             //[] is the gameObject floor holder
    [HideInInspector]
    public int[] patrons = new int[6];                      //is the number of patrons waiting at each floor
    private int activeFloorIndex;

    [SerializeField]
    private GameObject elevatorWaypoint;

    //Called from elevator movement when new floor is reached
    public void loadNewFloor(int targetFloor)
    {
        if (floors[activeFloorIndex] != floors[targetFloor])
        {
            //Turn off the previously active floor
            floors[activeFloorIndex].SetActive(false);
            //Turn on the next floor
            floors[targetFloor].SetActive(true);
        }

        activeFloorIndex = targetFloor;

        //Tell the hotel manager we've arrived at a floor
        floorPanel.GetComponent<FloorsPanel>().lightOff(targetFloor);
    }

    public GameObject fetchElevatorWaypoint()
    {
        return elevatorWaypoint;
    }

    public GameObject fetchFloorWaypoint(int floorNumber)
    {
        return floors[floorNumber].transform.Find("Waypoint").gameObject;
    }
}
