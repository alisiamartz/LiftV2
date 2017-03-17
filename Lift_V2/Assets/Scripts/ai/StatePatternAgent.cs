using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Edwon.VR.Gesture;

public class StatePatternAgent : MonoBehaviour {

    public string filename;

    //agent data
    public agentAttr attributes;
    public Dictionary<string, node> nodeDict = new Dictionary<string, node>();
    public Dictionary<string, change> changeDict = new Dictionary<string, change>();

    //
    public delegate void movement();
    public Dictionary<string, movement> movementDict;
    public string move;

    //tree data
    public node atNode;
    public node nextNode;

    public IAgentState currentState;
    public ThinkState thinkState;
    public MoveState moveState;
    public DialogueState dialogueState;

    //util
    public GestureList gl;
    public Text bubble;
    public float timer;
    public bool timerFlag;
    public PatronMovement pm;

    private void Awake()
    {
        //set up fsm
        thinkState = new ThinkState(this);
        moveState = new MoveState(this);
        dialogueState = new DialogueState(this);

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

        //test
        movementDict = new Dictionary<string, movement>();
        movementDict.Add("enter", () => pm.enterElevator());
        movementDict.Add("exit", () => pm.leaveElevator());
    }

    // Use this for initialization
    void Start () {
        currentState = thinkState;
        atNode = nodeDict["Start"];
        timer = atNode.wait;
        say(nodeDict["Start"]);
        timerFlag = false;

        //test
        move = null;
        movementDict["enter"]();
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
        timer -= Time.deltaTime;
	}

    public void say(node n)
    {
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos
        bubble.text = n.dialogue[index];
    }
}
