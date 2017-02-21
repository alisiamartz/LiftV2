using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Edwon.VR.Gesture;


public class AgentController : MonoBehaviour {

    public string filename; //with .json

    private JsonParser jp = new JsonParser();
    private CreateCharacters cc = new CreateCharacters();
    private GestureList gl;
    private Agent agent;

    private int step = 0;

    private jsonClass agentClass;

    // Use this for initialization
    void Start () {
        agentClass = jp.parse(filename);
        agent = cc.create(agentClass);
        gl = GetComponent<GestureList>();
        agent.setGestureList(ref gl);
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
