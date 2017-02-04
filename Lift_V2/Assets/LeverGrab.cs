using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverGrab : MonoBehaviour {

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private GameObject lever;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

        lever = GameObject.FindGameObjectWithTag("lever");
    }
	
	// Update is called once per frame
	void Update () {
        if (controller == null)
        {
            return;
        }

        if (controller.GetPressDown(triggerButton))
        {
            lever.GetComponent<LeverRange>().attemptGrab(this.gameObject);
        }
        else if (controller.GetPressUp(triggerButton))
        {
            lever.GetComponent<LeverRange>().attemptRelease();
        }  
    }
}
