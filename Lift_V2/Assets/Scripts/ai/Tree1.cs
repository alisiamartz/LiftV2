using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree1 : Agent {

    private Tree1 btree;

    void Start () {
        btree = new Tree1();
        Init();
        build();
	}
	
	// Update is called once per frame
	void Update () {
        selector(root);
	}

    //build tree
    public void build()
    {
        // build leafs first, then work your way up to root
        // ^ ignore that kuz scripting amirite?

        root.Add(() => sequence(checkStart));
        root.Add(() => sequence(checkDoorOpen));
        root.Add(() => sequence(checkGesture));
        root.Add(() => sequence(checkTimer));

        checkStart.Add(() => isStart());
        checkStart.Add(() => startAction());

        checkDoorOpen.Add(() => isDoorOpen());
        checkDoorOpen.Add(() => selector(checkFloor));

        checkFloor.Add(() => sequence(checkRightFloor));
        checkFloor.Add(() => wrongFloor());

        checkRightFloor.Add(() => isRightFloor());
        checkRightFloor.Add(() => exitElevator());

        checkGesture.Add(() => isRightGesture());
        checkGesture.Add(() => updateAgent());

        checkTimer.Add(() => isTimerUp());
        checkTimer.Add(() => updateAgent(true));
    }

    //decoratives in tree
    private List<decorative> root = new List<decorative>();
    private List<decorative> checkStart = new List<decorative>();
    private List<decorative> checkDoorOpen = new List<decorative>();
    private List<decorative> checkFloor = new List<decorative>();
    private List<decorative> checkRightFloor = new List<decorative>();
    private List<decorative> checkGesture = new List<decorative>();
    private List<decorative> checkTimer = new List<decorative>();

    //tree leafs
    private bool isStart()
    {
        //isStarted = has agent entered elevator? if so, stop here
        if (isStarted) return false;
        if (enter())
        {
            isStarted = true;
            return true;
        }

        return true;
    }
    
    private bool startAction()
    {
        currentNode = nodeDict["Start"];
        timer = currentNode.wait;
        say(currentNode);
        timerFlag = false;

        return true;
    }

    private bool isRightFloor()
    {
        Debug.Log("made it here " + attributes.goal + " " + getFloorNumber() + " " + isStarted);
        if (attributes.goal == getFloorNumber()) return true;


        return false;
    }

    private bool exitElevator()
    {
        currentNode = nodeDict["End"];
        if (!isExit)
        {
            exit();
            isExit = true;
        }

        return true;
    }

    ////////////////////////////////
    //HERE IS THE WRONG FLOOR PART//
    ////////////////////////////////

    private bool wrongFloor()
    {
        say(nodeDict["notFloor"]);

        return true;
    }

    private bool isRightGesture()
    {
        if (currentNode.listen.Contains(getGesture())) return true;

        return false;
    }

    private bool updateAgent(bool noResponse = false)
    {
        if (noResponse)
        {
            updateMood(currentNode.noResponseChange);
            currentNode = nodeDict[currentNode.noResponse];
        } else
        {
            int index = currentNode.listen.IndexOf(getGesture());
            updateMood(currentNode.change[index]);
            currentNode = nodeDict[currentNode.toNode[index]];
        }

        say(currentNode);
        timer = currentNode.wait;

        return true;
    }

    private bool isTimerUp()
    {
        if (timer < 0) return true;
        timer -= Time.deltaTime;

        return false;
    }

}
