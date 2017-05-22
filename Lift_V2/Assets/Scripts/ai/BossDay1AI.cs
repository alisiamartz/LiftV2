using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDay1AI : Agent {

	// Use this for initialization
	void Start () {
        filename = "1.1Boss.json";
        Init();
        isEndNode = false;
        isExit = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0) timer -= Time.deltaTime;

        
	}

    private bool isPlayed = false;
    private delegate bool bossEvent();
    private List<bossEvent> list = new List<bossEvent>();
    private bool playedFirst = false;
    int listIndex = 0;

    private bool enterElevator() {
        if (enter()) return true;

        if (playedFirst) {
            say("Still waiting");
        } else {
            say("Start");
            playedFirst = true;
        } return false;
    }

    private bool firstOrder() {
        if (!isDoorOpen()) return true;

        if (playedFirst) {
            if (isNearLever() && isDoorOpen()) {
                say("Ancient mechanism");
            }
        } else {
            say("First order");
            playedFirst = true;
        } return false;
    }

    private bool readyExit() {
        if (isDoorOpen()) {
            if (getFloorNumber() != attributes.goal) {
                say("notFloor");
            } else return true;
        } else say("Extra");
        return false;
    }

    private bool say(string name) {
        //do not play if dialog already played -- return false
        if (isPlayed) return false;

        //get correct dialoge to play (depending on state)
        node n = nodeDict[name];

        //get mood index
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos

        //get sound file
        string dialogue = n.dialogue[index];

        //play sound
        playDialogue(dialogue);

        //sets up the timer for the next node
        float audioTime = GetComponent<PatronAudio>().patronMouth.clip.length;
        timer = audioTime + n.wait;

        //talking animation
        animate(n.animation[index], audioTime);

        //text bubble
        bubble.text = n.dialogue[index];

        //update lastSound
        lastSound = dialogue;

        //update isPlayed
        isPlayed = true;

        //return true when successful
        return true;
    }
}
