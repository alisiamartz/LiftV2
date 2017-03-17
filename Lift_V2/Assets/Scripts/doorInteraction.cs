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

    private GameObject door;
    private GameObject hold;
    private GameObject doorOpen;
    private GameObject rope;

    bool lifting;
    bool closing;
    public bool open = false;

    private bool grabbingUp = false;
    private bool grabbingDown = false;

    float currY;
    float initX;
    float initY;
    float initZ;

    Vector3 frame1;
    Vector3 frame2;
    Vector3 frame3;

    private GameObject grabPointL;
    private GameObject grabPointR;
    public slidingDoor2 slidingDoor2;

    public string doorLoopSFX;
    bool playing = false;

    [SerializeField]
    SteamVR_TrackedObject trackedObj;
    [SerializeField]
    SteamVR_TrackedObject trackedObj2;

    [HideInInspector]
    public bool handInRange = false;
    private bool grabbed = false;
    private GameObject grabbingHand;

    private GameObject manager;

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
        door = GameObject.FindGameObjectWithTag("door");
        hold = GameObject.FindGameObjectWithTag("doorHold");
        doorOpen = GameObject.FindGameObjectWithTag("openDoor");
        rope = GameObject.FindGameObjectWithTag("rope");

        initX = door.transform.localPosition.x;
        initY = door.transform.localPosition.y;
        initZ = door.transform.localPosition.z;

        manager = GameObject.FindGameObjectWithTag("ElevatorManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabPointL)
        {
            grabPointL = GameObject.FindGameObjectWithTag("grabPointL");
        }
        if (!grabPointR)
        {
            grabPointR = GameObject.FindGameObjectWithTag("grabPointR");
        }

        if(handInRange == false)
        {
            //grabbed = false;
        }


        if (grabbed)
        {
            Debug.Log("hell yeah get ready to lift");
            lifting = true;

            if (grabbingUp == false)
            {
                //Trigger Haptic pulse
                manager.GetComponent<grabHaptic>().triggerBurst(5, 2);
                grabbingUp = true;
            }

            // Move the door according to the current y position of the controller
            door.transform.position = new Vector3(initX, (door.transform.position.y - hold.transform.position.y) + grabbingHand.transform.position.y, initZ);
            // Once the door reaches a certain height
            // it goes all the way up automatically

        }
     
        else
        {
            grabbingUp = false;
            // if door not at a certain point yet
            // it goes down
            if (door.transform.position.y < 2.5f && lifting)
            {
              // while the door position is greater than the y position
                door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(initX, 1.1f, initZ), 2f * Time.deltaTime);
            }

        }

        if (lifting)
        {
            // if past certain point
            if (door.transform.position.y > 2.5f)
            {
                door.transform.position = Vector3.MoveTowards(door.transform.position, doorOpen.transform.position, Time.deltaTime);
                Debug.Log("Attempt to lerP");
                if (door.transform.position.y >= 3.2f)
                {
                    Debug.Log("I WANT THIS");
                    lifting = false;
                    open = true;
                    slidingDoor2.openSlidingDoor();

                    //Tell the elevator manager that door is open
                    manager.GetComponent<ElevatorMovement>().doorOpened();
                }
            }
        }

        if (open)
        {
            // if door is at the top (open) 
            // if trigger is pressed on rope and it is collided
            if (grabbed)
            {
                Debug.Log("hell yeah get ready to close this shit");
                closing = true;
                // Move the door according to the current y position of the controller
                door.transform.position = new Vector3(initX, (door.transform.position.y - rope.transform.position.y) + grabbingHand.transform.position.y, initZ);

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
                //Debug.Log(door.transform.localPosition.y);//flooding console
                if (door.transform.localPosition.y <= 1.17f)  
                {
                    Debug.Log("Door is closed");
                    closing = false;
                    open = false;
                    slidingDoor2.closeSlidingDoor();
                    manager.GetComponent<ElevatorMovement>().doorClosed();
                }
            }
        }

        if (door.transform.position.y > 1.2f && door.transform.position.y < 3.15)
        {
            if (!playing)
            {
                //start sound
                doorLoopSFX.PlaySound(transform.position);
                playing = true;
            }
        }
        else
        {
            if (playing)
            {
                //stop sound
                GameObject myObject = GameObject.Find("_SFX_doorLoopSFX");
                myObject.GetComponent<SoundGroup>().pingSound();
                playing = false;
            }
        }


    } // end of update

    public void attemptGrab(GameObject hand)
    {
        if (handInRange)
        {
            grabbed = true;
            grabbingHand = hand;
        }
    }

    public void attemptRelease()
    {
        grabbed = false;
    }

    //used in LeverGrab script to stop the lever from moving if door is open
    public bool doorStatus()
    {
        return open;
    }

}   // eof

