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
        for (int i = 0; i < jsondata.changeList.Count; i++)
        {
            changeDict.Add(jsondata.changeList[i].name, jsondata.changeList[i]);
        }

        //get util
        gl = GameObject.FindWithTag("Player").GetComponent<GestureList>();
        bubble = GetComponentInChildren<Text>();
    }

    // Use this for initialization
    void Start () {
        currentState = thinkState;
        atNode = nodeDict["Start"];
        timer = atNode.wait;
        say(nodeDict["Start"]);
        timerFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
        timer -= Time.deltaTime;
	}

    public void say(node n)
    {
        bubble.text = n.dialogue;
    }
}
