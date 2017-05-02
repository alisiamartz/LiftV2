using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{
    public bool doorOpen;
    public int floorPos = -1;

    public GameObject[] floors;                             //[] is the gameObject floor holder
    [HideInInspector]
    public int[] patrons = new int[7];                      //is the number of patrons waiting at each floor
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
            floorPos = targetFloor;
        }

        activeFloorIndex = targetFloor;
    }

    public GameObject fetchElevatorWaypoint()
    {
        return elevatorWaypoint;
    }

    public GameObject fetchFloorWaypoint(int floorNumber)
    {
        return floors[floorNumber].transform.Find("Waypoint").gameObject;
    }

    public GameObject fetchFloorWaypoint2(int floorNumber, int waypointIter)
    {
        if (floors[floorNumber].transform.Find("Waypoint" + waypointIter.ToString()) != null) {
            return floors[floorNumber].transform.Find("Waypoint" + waypointIter.ToString()).gameObject;
        }
        else {
            return null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("main");
        }
    }
}
