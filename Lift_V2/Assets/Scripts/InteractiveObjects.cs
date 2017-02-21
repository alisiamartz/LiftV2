using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjects : MonoBehaviour {

    //When the player holds either hand controller on the object and holds trigger, an interaction will occur 
    private Rigidbody rb;
    public GameObject item1;
    public GameObject item2;

    [SerializeField]
    SteamVR_TrackedObject trackedObj;
    [SerializeField]
    SteamVR_TrackedObject trackedObj2;


    private SteamVR_Controller.Device device {
        get {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private SteamVR_Controller.Device device2 {
        get {
            return SteamVR_Controller.Input((int)trackedObj2.index);
        }
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

        item1 = GameObject.FindGameObjectWithTag("objA");

        item2 = GameObject.FindGameObjectWithTag("objB");
    }


    // Update is called once per frame
    void Update () {
        //if left/right hand on cube object and trigger is pressed, makes cube disappear
        if ((device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && Vector3.Distance(trackedObj.transform.position, item1.transform.position) < .15) || 
            (device2.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && Vector3.Distance(trackedObj2.transform.position, item1.transform.position) < .15)) {
            item1.SetActive(false);
        }
        //if left/right hand on sphere object and trigger is pressed, makes sphere disappear
        if ((device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && Vector3.Distance(trackedObj.transform.position, item2.transform.position) < .15) || 
            (device2.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && Vector3.Distance(trackedObj2.transform.position, item2.transform.position) < .15)) {
            item2.SetActive(false);
        }
    }
}
