using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueState : IAgentState {
    
    private StatePatternAgent agent;

    //constructor
    public DialogueState(StatePatternAgent spa)
    {
        agent = spa;
    }

    public void UpdateState()
    {
        if (agent.nextNode == null)
        {
            agent.isDone = true;
            toThinkState();
            return;
        }

        Debug.Log(agent.atNode.name + "update to" + agent.nextNode.name);

        agent.atNode = agent.nextNode;

        agent.nextNode = null;

        agent.say(agent.atNode);

        agent.timerFlag = true;

        agent.gl.resetGesture();

        toThinkState();
    }

    public void toThinkState()
    {
        agent.currentState = agent.thinkState;
    }

    public void toMoveState()
    {
        agent.currentState = agent.moveState;
    }

    public void toDialogueState()
    {
        //should never be called
        Debug.Log("Cannot change to same state");
    }
}
