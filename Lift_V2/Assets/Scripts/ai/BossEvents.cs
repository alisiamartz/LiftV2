using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvents : MonoBehaviour {

    //something something accessability
    public delegate bool Event();
    
    public List<Event> events = new List<Event>();

    //for debugging -- should just create its own
    public BossActions action;
    //BossActions action = new BossActions();

    float wait = 0;

    public int step = 0;

	// Use this for initialization
	void Start () {
        events.Add(Hello);
        events.Add(Introduction0);
        events.Add(Introduction1);
        events.Add(End);
	}
	
	// Update is called once per frame
	void Update () {
        //moved to agent controller
        //if (events[step]()) step++;
	}

    //////////
    //Legacy//
    //////////

    public bool Hello()
    {
        action.Hello();
        wait = 5; //sets up wait for next action.. will change set up location later 
        return true;
    }

    public bool Introduction0()
    {
        wait -= Time.deltaTime;
        if (wait <= 0)
        {
            action.Introduction0();
            return true;
        } else if (Input.GetKeyDown(KeyCode.Y))
        {
            action.Introduction0(0);
            return true;
        } else if (Input.GetKeyDown(KeyCode.N))
        {
            action.Introduction0(1);
            return true;
        } else
        {
            //do nothing or add wait time here
            return false;
        }
    }

    public bool Introduction1()
    {
        action.Introduction1();
        return true;
    }

    public bool End()
    {
        //do nothing
        return false;
    }
}
