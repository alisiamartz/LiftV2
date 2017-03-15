using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentState {

    void UpdateState();

    void toThinkState();

    void toMoveState();

    void toDialogueState();
}
