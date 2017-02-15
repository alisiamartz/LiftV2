using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour {

    public float floorPos;                              //The number floor the elevator is on. 
    public float floorRounding;                         //How close to the floor the elevator must be for it to round
    public float maxSpeedToRound;                       //The maximum speed the elevator can be moving for it to round
    public float liftSpeedMax;                          //The maximum speed to elevator can reach
    public float maxIter;                             //The maximum acceleration the elevator can take. (When the lever is at max or min)
    public float timeToStop;                            //The time it takes to halt to a complete stop
    private float liftSpeedCurrent;    //The current speed of the elevator
    private float liftSpeedIter;       //The current rate of speed increase for the elevator
    private float liftSpeedWinder;     //The current rate of speed decrease for the elevator
    public bool windingDown;                           //Whether or not elevator is winding down to a halt

    private float previousFloorPos;
    private float lastPassedFloor;

    private bool magnet;
    public float magnetForce;

    public int maxVibration;

    private GameObject lever;

    public string floorPassingSound;

    // Use this for initialization
    void Start () {
        floorPos = 0f;
        liftSpeedCurrent = 0f;
        liftSpeedWinder = liftSpeedMax / timeToStop;
        windingDown = false;

        magnet = false;

        lever = GameObject.FindGameObjectWithTag("lever");
	}
	
	// Update is called once per frame
	void Update () {
        //If within bounds of elevator
        if ((floorPos > -0.2 || liftSpeedCurrent > 0) && (floorPos < 5.2 || liftSpeedCurrent < 0)) {
            floorPos += liftSpeedCurrent;
        }
        else {
            //We've hit the top or the bottom
            liftSpeedCurrent = 0;
        }
        //If Elevator is moving
        if (liftSpeedCurrent != 0)
        {
            //Haptic Feedback
            var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort) Mathf.FloorToInt(Mathf.Abs(liftSpeedCurrent) / liftSpeedMax * maxVibration));

            //Check for passing floors
            if ((floorPos == Mathf.Round(floorPos) || (floorPos - Mathf.Round(floorPos) * previousFloorPos - Mathf.Round(floorPos) < 0)) && Mathf.Round(floorPos) != lastPassedFloor){
                //We're passing a floor
                Debug.Log("passing floor");
                floorPassingSound.PlaySound(transform.position);
                lastPassedFloor = Mathf.Round(floorPos);
            }
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

        if (lever.GetComponent<LeverRotation>().decensionRate != 0){
            windingDown = false;
            liftSpeedCurrent = -liftSpeedMax * lever.GetComponent<LeverRotation>().decensionRate;
            
        }
        if (lever.GetComponent<LeverRotation>().ascensionRate != 0){
            windingDown = false;
            liftSpeedCurrent = liftSpeedMax * lever.GetComponent<LeverRotation>().ascensionRate;
            
        }
    
        if (lever.GetComponent<LeverRotation>().ascensionRate == 0 && lever.GetComponent<LeverRotation>().decensionRate == 0) {
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

        previousFloorPos = floorPos;
    }
}
