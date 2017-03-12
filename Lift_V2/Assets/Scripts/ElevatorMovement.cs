using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour {

    [Header("Global Variables")]
    public float floorPos;                              //The number floor the elevator is on. 
    public float liftSpeedCurrent;                      //The current speed of the elevator
    public bool doorOpen;                               //If the elevator door is open, TRUE is open FALSE is closed

    [Header("Movement Variables")]
    public float maxSpeedToRound;                       //The maximum speed the elevator can be moving for it to round
    public float liftSpeedMax;                          //The maximum speed to elevator can reach
    public float maxIter;                              //The maximum acceleration the elevator can take. (When the lever is at max or min)
    public float timeToStop;                            //The time it takes to halt to a complete stop
    private float liftSpeedIter;                       //The current rate of speed increase for the elevator
    private float liftSpeedWinder;                    //The current rate of speed decrease for the elevator
    [HideInInspector]
    public bool windingDown;                           //Whether or not elevator is winding down to a halt

    [Header("Magnet Variables")]
    public float floorRounding;                         //How close to the floor the elevator must be for it to round
    private float previousFloorPos;
    private float lastPassedFloor = -1;
    private bool magnet;
    public float magnetForce;

    [Header("Effects Variables")]
    public int maxVibration;

    [Header("Sounds")]
    public string floorPassingSound;

    private GameObject lever;
    private GameObject hotelManager;

    //Used for hitting the max or min of the elevator bounds
    private bool firstHit = false;

    // Use this for initialization
    void Start () {
        floorPos = 0f;
        liftSpeedCurrent = 0f;
        liftSpeedWinder = liftSpeedMax / timeToStop;
        windingDown = false;

        magnet = false;

        lever = GameObject.FindGameObjectWithTag("lever");
        hotelManager = GameObject.FindGameObjectWithTag("HotelManager");
	}
	
	// Update is called once per frame
	void Update () {
        //Get Unity Components
        var leverRotation = lever.GetComponent<LeverRotation>();

        //If within bounds of elevator
        if ((floorPos > -0.2 || liftSpeedCurrent > 0) && (floorPos < 5.2 || liftSpeedCurrent < 0)) {
            floorPos += liftSpeedCurrent;
            if (firstHit)
            {
                firstHit = false;
            }
        }
        else {
            //We've hit the top or the bottom
            liftSpeedCurrent = 0;
            if (firstHit == false)
            {
                //TO DO ADD A SOUND EFFECT HERE --------------------------------------------------------------------------------------------------------------------
                GameObject.FindGameObjectWithTag("ElevatorManager").GetComponent<grabHaptic>().triggerBurst(20, 2);
                firstHit = true;
            }
        }

        //Send the elevator speed to the movementSound Script
        GetComponent<movementSound>().liftSpeed = liftSpeedCurrent;


        //If Elevator is moving
        if (liftSpeedCurrent != 0)
        {
            //Haptic Feedback
            var deviceIndex1 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            SteamVR_Controller.Input(deviceIndex1).TriggerHapticPulse((ushort) Mathf.FloorToInt(Mathf.Abs(liftSpeedCurrent) / liftSpeedMax * maxVibration));
            var deviceIndex2 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
            SteamVR_Controller.Input(deviceIndex2).TriggerHapticPulse((ushort)Mathf.FloorToInt(Mathf.Abs(liftSpeedCurrent) / liftSpeedMax * maxVibration));

            //Check for passing floors
            if ((floorPos == Mathf.Round(floorPos) || ((floorPos - Mathf.Round(floorPos)) * (previousFloorPos - Mathf.Round(floorPos)) < 0)) && Mathf.Round(floorPos) != lastPassedFloor){
                //We're passing a floor
                Debug.Log("passing floor");
                floorPassingSound.PlaySound(transform.position);
                lastPassedFloor = Mathf.Round(floorPos);
            }
        }
        //When magnetizing to floor
        if (magnet) {
            if (Mathf.Abs(floorPos - Mathf.Round(floorPos)) <= magnetForce) {
                //We've stopped exactly at a floor
                magnet = false;
                windingDown = false;
                liftSpeedCurrent = 0f;
                floorPos = Mathf.Round(floorPos);

                //Play the floor ding
                floorPassingSound.PlaySound(transform.position);

                //Load in the floor stopped at
                hotelManager.GetComponent<FloorManager>().loadNewFloor((int) floorPos);

                //Tell the hotel manager we've arrived at a floor

            }
            else {
                if (floorPos > Mathf.Round(floorPos)) { floorPos -= magnetForce; }
                else { floorPos += magnetForce; }
            }
        }

        if (leverRotation.decensionRate != 0){
            windingDown = false;
            liftSpeedCurrent = -liftSpeedMax * leverRotation.decensionRate;
            
        }
        if (leverRotation.ascensionRate != 0){
            windingDown = false;
            liftSpeedCurrent = liftSpeedMax * leverRotation.ascensionRate;
            
        }
    
        if (leverRotation.ascensionRate == 0 && leverRotation.decensionRate == 0) {
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
            if(floorPos == Mathf.Round(floorPos))
            {
                lastPassedFloor = floorPos;
            }
            else
            {
                lastPassedFloor = -1;
            }
        }

        previousFloorPos = floorPos;
    }

    public void doorOpened()
    {
        doorOpen = true;
    }

    public void doorClosed()
    {
        doorOpen = false;
    }
}
