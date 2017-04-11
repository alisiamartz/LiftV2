using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for speech bubble
using UnityEngine.UI;
//for gestures
using Edwon.VR.Gesture;

public abstract class Agent : MonoBehaviour {

    public string filename;

    //agent data
    protected agentAttr attributes;
    protected Dictionary<string, node> nodeDict = new Dictionary<string, node>();

    //movement

    //node data
    protected node currentNode;
    protected node nextNode;

    //util
    protected GestureList gl;
    protected Text bubble;
    protected float timer;
    protected bool timerFlag;
    protected PatronMovement pm;
    protected bool isStarted;
    protected FloorManager fm;
    protected bool isExit;

    //useful stuff
    protected string lastGesture() { return gl.getGesture(); }
    protected void resetGesture() { gl.resetGesture(); }
    protected bool isDoorOpen() { return fm.doorOpen; }
    protected bool enter() { return pm.enterElevator(); }
    protected bool exit() { return pm.leaveElevator(); }
    protected int getFloorNumber() { return fm.floorPos; }
    
    protected void Init()
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
        Debug.Log(pm);
        fm = GameObject.FindWithTag("HotelManager").GetComponent<FloorManager>();

        //move start to here
        isStarted = false;
        isExit = false;
    }

    //decorative stuff
    protected delegate bool decorative();

    protected bool selector(List<decorative> ld)
    {
        for (int i = 0; i < ld.Count; i++)
        {
            if (ld[i]()) return true;
        }
        return false;
    }

    protected bool sequence(List<decorative> ld)
    {
        for (int i = 0; i < ld.Count; i++)
        {
            if (!ld[i]()) return false;
        }
        return true;
    }

    //
    protected void say(node n)
    {
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos
        bubble.text = n.dialogue[index];
    }
}
