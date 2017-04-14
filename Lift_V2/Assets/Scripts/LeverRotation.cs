using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRotation : MonoBehaviour {

    public float leverRotation;
    public float neutralRotation;
    public float neutralRadius;
    public float maxRotation;
    public float minRotation;

    private float positionToRotation;
    private float previousHandPosition;

    public bool grabbed;
    [HideInInspector]
    public GameObject grabHand;

    private bool reset;
    public float resetSpeed = 30;

    [HideInInspector]
    public float ascensionRate = 0f;
    [HideInInspector]
    public float decensionRate = 0f;

    [Header("Floors")]
    public float[] floors;
    public bool setToFloor = false;
    private ElevatorMovement movementScript;

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
        leverRotation = -90;                                                                                      //Rotation of starting floor
        positionToRotation = Mathf.Abs(maxRotation - minRotation) / 0.6f;

        movementScript = GameObject.FindGameObjectWithTag("ElevatorManager").GetComponent<ElevatorMovement>();
    }

    void Update(){
        if (grabbed){
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
            leverRotation = floors[lowestIndex];
            setToFloor = true;
            movementScript.newDoorTarget(lowestIndex);
        }

        transform.rotation = Quaternion.Euler(leverRotation, -0, 0);
    }



    /*
    //5 Unity units between top rotation and bottom rotation. Helper objects in scene

	// Use this for initialization
	void Start () {
        reset = true;
        leverRotation = neutralRotation;

        positionToRotation = Mathf.Abs(maxRotation - minRotation) / 0.5f;
        jiggleDivisor = 10 - jiggleSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (grabbed) {
            if (reset)
            {
                previousHandPosition = grabHand.transform.position.y;
                reset = false;
            }

            if(previousHandPosition != grabHand.transform.position.y) {
                if(previousHandPosition - grabHand.transform.position.y < 0) {
                    //The hand was moving the lever up
                    leverRotation -= Mathf.Abs(previousHandPosition - grabHand.transform.position.y) * positionToRotation;
                }
                else {
                    //The hand was moving the lever down
                    leverRotation += Mathf.Abs(previousHandPosition - grabHand.transform.position.y) * positionToRotation;
                }
                
                previousHandPosition = grabHand.transform.position.y;
            }
        }
        else if(reset == false)
        {
            if (Mathf.Abs(leverRotation - neutralRotation) < 20)
            {
                leverRotation = neutralRotation;
                reset = true;
                leverResetSound.PlaySound(transform.position);
            }
            else
            {
                //return to neutral position
                if (leverRotation > neutralRotation)
                {
                    leverRotation -= resetSpeed;
                }
                if (leverRotation < neutralRotation)
                {
                    leverRotation += resetSpeed;
                }
            }
        }
        //Bounds checks
        if(leverRotation > maxRotation)
        {
            leverRotation = maxRotation;
        }
        else if(leverRotation < minRotation)
        {
            leverRotation = minRotation;
        }


        transform.rotation = Quaternion.Euler(0, 180, leverRotation);

        //Check for elevator control
        if (leverRotation != neutralRotation && jiggling == false)
        {
            if (leverRotation > neutralRotation + neutralRadius)
            {
                decensionRate = Mathf.Abs((leverRotation - (neutralRotation + neutralRadius)) / (maxRotation - (neutralRotation + neutralRadius)));
            }
            else
            {
                decensionRate = 0;
            }
            if (leverRotation < neutralRotation - neutralRadius)
            {
                ascensionRate = Mathf.Abs((leverRotation - (neutralRotation - neutralRadius)) / (minRotation - (neutralRotation - neutralRadius)));
            }
            else
            {
                ascensionRate = 0;
            }
        }
        else
        {
            ascensionRate = 0;
            decensionRate = 0;
        }

        if (jiggling)
        {
            Debug.Log("Jiggling");
            if(jiggleTimer < jiggleLength)
            {
                if (jiggleTimer % jiggleDivisor == 0)
                {
                    leverRotation = neutralRotation + jiggleStrength * Mathf.Sin(jiggleTimer);
                }
                jiggleTimer++;
            }
            else
            {
                jiggling = false;
                jiggleTimer = 0;
                leverRotation = neutralRotation;
            }
        }
    }

    public void jiggleResponse()
    {
        jiggleSound.PlaySound(transform.position);
        jiggling = true;
    }
    */
}
