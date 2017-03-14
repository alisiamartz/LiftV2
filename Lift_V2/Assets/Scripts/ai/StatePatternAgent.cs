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
    public string mood = null;
    /* determines what mood agent is in. null is neutral. can be "happy" "sad" "confused" "angry" coropponds to each attribute.
     * attributes will be limited to 100 to 0. think state will handle changing this. attribute must reach at least a threshold of 7 to change it, else it will stay null.
     * will change mood change values to balance this later
     */

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
        say(nodeDict["Start"], null);
        timerFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
        timer -= Time.deltaTime;
	}

    public void say(node n, string mood)
    {
        int index = n.choose.IndexOf(mood);
        if (index < 0) index = n.choose.IndexOf("default");
        bubble.text = n.dialogue[index];
    }
}
