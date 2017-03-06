using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Edwon.VR.Gesture;

public class StatePatternAgent : MonoBehaviour {

    public string filename;
    public jsonClass jsondata;

    public IAgentState currentState;
    public ThinkState thinkState;
    public MoveState moveState;
    public DialogueState dialogueState;

    public GestureList gl;
    public int beat;

    private void Awake()
    {
        //set up fsm
        thinkState = new ThinkState(this);
        moveState = new MoveState(this);
        dialogueState = new DialogueState(this);

        //get json data
        JsonParser jp = new JsonParser();
        jsondata = jp.parse(filename);

        //get gesturelist
        gl = GameObject.FindWithTag("Player").GetComponent<GestureList>();

        beat = 0;
    }

    // Use this for initialization
    void Start () {
        currentState = thinkState;
	}
	
	// Update is called once per frame
	void Update () {
        currentState.UpdateState();
	}
}
