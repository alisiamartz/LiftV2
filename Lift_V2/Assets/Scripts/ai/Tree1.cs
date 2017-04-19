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
        root = new List<decorative>(new decorative[] { () => sequence(checkStart), () => sequence(checkDoorOpen), () => sequence(checkGesture), () => sequence(checkTimer) });
        checkStart = new List<decorative>(new decorative[] { () => isStart(), () => startAction() });
        checkDoorOpen = new List<decorative>(new decorative[] { () => isDoorOpen(), () => selector(checkFloor) });
        checkFloor = new List<decorative>(new decorative[] { () => sequence(checkRightFloor), () => wrongFloor() });
        checkRightFloor = new List<decorative>(new decorative[] { () => isRightFloor(), () => exitElevator() });
        checkGesture = new List<decorative>(new decorative[] { () => isRightGesture(), () => updateAgent() });
        checkTimer = new List<decorative>(new decorative[] { () => isTimerUp(), () => updateAgent(true) });
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
        if (isDoorOpen())
        {
            enter();
            isStarted = true;
            return true;
        }

        return true;
    }
    
    private bool startAction()
    {
        if (isStarted)
        {
            currentNode = nodeDict["Start"];
            timer = currentNode.wait;
            say(currentNode);
        }

        return true;
    }

    private bool isRightFloor()
    {
        if (attributes.goal == getFloorNumber()) return true;


        return false;
    }

    private bool exitElevator()
    {
        if (!isExit)
        {
            say(endNode);
            exit();
            isExit = true;
        }

        return true;
    }

    private bool wrongFloor()
    {
        patience -= Time.deltaTime;
        if (patience < 0)
        {
            changeMood(notFloorNode.noResponseChange);
            patience = notFloorNode.wait;
        }
        say(notFloorNode);

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
            changeMood(currentNode.noResponseChange);
            currentNode = nodeDict[currentNode.noResponse];
        } else
        {
            int index = currentNode.listen.IndexOf(getGesture());
            changeMood(currentNode.change[index]);
            currentNode = nodeDict[currentNode.toNode[index]];
        }

        resetGesture();
        timer = currentNode.wait;

        return true;
    }

    private bool isTimerUp()
    {
        if (timer < 0) return true;
        timer -= Time.deltaTime;
        say(currentNode);

        return false;
    }

}
