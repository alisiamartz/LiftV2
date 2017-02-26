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
    private GameObject item1A;
    private string thisHand;
    private bool released;
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
        item1A = GameObject.FindGameObjectWithTag("objA");
        item2 = GameObject.FindGameObjectWithTag("objB");
        released = false;
    }

    //will give it throw physics
    void Launch(string hand) {

    }


    // Update is called once per frame
    void Update () {
        //if left/right hand on cube object and trigger is pressed, makes cube disappear
        if ((device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj.transform.position, item1.transform.position) < .2) || 
            (device2.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj2.transform.position, item1.transform.position) < .2)) {
            item1.GetComponent<Rigidbody>().isKinematic = true;
            item1.transform.rotation = Quaternion.identity;
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
            item1.transform.position = trackedObj.transform.position;
            }
            else
            {
            item1.transform.position = trackedObj2.transform.position;
            }
        }
        else {
            item1.GetComponent<Rigidbody>().isKinematic = false;
            item1.transform.rotation = Quaternion.identity;
        }
        //if left/right hand on sphere object and trigger is pressed, makes sphere disappear
        if ((device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj.transform.position, item2.transform.position) < .2) ||
            (device2.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad) && Vector3.Distance(trackedObj2.transform.position, item2.transform.position) < .2))
        {
            item2.GetComponent<Rigidbody>().isKinematic = true;
            item2.transform.rotation = Quaternion.identity;
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                item2.transform.position = trackedObj.transform.position;
                released = true;
                thisHand = "right";
            }
            else
            {
                item2.transform.position = trackedObj2.transform.position;
                released = true;
                thisHand = "left";
            }
        }
        else
        {
            item2.GetComponent<Rigidbody>().isKinematic = false;
            item2.transform.rotation = Quaternion.identity;
            if (released) { Launch(thisHand); }
        }
    }
}
