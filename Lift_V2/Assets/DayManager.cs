using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour {

    [Header("Patrons")]
    public GameObject[] day1;
    public GameObject[] day2;
    public GameObject[] day3;
    public GameObject[] day4;
    public GameObject[] day5;

    //In the days array 
    private GameObject[][] days = new GameObject[5][];

    [Header("Timeline")]
    [Range(1, 5)]
    public int day = 1;
    //[Range(1, 4)]
    public int patronNumber = 1;

    // Use this for initialization
    void Start () {

        days[0] = day1; days[1] = day2; days[2] = day3; days[3] = day4; days[4] = day5;
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, 10);

        patronNumber -= 1;
        nextPatron();
    }

    //Spawns the next patron in the timeline, if at the end of a day move to next day
    public void nextPatron() {
        if(patronNumber < days[day-1].Length) {

            patronNumber += 1;

            var patronPrefab = days[day-1][patronNumber-1];
            var startFloor = 0;

            Quaternion basePatronRotation = patronPrefab.transform.rotation;

            var newPatron = Instantiate(patronPrefab, GetComponent<FloorManager>().fetchFloorWaypoint(startFloor).transform.position, basePatronRotation);
            newPatron.transform.parent = GetComponent<FloorManager>().floors[startFloor].transform;

            //floorPanel.GetComponent<FloorsPanel>().lightOn(startFloor);
            GetComponent<FloorManager>().patrons[startFloor] += 1;
        }
        else {
            nextDay();
        }
    }

    public void nextDay() {
        if (day < 5) {
            patronNumber = 0;
            day += 1;

            nextPatron();

            SteamVR_Fade.Start(Color.clear, 0);
            SteamVR_Fade.Start(Color.black, 5);

            //dayReset();
        }
        //We've reached the end of the game timeline
        else {
            
        }
    }


    public void dayReset() {
        SteamVR_Fade.Start(Color.black, 5);
        SteamVR_Fade.Start(Color.clear, 5);
    }
}
