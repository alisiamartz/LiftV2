using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour {

    [Header("Patrons")]
    public string[] day1;
    public string[] day2;
    public string[] day3;
    public string[] day4;
    public string[] day5;

    //In the days array 
    private string[][] days = new string[5][];

    [Header("Timeline")]
    [Range(1, 5)]
    public int day = 1;
    //[Range(1, 4)]
    public int patronNumber = 1;

    [Header("Day Reset")]
    public int fadeToBlackTime;
    public int timeInBlack;
    public int unFadeTime;
    private GameObject elevatorManager;
    public doorInteraction liftableDoor;
    private GameObject leverRotator;

    private Patrons patron = new Patrons();

    // Use this for initialization
    void Start () {
        elevatorManager = GameObject.FindGameObjectWithTag("ElevatorManager");
        leverRotator = GameObject.FindGameObjectWithTag("lever");

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

            var patronObject = patron.fetchPatron(days[day - 1][patronNumber - 1]);
            var patronPrefab = patronObject.prefab;
            var startFloor = patronObject.startFloor;

            Quaternion basePatronRotation = patronPrefab.transform.rotation;

            var newPatron = Instantiate(patronPrefab, GetComponent<FloorManager>().fetchFloorWaypoint(startFloor).transform.position, basePatronRotation);

            //sets script
            patron.configPatron(ref newPatron, days[day - 1][patronNumber - 1]);

            newPatron.transform.parent = GetComponent<FloorManager>().floors[startFloor].transform;

            //Set the light on the elevator
            leverRotator.GetComponent<patronWaiting>().lightOn(startFloor);

            //floorPanel.GetComponent<FloorsPanel>().lightOn(startFloor);

            //GetComponent<FloorManager>().patrons[startFloor] += 1;
        }
        else {
            nextDay();
        }
    }

    public void nextDay() {
        if (day < 5) {
            patronNumber = 0;
            day += 1;

            SteamVR_Fade.Start(Color.clear, 0);
            SteamVR_Fade.Start(Color.black, fadeToBlackTime);

            StartCoroutine(ExecuteAfterTime(5f + timeInBlack));
        }
        //We've reached the end of the game timeline
        else {
            
        }
    }


    public void dayReset() {
        //Close the elevator door
        //Reset elevator position etc.
        leverRotator.GetComponent<LeverRotation>().resetLever();
        elevatorManager.GetComponent<ElevatorMovement>().arriveAtFloor(0);
        liftableDoor.closeDoor();

        nextPatron();

        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, unFadeTime);
    }

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        dayReset();
    }
}
