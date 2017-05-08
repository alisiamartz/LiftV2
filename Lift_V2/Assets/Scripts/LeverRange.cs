using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverRange : MonoBehaviour {

    public static bool inRange;
    private GameObject camRig;
	public static bool nearLever;

    private int handsColliding = 0;

    // Use this for initialization
    void Start () {

        inRange = false;
        camRig = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update () {

        if(inRange == false)
        {
            GetComponent<LeverRotation>().grabbed = false;
        }
	}

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "grabPoint")
        {
            inRange = true;
            handsColliding += 1;
        }
    }
 
    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "grabPoint")
        {
            handsColliding -= 1;
            if (handsColliding <= 0) {
                inRange = false;
            }
        }
    }
 

    public void attemptGrab(GameObject hand, bool doorOpen, float timer) {
        if (inRange)
        {
			nearLever = true;
            DisableGesture.turnOff(camRig);

            if (!doorOpen && timer <= 0.0f) {
                GetComponent<LeverRotation>().grabbed = true;
                GetComponent<LeverRotation>().grabHand = hand;
            } else {
                GetComponent<LeverRotation>().jiggleResponse();
            }

            var whichHand = "both";
            //Trigger Haptic pulse
            if(hand.tag == "leftControl") {
                whichHand = "left";
            }
            else if(hand.tag == "rightControl") {
                whichHand = "right";
            }

            Debug.Log("Called Rumble");
            Haptic.rumbleController(0.75f, 0.5f, whichHand);

        } else {
			nearLever = false;
            if (!DisableGesture.isComponentEnabled(camRig)) {
                DisableGesture.turnOn(camRig);
            }
        }
    }

    public void attemptRelease()  {
        GetComponent<LeverRotation>().grabbed = false;
    }
}
