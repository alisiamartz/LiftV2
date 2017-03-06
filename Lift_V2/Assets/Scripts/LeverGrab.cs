using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverGrab : MonoBehaviour {

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private GameObject lever;

    private GameObject objDoor;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        lever = GameObject.FindGameObjectWithTag("lever");

        objDoor = GameObject.FindGameObjectWithTag("door");
    }
	
	// Update is called once per frame
	void Update () {
        //checks if door is open
        doorInteraction door = objDoor.GetComponent<doorInteraction>();
        if (controller == null)
        {
            return;
        }

        if (controller.GetPressDown(triggerButton))
        {
            if (!door.open)
            {
                lever.GetComponent<LeverRange>().attemptGrab(this.gameObject);
            }
            //If lever cannot move because door is open
            else
            {
                lever.GetComponent<LeverRotation>().jiggleResponse();
            }
        }
        else if (controller.GetPressUp(triggerButton))
        {
            lever.GetComponent<LeverRange>().attemptRelease();
        }  
    }
}
