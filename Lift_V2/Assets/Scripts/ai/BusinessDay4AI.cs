using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusinessDay4AI : Agent {

	// Use this for initialization
	void Start () {
        filename = "4.2Businessman.json";
        Init();

        //stuff
        patTimer = attributes.patience;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //declarations
    private bool isPlayed = false;

    private void doState() {
        if (!isEndNode) say();

        node n = currentNode;

        if (timer > n.wait) {
            resetGesture();
            stopGestures();
            return;
        }

        startGesture();

        string gesture = getGesture();

        switch (currentNode.name) {
            case "Start": case "Home Value": case "Hard workers":
                normal(n, gesture);
                break;
            case "Bad impressions": case "Missed opportunity": case "Paradox": case "Point taken":
            case "Perfect for the business": case "Last chance": case "Room for one more": case "Questionable decision":
                leaveElevator();
                break;
            case "No Regrets": case "One Sided":
                waitForFloor("Missed opportunity", "Bad impressions");
                break;
            case "Parasites":
                waitForFloor("Paradox", "Point taken");
                break;
            case "Cutthroat":
                waitForFloor("Perfect for the business", "Last chance");
                break;
            case "Diamond in the rough":
                waitForFloor("Room for one more", "Questionable decision");
                break;
            default:
                break;
        }
    }

    private void normal(node n, string gesture) {
        if (n.listen.Contains(gesture)) {
            int index = n.listen.IndexOf(gesture);
            changeMood(n.change[index]);
            currentNode = nodeDict[n.toNode[index]];
            isPlayed = false;
        }
        else if (timer <= 0) {
            changeMood(n.noResponseChange);
            currentNode = nodeDict[n.noResponse];
            isPlayed = false;
        }
    }

    private void waitForFloor(string right, string wrong) {
        //true if door it open else false;
        if (isDoorOpen()) {
            if (getFloorNumber() == attributes.goal) currentNode = nodeDict[right];
            else currentNode = nodeDict[wrong];
            isPlayed = false;
        }
        else if (timer > 0) isPlayed = false;
    }

    private void leaveElevator() {
        if (!isExit) {
            exit();
            (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).setMood(name, attributes.mood);
            isExit = true;
        }
    }

    private node getNode() {
        //returns correct state node (onNode without tracking)
        switch (state) {
            case 0:
                return currentNode;
            case 1:
                return notFloorNode;
            case 2:
                return endNode;
            default:
                Debug.LogError("state out of range: " + state);
                return null;
        }
    }

    private void say() {

        //do not play if dialog already played
        if (isPlayed) return;

        //get correct dialoge to play (depending on state)
        node n = getNode();

        //get modd index
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos

        //talking animation
        animate(n.animation[index]);

        //text bubble
        bubble.text = n.dialogue[index];

        //sound file
        string dialogue = n.dialogue[index];

        //stop sounds - should only be called on the start to start1 nodes or not at all
        GameObject soundObject = GameObject.Find("_SFX_" + lastSound);
        if (soundObject != null) soundObject.GetComponent<SoundGroup>().pingSound();

        //play sound
        playDialogue(dialogue);

        //add delay?
        float i = 1;
        float total = 0;
        while (total < i) total += Time.deltaTime;

        //sets up the timer for the next node
        float audioTime = GetComponent<PatronAudio>().patronMouth.clip.length;
        timer = audioTime + n.wait; ;

        //update lastSound
        lastSound = dialogue;

        //update isPlayed
        isPlayed = true;
    }
}
