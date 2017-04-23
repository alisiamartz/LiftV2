using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AI : Agent {

	void Start () {
        filename = "Boss1.json";
        Init();
        timer = nodeDict["Start"].wait;
        currentNode = nodeDict["Start"];
        isExit = false;
        Build();
	}

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;

        if (list[listIndex]()) {
            listIndex += 1;
            currentNode = listNode[listIndex];
            timer = currentNode.wait;
            isPlayed = false;
            playedFirst = false;

            //never be called
            if (listIndex >= list.Count) {
                Debug.Log("ERROR AI DUN GOOFED");
                listIndex = list.Count - 1;
            }
        }

        if (timer <= 0) {
            timer = currentNode.wait;
            isPlayed = false;
        }
    }

    private void Build() {
        list.Add(() => playStart());
        listNode.Add(nodeDict["Start"]);

        list.Add(() => playFirstOrder());
        listNode.Add(nodeDict["First order"]);

        list.Add(() => playNormal());
        listNode.Add(nodeDict["Day to day operations"]);

        list.Add(() => End());
    }

    //
    private delegate bool bossEvent();
    private bool isPlayed = false;
    private List<bossEvent> list = new List<bossEvent>();
    private List<node> listNode = new List<node>();
    private bool isInEle = false;
    private bool playedFirst = false;
    private int listIndex = 0;
    
    private bool playEvent(node n) {
        say(n);
        isPlayed = true;
        return true;
    }

    private bool playStart() {
        if (enter()) return true;

        if (playedFirst) {
            currentNode = nodeDict["Still waiting"];
            say();
            isPlayed = true;
            return false;
        }

        say();
        isPlayed = true;
        playedFirst = true;
        return false;
    }

    private bool playFirstOrder() {
        if (!isDoorOpen()) return true;

        if (isNearLever() && isDoorOpen()) {
            currentNode = nodeDict["Ancient mechanism"];
            say();
            isPlayed = true;
            return false;
        }

        say();
        isPlayed = true;
        playedFirst = true;
        return false;
    }

    private bool playNormal() {
        if (timer <= 0) return true;

        say();
        isPlayed = true;
        return false;
    }

    private bool End() {
        return false;
    }

    private void say() {
        if (isPlayed) return;

        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos
        bubble.text = currentNode.dialogue[index];

        string dialogue = currentNode.dialogue[index];

        GameObject myObject = GameObject.Find("_SFX_" + lastSound);
        if (myObject != null) myObject.GetComponent<SoundGroup>().pingSound();
        dialogue.PlaySound(transform.position);
        lastSound = dialogue;
    }
}
