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

public class doorInteraction : MonoBehaviour
{

    public GameObject door;
    public GameObject hold;
    public GameObject doorOpen;
    public GameObject rope;

    public static bool holdCollide;
    public static bool ropeCollide;
    bool lifting;
    bool closing;
    public bool open;
    public string doorSFX;

    private bool grabbingUp = false;
    private bool grabbingDown = false;

    float currY;
    float initX;
    float initY;
    float initZ;

    Vector3 frame1;
    Vector3 frame2;
    Vector3 frame3;

    public GameObject grabPoint;
    public slidingDoor2 slidingDoor2;

    [SerializeField]
    SteamVR_TrackedObject trackedObj;
    [SerializeField]
    SteamVR_TrackedObject trackedObj2;

    public GameObject manager;

    private SteamVR_Controller.Device device
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private SteamVR_Controller.Device device2
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj2.index);
        }
    }


    // Use this for initialization
    void Start()
    {
        open = false;

        holdCollide = false; //setting false cause its static
        ropeCollide = false;

        if (!door)
            door = GameObject.FindGameObjectWithTag("door");

        hold = GameObject.FindGameObjectWithTag("doorHold");

        doorOpen = GameObject.FindGameObjectWithTag("openDoor");

        rope = GameObject.FindGameObjectWithTag("rope");

        initX = door.transform.position.x;
        initY = door.transform.position.y;
        initZ = door.transform.position.z;

        if (!grabPoint)
            grabPoint = GameObject.FindGameObjectWithTag("grabPoint");
    }

    // Update is called once per frame
    void Update()
    {

        // all actions are tracked by whether the door is open or not 
        //switch (open)
        //{
        //    case true:
        //        break;
        //    case false:
        //        break;
        //}


        if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && (holdCollide || Vector3.Distance(hold.transform.position, grabPoint.transform.position) < (hold.transform.localScale.x * 10f)))
        {
            //if (Vector3.Distance (hold.transform.position, grabPoint.transform.position) < hold.transform.localScale.x * 2f) {
            Debug.Log("hell yeah get ready to lift");
            lifting = true;

            if (grabbingUp == false)
            {
                //Trigger Haptic pulse
                manager.GetComponent<grabHaptic>().triggerBurst(5, 2);
                grabbingUp = true;
            }

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
            grabbingUp = false;
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
                    slidingDoor2.openSlidingDoor();
                    // TODO: MAKE NOISE PLAY WHEN ANIMATED DOOR IS OPEN
                    doorSFX.PlaySound(transform.position);

                    //Tell the elevator manager that door is open
                    manager.GetComponent<ElevatorMovement>().doorOpened();
                }
            }
        }

        if (open)
        {
            // if door is at the top (open) 
            // if trigger is pressed on rope and it is collided
            if (device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger) && (ropeCollide || Vector3.Distance(rope.transform.position, grabPoint.transform.position) < (rope.transform.localScale.x * 10f)))
            {
                Debug.Log("hell yeah get ready to close this shit");
                closing = true;
                // Move the door according to the current y position of the controller
                door.transform.position = new Vector3(initX, (door.transform.position.y - rope.transform.position.y) + trackedObj.transform.position.y, initZ);

                if (grabbingDown == false)
                {
                    //Trigger Haptic pulse
                    manager.GetComponent<grabHaptic>().triggerBurst(5, 2);
                    grabbingDown = true;
                }

            } // condition about where the door goes when the user stops interacting w it in some way?
            else
            {
                if (door.transform.position.y > 2.5f && closing)
                {
                    door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpen.transform.position, 2f * Time.deltaTime);
                }

                grabbingDown = false;
            }
        }

        if (closing)
        {
            if (door.transform.position.y < 2.5f)
            {   
                door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(initX, initY, initZ), Time.deltaTime);
                Debug.Log("Attempt to lerP down to close");
                if (door.transform.localPosition.y <= 0.1f)  
                {
                    Debug.Log("Door is closed");
                    closing = false;
                    open = false;
                    slidingDoor2.closeSlidingDoor();
                    manager.GetComponent<ElevatorMovement>().doorClosed();
                }
            }
        }

    } // end of update

    //used in LeverGrab script to stop the lever from moving if door is open
    public bool doorStatus()
    {
        return open;
    }

}   // eof

