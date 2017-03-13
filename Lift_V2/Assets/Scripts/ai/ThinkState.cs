using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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
        //set wait timer
        if (agent.timerFlag)
        {
            agent.timer = agent.atNode.wait;
            agent.timerFlag = false;
        }

        //
        if (agent.atNode.listen.Contains(agent.gl.getGesture()))
        {
            int index = agent.atNode.listen.IndexOf(agent.gl.getGesture());
            updateUtil(agent.changeDict[agent.atNode.change[index]]);
            agent.nextNode = agent.nodeDict[agent.atNode.toNode[index]];
            toMoveState();
        } else if (agent.timer < 0)
        {
            updateUtil(agent.changeDict[agent.atNode.noResponseChange]);
            agent.nextNode = agent.nodeDict[agent.atNode.noResponse];
            toMoveState();
        } else
        {
            //do nothing
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

    void updateUtil(change c)
    {
        agent.attributes.utility[0] += c.happiness;
        agent.attributes.utility[1] += c.sadness;
        agent.attributes.utility[2] += c.confusion;
        agent.attributes.utility[3] += c.anger;
    }
}
