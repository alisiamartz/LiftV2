using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	// TODO:
	// - turn off gesture component
	// - set them down in correct place depending on object

	// Placed on objects that are interactable
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	 
	[SerializeField]
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

	public static bool inRange;
	bool holding;

	public ItemSlot slot;
	public enum ItemSlot {
		HAT,
		ID,
		GLASSES
	}

	public GameObject hatHolder;
	public GameObject glassesHolder;
	public GameObject idHolder;

	public GameObject camRig;

	private bool nearObj;


	// Use this for initialization
	void Start () {
		//trackedObj = GetComponent<SteamVR_TrackedObject>();

		hatHolder = GameObject.FindGameObjectWithTag("hatHolder");
		glassesHolder = GameObject.FindGameObjectWithTag("glassesHolder");
		idHolder = GameObject.FindGameObjectWithTag("idHolder");

		camRig = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
		// pcik up when trigger is pressed

	//	if (device.GetPressDown (triggerButton)) {
	//		this.attemptGrab ();
	//	} else if (device.GetPressUp(triggerButton)) {
			
	//	}  












		if (device.GetPressDown (triggerButton) && (inRange || Vector3.Distance (trackedObj.transform.position, this.transform.position) < .1f)) {
			// parent object to controller
			DisableGesture.turnOff (camRig);
			this.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			this.transform.SetParent (trackedObj.transform);
			holding = true;
		} else if (!inRange || !(Vector3.Distance (trackedObj.transform.position, this.transform.position) < .1f)) {
			if (!DisableGesture.isComponentEnabled (camRig)) {
				print ("yeah????");
				DisableGesture.turnOn (camRig);
			}
		}


		// if you are holding it and the trigger is released
		// check where the object is
		// if near end point, then go there

		if (holding && device.GetPressUp(triggerButton)) { // || near the end location??)

			// unless it is near the intended location,
			// the object will fall on the ground 
			switch (slot) {
			case ItemSlot.ID:
			// allow an object to go on players torso
				if (Vector3.Distance (this.transform.position, idHolder.transform.position) < .3f) {
					this.transform.SetParent (idHolder.transform);
				} else {
					dropObj (this.gameObject);
				}
				break;

			case ItemSlot.HAT:
			// allow an object to go on players head
				//parent to camera
				if (Vector3.Distance(this.transform.position, hatHolder.transform.position)<.3f) {
					this.transform.SetParent(hatHolder.transform);
				}  else {
					dropObj (this.gameObject);
				}
				break;

			case ItemSlot.GLASSES:
			// these allow it to be stuck to the persons face
				break;
			default:
				break;
			}
		}
	}

	public void attemptGrab() {
		if (inRange) {
			nearObj = true;
			DisableGesture.turnOff (camRig);
			holding = true;

		} else {
			nearObj = false;
			if (!DisableGesture.isComponentEnabled (camRig)) {
				DisableGesture.turnOn (camRig);
			}
		}
	}

	public void attemptRelease() {
		holding = false;
	}

	private void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "grabPoint"){
			inRange = true;
		}
	}

	private void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "grabPoint") {
			inRange = false;
		}
	}

	void dropObj(GameObject item) {
		// de parent from controller
		this.transform.parent = null;
		// turn on gravity
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	}
}