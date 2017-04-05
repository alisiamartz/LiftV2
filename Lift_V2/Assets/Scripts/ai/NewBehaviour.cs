using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for speech bubble
using UnityEngine.UI;
//for gestures
using Edwon.VR.Gesture;

public class NewBehaviour : MonoBehaviour {

    public string filename;

    //agent data
    private agentAttr attributes;
    private Dictionary<string, node> nodeDict = new Dictionary<string, node>();

    //movement
    private delegate bool movement();
    private Dictionary<string, movement> movementDict = new Dictionary<string, movement>();
    private string move; //?

    //node data
    private node currentNode;
    private node nextNode;

    //util
    private GestureList gl;
    private Text bubble;
    private float timer;
    private bool timerFlag;
    private PatronMovement pm;
    private bool isStart;
    private bool isDone;
    private FloorManager fm;

    //useful stuff
    private string lastGesture() { return gl.getGesture(); }
    private void resetGesture() { gl.resetGesture(); }
    private bool isDoorOpen() { return fm.doorOpen; }
    private void enter() { pm.enterElevator(); }
    private void exit() { pm.enterElevator(); }
    
    private void Awake()
    {
        //get json data
        JsonParser jp = new JsonParser();
        jsonClass jsondata = jp.parse(filename);
        attributes = jsondata.agentAttr;
        for (int i = 0; i < jsondata.nodes.Count; i++)
        {
            nodeDict.Add(jsondata.nodes[i].name, jsondata.nodes[i]);
        }

        //get util
        gl = GameObject.FindWithTag("Player").GetComponent<GestureList>();
        bubble = GetComponentInChildren<Text>();
        pm = GetComponent<PatronMovement>();
        fm = GameObject.FindWithTag("HotelManager").GetComponent<FloorManager>();

    }

    void Start () {
		
	}

	void Update () {
		
	}

    //classes?
    class selector
    {

    }

    //main functions
    public void think()
    {
        //
    }

}
