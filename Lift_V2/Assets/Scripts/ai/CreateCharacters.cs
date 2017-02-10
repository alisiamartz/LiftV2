using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacters : MonoBehaviour {
    //hold step of timeline
    delegate bool step();
    //sequence of events
    List<step> timeline = new List<step>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void create(parser p, string character)
    {
        events[] eventList = p.eventList;
        actions[] actionList = p.actionList;

        Dictionary<string, change> actionDict = new Dictionary<string, change>();
        for (int i = 0; i < actionList.Length; i++)
        {
            var t = actionList[i];
            actionDict.Add(t.name, t.change);
        }

        for (int i = 0; i < eventList.Length; i++)
        {
            switch (eventList[i].type)
            {
                case "init":
                    init t = new init();
                    break;
                default:
                    Debug.Log("Something went wrong, check json eventList type at index " + i);
                    return;
            }
        }
    }
}

public class init
{

}

public class action
{

}
public class listen
{

}
public class utility
{

}