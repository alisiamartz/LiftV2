using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronManager : MonoBehaviour
{

    private GameObject elevatorManager;
    private GameObject hotelManager;

    [Header("References")]
    public int destinationFloor;

    public string status = "waiting";

    public GameObject floorRequest;

    [Header("Character Values")]
    public float enterElevatorWaitTime = 5f;

    private string currentTimer = "";
    private float timer = 0;

    void Start()
    {
        elevatorManager = GameObject.FindGameObjectWithTag("ElevatorManager");
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
    }

    // Update is called once per frame
    void Update()
    {
        floorRequest.GetComponent<TextMesh>().text = destinationFloor.ToString();

        var doorOpen = elevatorManager.GetComponent<ElevatorMovement>().doorOpen;
        if (status == "waiting" && doorOpen)
        {
            //Impliment wait time before triggering enter elevator
            if(currentTimer == "")
            {
                currentTimer = "enter";
                timer = enterElevatorWaitTime;
            }
            if (currentTimer == "enter")
            {
                if (timer <= 0)
                {
                    GetComponent<PatronMovement>().enterElevator();
                    status = "movingIn";
                    transform.parent = null;
                }
            }
        }
        if (status == "riding" && destinationFloor == elevatorManager.GetComponent<ElevatorMovement>().floorPos && doorOpen)
        {
            GetComponent<PatronMovement>().leaveElevator(destinationFloor);
            status = "movingOut";
            transform.parent = hotelManager.GetComponent<FloorManager>().floors[destinationFloor].transform;

            GetComponent<Animator>().SetBool("reachedWaypoint", false);
        }

        //Timers
        if(timer  > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
            currentTimer = "";
        }

    }

    public void destinationReached()
    {
        //We were moving in, so now we're riding
        if (status == "movingIn")
        {
            //Stop the walking animation and go to idle
            GetComponent<Animator>().SetBool("reachedWaypoint", true);

            //Turn towards the player
            GetComponent<PatronMovement>().turnTowardsPlayer();

            status = "riding";
        }
        if (status == "movingOut")
        {
            status = "finished";

            //Tell the patron manager to start the next event
            hotelManager.GetComponent<DayTimeline>().nextEvent();
        }
    }
}
