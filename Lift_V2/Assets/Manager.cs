using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public float floorPos;                              //The number floor the elevator is on. 
    public float floorRounding;                         //How close to the floor the elevator must be for it to round
    public float liftSpeedMax;                          //The maximum speed to elevator can reach
    public float timeToMax;                             //The time it takes to reach max speed
    public float timeToStop;                            //The time it takes to halt to a complete stop
    private float liftSpeedCurrent;    //The current speed of the elevator
    private float liftSpeedIter;       //The current rate of speed increase for the elevator
    private float liftSpeedWinder;     //The current rate of speed decrease for the elevator
    private bool windingDown;                           //Whether or not elevator is winding down to a halt

	// Use this for initialization
	void Start () {
        floorPos = 0f;
        liftSpeedCurrent = 0f;
        liftSpeedIter = liftSpeedMax / timeToMax;
        liftSpeedWinder = liftSpeedMax / timeToStop;
        windingDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        floorPos += liftSpeedCurrent;

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
        if (windingDown == true && (Mathf.Abs(liftSpeedCurrent) > liftSpeedWinder)) {
            if (liftSpeedCurrent > 0) { liftSpeedCurrent -= liftSpeedWinder; }
            else { liftSpeedCurrent += liftSpeedWinder;  }
            //If elevator within range of floor
            if(Mathf.Abs(floorPos - Mathf.Round(floorPos)) < floorRounding) {
                //Wind down into the floor
                //TO DO -----!-!
            }
        }
        else if (windingDown) {
            windingDown = false;
            liftSpeedCurrent = 0f;
        }
    }
}
