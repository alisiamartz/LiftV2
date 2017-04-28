using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRotation : MonoBehaviour {

    public float leverRotation;
    public float maxRotation;
    public float minRotation;

    private float positionToRotation;
    private float previousHandPosition;

    public bool grabbed;
    [HideInInspector]
    public GameObject grabHand;

    private bool reset;
    public float resetSpeed = 0.1f;

    [Header("Floors")]
    public float[] floors;
    public bool setToFloor = false;
    public int holdingVibration;
    private GameObject elevatorManager;

    [Header("Jiggle Effect")]
    public float jiggleStrength = 10;
    private bool jiggling = false;
    private int jiggleTimer = 0;
    public int jiggleLength = 50;
    [Range(1, 10)]
    public int jiggleSpeed = 6;
    private int jiggleDivisor;

    [Header("Sounds")]
    public string leverResetSound;
    public string jiggleSound;

    void Start(){
        positionToRotation = Mathf.Abs(maxRotation - minRotation) / 0.6f;
        reset = true;

        elevatorManager = GameObject.FindGameObjectWithTag("ElevatorManager");
        leverRotation = floors[(int)elevatorManager.GetComponent<ElevatorMovement>().floorPos];

        jiggleDivisor = 10 - jiggleSpeed;
    }

    void Update(){
        if (grabbed){
            if (reset) {
                previousHandPosition = grabHand.transform.position.z;
                reset = false;
                Debug.Log(previousHandPosition);
            }

            //Debug.Log("previous: " + previousHandPosition + " and current: " + grabHand.transform.position.z);
            if (previousHandPosition != grabHand.transform.position.z) {
                //if lever has been moved 
                setToFloor = false;

                //What direction the lever is being moved in
                if (previousHandPosition - grabHand.transform.position.z < 0) {
                    leverRotation += Mathf.Abs(previousHandPosition - grabHand.transform.position.z) * positionToRotation;
                }
                else {
                    leverRotation -= Mathf.Abs(previousHandPosition - grabHand.transform.position.z) * positionToRotation;
                }
            }

            previousHandPosition = grabHand.transform.position.z;

            //Small haptic vibration while holding lever

            //What hand is holding the lever
            var deviceIndex = 0;
            if(grabHand.gameObject.tag == "rightControl") {
                deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
            }
            else {
                deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            }

            SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort)holdingVibration);
        }
        else {
            reset = true;
        }

        //Bounds checks
        if (leverRotation > maxRotation) {
            leverRotation = maxRotation;
        }
        else if (leverRotation < minRotation) {
            leverRotation = minRotation;
        }

        //Check if rotation is on a floor
        if (!setToFloor && !grabbed) {
            var lowestDistance = 999f;
            var lowestIndex = -1;

            for (var i = 0; i < floors.Length; i++) {
                if(Mathf.Abs(floors[i] - leverRotation) < lowestDistance) {
                    lowestDistance = Mathf.Abs(floors[i] - leverRotation);
                    lowestIndex = i;
                }
            }
            //We've identified a new target floor
            //Lerp to the new rotation
            if (leverRotation > floors[lowestIndex] - resetSpeed && leverRotation < floors[lowestIndex] + resetSpeed) {
                leverRotation = floors[lowestIndex];
                setToFloor = true;
                elevatorManager.GetComponent<ElevatorMovement>().newDoorTarget(lowestIndex);
            }
            else {
                if (leverRotation < floors[lowestIndex]) {
                    leverRotation += resetSpeed;
                }
                else {
                    leverRotation -= resetSpeed;
                }
            }
        }

        //Jiggle
        if (jiggling) {
            var neutralRotation = floors[(int)elevatorManager.GetComponent<ElevatorMovement>().floorPos];
            if (jiggleTimer < jiggleLength) {
                if (jiggleTimer % jiggleDivisor == 0) {
                    leverRotation = neutralRotation + jiggleStrength * Mathf.Sin(jiggleTimer);
                }
                jiggleTimer++;
            }
            else {
                jiggling = false;
                jiggleTimer = 0;
                leverRotation = neutralRotation;
            }
        }

        //Impliment lever rotation changes
        transform.rotation = Quaternion.Euler(0, 91.95f, leverRotation);
    }

    public void jiggleResponse() {
        jiggleSound.PlaySound(transform.position);
        jiggling = true;
    }

    public void resetLever() {
        leverRotation = minRotation;
        //Impliment lever rotation changes
        transform.rotation = Quaternion.Euler(0, 91.95f, leverRotation);
    }
}
