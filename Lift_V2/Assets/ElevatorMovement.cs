using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour {

    public float floorPos;                              //The number floor the elevator is on. 
    public float floorRounding;                         //How close to the floor the elevator must be for it to round
    public float maxSpeedToRound;                       //The maximum speed the elevator can be moving for it to round
    public float liftSpeedMax;                          //The maximum speed to elevator can reach
    public float timeToMax;                             //The time it takes to reach max speed
    public float timeToStop;                            //The time it takes to halt to a complete stop
    private float liftSpeedCurrent;    //The current speed of the elevator
    private float liftSpeedIter;       //The current rate of speed increase for the elevator
    private float liftSpeedWinder;     //The current rate of speed decrease for the elevator
    private bool windingDown;                           //Whether or not elevator is winding down to a halt

    private bool magnet;
    public float magnetForce;

	// Use this for initialization
	void Start () {
        floorPos = 0f;
        liftSpeedCurrent = 0f;
        liftSpeedIter = liftSpeedMax / timeToMax;
        liftSpeedWinder = liftSpeedMax / timeToStop;
        windingDown = false;

        magnet = false;
	}
	
	// Update is called once per frame
	void Update () {
        //If within bounds of elevator
        if ((floorPos > -0.2 || liftSpeedCurrent > 0) && (floorPos < 5.2 || liftSpeedCurrent < 0)) {
            floorPos += liftSpeedCurrent;
        }
        else {
            //We've hit the top or the bottom
        }
        if (magnet) {
            if (Mathf.Abs(floorPos - Mathf.Round(floorPos)) <= magnetForce) {
                magnet = false;
                windingDown = false;
                liftSpeedCurrent = 0f;
                floorPos = Mathf.Round(floorPos);
            }
            else {
                if (floorPos > Mathf.Round(floorPos)) { floorPos -= magnetForce; }
                else { floorPos += magnetForce; }
            }
        }

        if (Input.GetKey("d")){
            windingDown = false;
            if (liftSpeedCurrent < liftSpeedMax)
            {
                liftSpeedCurrent += liftSpeedIter;
            }
        }
        if (Input.GetKey("a")){
            windingDown = false;
            if (liftSpeedCurrent < liftSpeedMax)
            {
                liftSpeedCurrent -= liftSpeedIter;
            }
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("a")) {
            windingDown = true;
        }
        if (windingDown && (Mathf.Abs(liftSpeedCurrent) > liftSpeedWinder)) {
            //If elevator within range of floor
            if (Mathf.Abs(floorPos - Mathf.Round(floorPos)) < floorRounding && Mathf.Abs(liftSpeedCurrent) <= Mathf.Abs(maxSpeedToRound)) {
                //turn magnetic effect on
                magnet = true;
            }
            else {
                if (liftSpeedCurrent > 0) { liftSpeedCurrent -= liftSpeedWinder; }
                else { liftSpeedCurrent += liftSpeedWinder; }
            }
        }
        else if (windingDown && magnet == false) {
            windingDown = false;
            liftSpeedCurrent = 0f;
        }
    }
}
