using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IAgentState {

    private StatePatternAgent agent;

    //constructor
    public MoveState(StatePatternAgent spa)
    {
        agent = spa;
    }

    public void UpdateState()
    {
        toDialogueState();
    }

    public void toThinkState()
    {
        agent.currentState = agent.thinkState;
    }

    public void toMoveState()
    {
        //should never be called
        Debug.Log("Cannot change to same state");
    }

    public void toDialogueState()
    {
        agent.currentState = agent.dialogueState;
    }
}
