using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacters{

    public Agent create(jsonClass j)
    {
        Agent agent = new Agent();

        events[] eventList = j.eventList;
        actions[] actionList = j.actionList;

        //dictionary of actions
        Dictionary<string, change> actionDict = new Dictionary<string, change>();
        for (int i = 0; i < actionList.Length; i++)
        {
            var t = actionList[i];
            actionDict.Add(t.name, t.change);
        }
        agent.actionDict = actionDict;

        //build timeline
        for (int i = 0; i < eventList.Length; i++)
        {
            var t_event = eventList[i];
            //not used == or just for init
            switch (t_event.type)
            {
                case "init":
                    init(t_event, agent);
                    break;
                case "action":
                    agent.timeline.Add(() => agent.action(t_event));
                    break;
                case "listen":
                    agent.timeline.Add(() => agent.listen(t_event));
                    break;
                case "utility":
                    agent.timeline.Add(() => agent.utility(t_event));
                    break;
                default:
                    Debug.Log("not a type at event list index: " + i);
                    break;
            }
        }
        agent.timeline.Add(() => agent.end());

        return agent;
    }

    private void init(events e, Agent a)
    {
        //manually input utility -- cannot create poitners or refernces :(
        a.happiness = e.utility[0];
        a.sadness = e.utility[1];
        a.confusion = e.utility[2];
        a.anger = e.utility[3];
    }
}
