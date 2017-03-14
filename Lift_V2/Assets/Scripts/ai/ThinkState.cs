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

        if (agent.atNode.listen.Count < 1)
        {
            return;
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
        //update mood
        updateMood();
        agent.currentState = agent.moveState;
    }

    public void toDialogueState()
    {
        agent.currentState = agent.dialogueState;
    }

    void updateUtil(change c)
    {
        agent.attributes.utility[0] += c.happiness;
        agent.attributes.utility[1] += c.sadness;
        agent.attributes.utility[2] += c.confusion;
        agent.attributes.utility[3] += c.anger;
    }

    void updateMood()
    {
        int[] util = agent.attributes.utility;
        int max_value = util.Max();
        int max_index = util.ToList().IndexOf(max_value);

        int curr_index = -1;
        switch (agent.mood)
        {
            case "happy":
                curr_index = 0;
                break;
            case "sad":
                curr_index = 1;
                break;
            case "confused":
                curr_index = 2;
                break;
            case "angry":
                curr_index = 3;
                break;
            default:
                break;
        }

        bool is_threshold = max_value > 7;
        bool is_same = curr_index == max_index; 

        if (!is_threshold)
        {
            agent.mood = null;
            return;
        } else if (is_same)
        {
            return;
        }
        else
        {
            switch (max_index)
            {
                case 0:
                    agent.mood = "happy";
                    break;
                case 1:
                    agent.mood = "sad";
                    break;
                case 2:
                    agent.mood = "confused";
                    break;
                case 3:
                    agent.mood = "angry";
                    break;
                default:
                    Debug.Log("Update Mood went wrong");
                    break;
            }
            return;
        }
    }
}
