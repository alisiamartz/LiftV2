using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacters : MonoBehaviour {
    //hold step of timeline
    delegate bool step();
    //sequence of events
    List<step> timeline = new List<step>();

    public Agent agent = new Agent();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void create(parser p)
    {
        events[] eventList = p.eventList;
        actions[] actionList = p.actionList;

        //dictionary of actions
        Dictionary<string, change> actionDict = new Dictionary<string, change>();
        for (int i = 0; i < actionList.Length; i++)
        {
            var t = actionList[i];
            actionDict.Add(t.name, t.change);
        }
        agent.actionDict = actionDict;

        //dictionary of posstible things to listen too -- manually inputed for now
        Dictionary<string, KeyCode> listenDict = new Dictionary<string, KeyCode>();
        listenDict.Add("yes", KeyCode.Y);
        listenDict.Add("no", KeyCode.N);
        agent.listenDict = listenDict;

        //build timeline
        for (int i = 0; i < eventList.Length; i++)
        {
            var t_event = eventList[i];
            //not used == or just for init
            switch (t_event.type)
            {
                case "init":
                    init(t_event);
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
    }

    private void init(events e)
    {
        //manually input utility -- cannot create poitners or refernces :(
        agent.happiness = e.utility[0];
        agent.sadness = e.utility[1];
        agent.confusion = e.utility[2];
        agent.anger = e.utility[3];
    }
}
