using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour {

    //for debugging purposes, we will add classes here
    public BossActions actions;
    public BossEvents events;


    // Use this for initialization
    void Start () {
        events.action = actions;
	}
	
	// Update is called once per frame
	void Update () {
        if (events.events[events.step]()) events.step++;

    }
}
