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
    public float handRange = 0.5f;

    public bool grabbed;
    public GameObject grabHand;

    private bool reset;

    public float ascensionRate = 0f;
    public float decensionRate = 0f;

    //5 Unity units between top rotation and bottom rotation. Helper objects in scene

	// Use this for initialization
	void Start () {
        reset = true;
        leverRotation = neutralRotation;

        positionToRotation = Mathf.Abs(maxRotation - minRotation) / 0.5f;
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
        else
        {
            //return to neutral position
            leverRotation = neutralRotation;
            reset = true;
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
        if (leverRotation != neutralRotation)
        {
            if (leverRotation > neutralRotation + neutralRadius)
            {
                ascensionRate = Mathf.Abs((leverRotation - (neutralRotation + neutralRadius)) / maxRotation);
            }
            else
            {
                ascensionRate = 0;
            }
            if (leverRotation < neutralRotation - neutralRadius)
            {
                decensionRate = Mathf.Abs((leverRotation - (neutralRotation + neutralRadius)) / minRotation);
            }
            else
            {
                decensionRate = 0;
            }
        }
        else
        {
            ascensionRate = 0;
            decensionRate = 0;
        }
    }
}
