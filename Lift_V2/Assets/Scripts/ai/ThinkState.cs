using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        //do nothing if at exit node
        if (agent.atNode.wait < 1)
        {
            agent.move = "exit";
            toMoveState();
            return;
        }

        //check if floor is correct
        if (agent.fm.doorOpen == true)
        {
            //not floor
            if (agent.fm.floorPos != agent.attributes.goal)
            {
                agent.say(agent.nodeDict["notFloor"]);
                return;
            } else
            {
                agent.nextNode = agent.nodeDict["End"];
                toMoveState();
                return;
            }
        } else
        {
            agent.say(agent.atNode);
        }

        agent.timer -= Time.deltaTime;

        //determine next node and update mood
        if (agent.atNode.listen.Contains(agent.gl.getGesture()))
        {
            int index = agent.atNode.listen.IndexOf(agent.gl.getGesture());
            agent.nextNode = agent.nodeDict[agent.atNode.toNode[index]];
            updateMood(agent.atNode.change[index]);
            toMoveState();
        } else if (agent.timer < 0)
        {
            agent.nextNode = agent.nodeDict[agent.atNode.noResponse];
            updateMood(agent.atNode.noResponseChange);
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

    void updateMood(short c)
    {
        agent.attributes.mood += c;
        //validators
        if (agent.attributes.mood > 10) agent.attributes.mood = 10;
        else if (agent.attributes.mood < -10) agent.attributes.mood = -10;
    }
}
