using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//gestures
using Edwon.VR.Gesture;
//ui
using UnityEngine.UI;


public class AgentController : MonoBehaviour {

    public string filename; //with .json

    private JsonParser jp = new JsonParser();
    private CreateCharacters cc = new CreateCharacters();
    private GestureList gl;
    private Text bubble;
    private Agent agent;

    private int step = 0;

    private jsonClass agentClass;

    // Use this for initialization
    void Start () {
        agentClass = jp.parse(filename);
        agent = cc.create(agentClass);
        gl = GameObject.FindWithTag("Player").GetComponent<GestureList>();
        agent.setGestureList(ref gl);
        bubble = GetComponentInChildren<Text>();
        agent.setBubble(ref bubble);
	}
	
	// Update is called once per frame
	void Update () {
        if (agent.timeline[step]())
        {
            agent.flag = true;
            step++;
        }
    }
}
