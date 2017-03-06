using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Edwon.VR.Gesture;

public class ThinkState : IAgentState {

    private StatePatternAgent agent;

    //constructor
    public ThinkState(StatePatternAgent spa)
    {
        agent = spa;
    }

    public void UpdateState()
    {
        if (agent.jsondata.storyBeats[agent.beat].listen == false)
        {
            toMoveState();
        }
        else
        {
            getUrgency();
        }
    }

    public void toThinkState()
    {
        //should never be called
        Debug.Log("Cannot change to same state");
    }

    public void toMoveState()
    {
        agent.currentState = agent.moveState;
    }

    public void toDialogueState()
    {
        agent.currentState = agent.dialogueState;
    }

    public void getUrgency()
    {
        toMoveState();
    }

}
