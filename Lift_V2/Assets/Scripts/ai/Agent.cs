using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for speech bubble
using UnityEngine.UI;
//for gestures
using Edwon.VR.Gesture;

public abstract class Agent : MonoBehaviour {

    //example.json
    public string filename;

    //agent data
    protected agentAttr attributes;
    protected Dictionary<string, node> nodeDict = new Dictionary<string, node>();

    //node data
    protected node currentNode;
    protected node notFloorNode;
    protected node endNode;

    //util
    protected GestureList gl;
    protected Text bubble;
    protected float patience;
    protected float timer;
    protected float graceTimer;
    protected PatronMovement pm;
    protected bool isStarted;
    protected FloorManager fm;
    protected bool isExit;
    protected string lastSound;
    protected bool isEndNode;
    protected StateFetch sf;

    //useful stuff
    protected string getGesture() { return gl.getGesture(); }
    protected void resetGesture() { gl.resetGesture(); }
    protected bool isDoorOpen() { return fm.doorOpen; }
    protected bool enter() { return pm.enterElevator(); }
    protected bool exit() { return pm.leaveElevator(); }
    protected int getFloorNumber() { return fm.floorPos; }
    protected bool isNearLever() { return sf.nearLever(); }
    protected bool isNearDoor() { return sf.nearDoor(); }
    
    protected virtual void Init()
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
        Debug.Log("HERERERERE LOOK AT ME: " + bubble);
        pm = GetComponent<PatronMovement>();
        Debug.Log(pm);
        fm = GameObject.FindWithTag("HotelManager").GetComponent<FloorManager>();
        sf = GameObject.FindWithTag("Player").GetComponent<StateFetch>();
    }


    /*
    
    //FOR BASE TREE LOGIC
     
    isStarted = false;
    isExit = false;
    timer = nodeDict["Start"].wait;
    patience = nodeDict["notFloor"].wait;
    notFloorNode = nodeDict["notFloor"];
    endNode = nodeDict["End"];
    graceTimer = 10; //seconds grace time before telling you that you are in the wrong floor
    isEndNode = false;
    */

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

    //agent util
    protected virtual void say(node n)
    {
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos
        bubble.text = n.dialogue[index];

        string dialogue = n.dialogue[index];

        if (isEndNode && !(n == endNode || n== notFloorNode)) return;

        if (n.name == n.noResponse && n != notFloorNode) isEndNode = true;

        if (lastSound != dialogue)
        {
            GameObject myObject = GameObject.Find("_SFX_" + lastSound);
            if (myObject != null) myObject.GetComponent<SoundGroup>().pingSound();
            dialogue.PlaySound(transform.position);
            lastSound = dialogue;
        }
    }

    protected void changeMood(short i)
    {
        attributes.mood += i;

        if (attributes.mood > 10) attributes.mood = 10;
        if (attributes.mood < -10) attributes.mood = -10;
    }

    protected void setMood(short i) {
        attributes.mood = i;
    }
}
