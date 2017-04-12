using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {

    private Valve.VR.EVRButtonId touchpadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public GameObject lever;

    public GameObject objDoor;

    private float leverTimer;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        lever = GameObject.FindGameObjectWithTag("lever");

        objDoor = GameObject.FindGameObjectWithTag("door");

        leverTimer = 3.0f;
    }
	
	// Update is called once per frame
	void Update () {
        //checks if door is open
        doorInteraction door = objDoor.GetComponent<doorInteraction>();
        if (door.open == true) { leverTimer = 3.0f; }
        else { leverTimer -= Time.deltaTime; }
        if (controller == null)
        {
            return;
        }

        if (controller.GetPressDown(triggerButton))
        {
            lever.GetComponent<LeverRange>().attemptGrab(this.gameObject, door.open, leverTimer);
            objDoor.GetComponent<doorInteraction>().attemptGrab(this.gameObject);
        }
        else if (controller.GetPressUp(triggerButton))
        {
            lever.GetComponent<LeverRange>().attemptRelease();
            objDoor.GetComponent<doorInteraction>().attemptRelease();
        }  
    }
}
