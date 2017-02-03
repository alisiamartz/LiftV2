using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attached to the door
 * 
 * When the players controller comes in contact with the holds,
 * 	If the door is closed
 * 		the player raises it when it has trigger pressed and raises arm
 * 	If the door is open
 * 		the player closes door when it has trigger pressed and lowers arm
 * 
 */ 

public class doorInteraction : MonoBehaviour {

	public GameObject door;
	public GameObject hold;
	public GameObject doorOpen;
    public GameObject rope;

	public static bool holdCollide;
    public static bool ropeCollide;
	bool lifting;
	bool closing;
    bool open;

	float currY;
	float initX;
	float initY;
	float initZ;

	Vector3 frame1;
	Vector3 frame2;
	Vector3 frame3;

	public GameObject grabPoint;


	[SerializeField]
	SteamVR_TrackedObject trackedObj;
	[SerializeField]
	SteamVR_TrackedObject trackedObj2;


	private SteamVR_Controller.Device device { 
		get { 
			return SteamVR_Controller.Input((int)trackedObj.index); 
		} 
	}

	private SteamVR_Controller.Device device2{ 
		get { 
			return SteamVR_Controller.Input((int)trackedObj2.index); 
		} 
	}
		

	// Use this for initialization
	void Start () {
        open = false;

		holdCollide = false; //setting false cause its static
        ropeCollide = false;

		if (!door)
			door = GameObject.FindGameObjectWithTag ("door");
	
		hold = GameObject.FindGameObjectWithTag ("doorHold");

		doorOpen = GameObject.FindGameObjectWithTag ("openDoor");

        rope = GameObject.FindGameObjectWithTag("rope");

		initX = door.transform.position.x;
		initY = door.transform.position.y;
		initZ = door.transform.position.z;

		if (!grabPoint)
			grabPoint = GameObject.FindGameObjectWithTag ("grabPoint");
	}
	
	// Update is called once per frame
	void Update () {

        // all actions are tracked by whether the door is open or not 
        //switch (open)
        //{
        //    case true:
        //        break;
        //    case false:
        //        break;
        //}


        if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && (holdCollide || Vector3.Distance(hold.transform.position, grabPoint.transform.position) < (hold.transform.localScale.x * 6f)))
        {
            //if (Vector3.Distance (hold.transform.position, grabPoint.transform.position) < hold.transform.localScale.x * 2f) {
            Debug.Log("hell yeah get ready to lift");
            lifting = true;

            // Move the door according to the current y position of the controller
            door.transform.position = new Vector3(initX, (door.transform.position.y - hold.transform.position.y) + trackedObj.transform.position.y, initZ);
            // Once the door reaches a certain height
            // it goes all the way up automatically

        }
        else if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            // if the door gets unhooked, lerp up just a bit 

        }
        else
        {
            // if door not at a certain point yet
            // it goes down
            if (door.transform.position.y < 2.5f && lifting)
            {
                //if (door.transform.position.y >= initY) { // while the door position is greater than the y position
                door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(initX, initY, initZ), 2f * Time.deltaTime);
                //}
            }

        }

        if (lifting)
        {
            // if past certain point
            if (door.transform.position.y > 2.5f)
            {
                door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpen.transform.position, Time.deltaTime);
                Debug.Log("Attempt to lerP");
                if (door.transform.localPosition.y >= 5.8f)
                {
                    Debug.Log("I WANT THIS");
                    lifting = false;
                    open = true;
                }
            }
        }

        if (open)
        {
            // if door is at the top (open) 
            // if trigger is pressed on rope and it is collided
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && (ropeCollide || Vector3.Distance(rope.transform.position, grabPoint.transform.position) < (rope.transform.localScale.x *4f)))
            {
                Debug.Log("hell yeah get ready to close this shit");
                closing = true;
                // Move the door according to the current y position of the controller
                door.transform.position = new Vector3(initX, (door.transform.position.y - rope.transform.position.y) + trackedObj.transform.position.y, initZ);
            } // condition about where the door goes when the user stops interacting w it in some way?
            else
            {
                if (door.transform.position.y > 2.5f && closing)
                {
                    door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpen.transform.position, 2f * Time.deltaTime);
                }
            }
        }

        if (closing)
        {
            if (door.transform.position.y < 2.5f)
            {
                door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(initX,initY,initZ), Time.deltaTime);
                Debug.Log("Attempt to lerP down to close");
                if (door.transform.localPosition.y <=0f)
                {
                    Debug.Log("Door is closed");
                    closing = false;
                    open = false;
                }
            }
        }



    } // end of update



    /*
     * If the player is holding the door hold (trigger down on door) 
     *  Sets lifting to true
     *  and moves the door according to the y of the controller
     * 
     * If the player lets go of the trigger
     *  TODO
     *  
     * Else if player is not holding the door and not in the scale of door hold
     *  If the door y is less than 3f and they are still lifting
     *      closing = true (because it is falling down)
     *      door position moves down to init
     */

    //switch (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
    //{
    //    case true: // When the trigger is pressed down

    //        if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) { 

    //            if (holdCollide || Vector3.Distance(hold.transform.position, grabPoint.transform.position) < (hold.transform.localScale.x * 6f))
    //            {
    //                // if the user is holding the doorHold 
    //                // lifting is true (since the player is holding onto it and moving the transform position)
    //                // Move the door according to the current y position of the controller
    //                Debug.Log("hell yeah get ready to lift");
    //                lifting = true;
    //                door.transform.position = new Vector3(0, (door.transform.position.y - hold.transform.position.y) + trackedObj.transform.position.y, 0);
    //            }

    //            // while lifting and then the 
    //            if (lifting)
    //            {
    //                // if past certain point
    //                if (door.transform.position.y >= 3f)
    //                {
    //                    door.transform.position = Vector3.Lerp(door.transform.position, doorOpen.transform.position, Time.deltaTime);
    //                    Debug.Log("Lerping up to top of door");
    //                    if (door.transform.position.y >= 5.8f) {
    //                        //  lifting = false;
    //                        //   frozen = true;
    //                        Debug.Log("door frozen at top");
    //                    }
    //                }
    //            }

    //            if (ropeCollide || Vector3.Distance(rope.transform.position, grabPoint.transform.position) < (rope.transform.localScale.x * 6f))
    //            {  // if the user is holding the door rope
    //            }
    //        }
    //        break;

    //    case false: // When the trigger is not pressed

    //        if (door.transform.position.y <= 3f && lifting)
    //        {
    //            // if door y is less than 3 and the player is still technically in lift mode 
    //            door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(0, 0, 0), 2f * Time.deltaTime);
    //            Debug.Log("Going down and lerp");
    //        }

    //        // while lifting and then the 
    //        if (lifting)
    //        {
    //            // if past certain point
    //            if (door.transform.position.y >= 3f)
    //            {
    //                door.transform.position = Vector3.Lerp(door.transform.position, doorOpen.transform.position, Time.deltaTime);
    //                Debug.Log("Lerping up to top of door");
    //                if (door.transform.position.y >= 5.8f)
    //                {
    //                    //  lifting = false;
    //                    //   frozen = true;
    //                    Debug.Log("door frozen at top");
    //                }
    //            }
    //        }

    //        break;
    //}

    //if (frozen && !lifting)
    //{
    //    Debug.Log("Frozen up top hopefully?");
    //    // Trigger is pressed on the rope object
    //    if (!lifting && device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && (ropeCollide || Vector3.Distance(rope.transform.position, grabPoint.transform.position) < (rope.transform.localScale.x * 6f)))
    //    {
    //        Debug.Log("hell yeah get ready to close");
    //        closing = true;

    //        // Move the door according to the current y position of the controller
    //        door.transform.position = new Vector3(doorOpen.transform.position.x, (door.transform.position.y - rope.transform.position.y) + trackedObj.transform.position.y, doorOpen.transform.position.z);
    //    }
    //    else if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
    //    {
    //        // if the door gets unhooked, lerp up just a bit 
    //    } /// here 
    //}

    //if (closing)
    //{
    //    // if past certain point
    //    if (door.transform.position.y > 3f)
    //    {
    //        door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(initX, initY, initZ), Time.deltaTime);
    //        Debug.Log("Lerping to bottom");
    //        if (door.transform.position.y <= initY)
    //        {
    //            closing = false;
    //            //frozen = true;
    //            Debug.Log("is the door closed?");
    //        }
    //    }
    //}

    //   if (device.GetPress (Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && (holdCollide || Vector3.Distance (hold.transform.position, grabPoint.transform.position) < (hold.transform.localScale.x * 6f))) {
    //if (Vector3.Distance (hold.transform.position, grabPoint.transform.position) < hold.transform.localScale.x * 2f) {
    //	Debug.Log ("hell yeah get ready to lift");
    //	lifting = true;
    // Move the door according to the current y position of the controller
    //	door.transform.position = new Vector3 (initX, (door.transform.position.y - hold.transform.position.y) + trackedObj.transform.position.y, initZ);
    //} else if (device.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger)) {
    // if the door gets unhooked, lerp up just a bit 
    //} else {
    // if door not at a certain point yet
    // it goes down
    //	if (door.transform.position.y < 3f && lifting) {
    //          //closing = true;
    //		door.transform.position = Vector3.MoveTowards (door.transform.position, new Vector3 (initX, initY, initZ), 2f * Time.deltaTime);
    //	}
    //}

    /*
     * If the player is holding the door hold (trigger down on door) 
     *  Sets lifting to true
     *  lifting 
     *  if the door y is greater than 3f (aka player has lifted up to that spot) 
     *      door y lerps up to open door 
     *  else if door position is greater than or equal to open door y
     *      frozen = true (door no longer moving) 
     */



    /*
     * If the player is holding the door hold (trigger down on door) 
     *  Sets lifting to true
     *  and moves the door according to the y of the controller
     * 
     * If the player lets go of the trigger
     *  TODO
     *  
     * Else if player is not holding the door and not in the scale of door hold
     *  If the door y is less than 3f and they are still lifting
     *      closing = true (because it is falling down)
     *      door position moves down to init
     */
    // if not lifting
    // which means that it is either up or down

}   // eof

