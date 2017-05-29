using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristDay1AI : Agent {
    /*
     * Modified GenericAIv2 for first Tourist Interactions
     * 
     * Features:
     * - Will not leave elevator until interaction ends
     * - Different dialogue for leavintg elevator
     */

    // Use this for initialization
    void Start() {
        filename = "1.2Tourist.json";
        Init();
        isEndNode = false;
        isExit = false;
        timer = currentNode.wait;
    }

    // Update is called once per frame
    void Update() {

        //timer -  will not start untill walkedIn is true
        if (timer > 0 && walkedIn) timer -= Time.deltaTime;

        //waiting to walk in
        if (!walkedIn) {
            if (enter()) walkedIn = true;
            else return;
        }

        //start
        if (!doneStart) {
            if (playStart()) doneStart = true;
            else return;
        }

        //setup normal
        if (!doneSetup) {
            if (setupNormal()) doneSetup = true;
            else return;
        }

        //update state
        if (isUpdate) updateState();

        //play normal
        doState();
    }

    //declarations
    private bool walkedIn = false;
    private bool doneStart = false;
    private bool doneSetup = false;
    private bool isUpdate = false;
    private bool isPlayed = false;
    private int state = 0; //0 = story 1 = ready to exit 2 = exit (custom)

    private bool playStart() {
        //once entered will check if door is closed, else it'll repeat "take me to x"
        if (!isDoorOpen()) return true;

        say();

        if (timer <= 0) {
            isPlayed = false;
            //not needed?
            changeMood(currentNode.noResponseChange);
        }

        return false;
    }

    private bool setupNormal() {
        //used to make sure ai finishes start dialogue
        if (timer < currentNode.wait) {
            //set up normal
            currentNode = nodeDict[currentNode.noResponse];
            isPlayed = false;
            return true;
        }

        return false;
    }

    private void updateState() {
        if (!isExit) isPlayed = false;
        switch (state) {
            case 0:
                if (isEndNode) state = 1;
                break;
            case 1:
                if (isDoorOpen()) {
                    if (getFloorNumber() == attributes.goal) {
                        currentNode = nodeDict["Souvenir"];
                    }
                    else {
                        currentNode = nodeDict["Detour"];
                    } state = 2;
                }
                break;
            case 2:
                break;
            default:
                Debug.LogError("state out of range: " + state);
                break;
        }
        isUpdate = false;
    }

    private void doState() {
        if(state != 1) say();

        //listen only when dialogue is done
        if (timer > currentNode.wait) {
            resetGesture(); //for hand stuff
            stopGestures();
            return;
        }

        if (currentNode.listen.Count > 0) startGesture(); //for hand stuff

        //get gesture
        string gesture = getGesture();

        //timer in listening range
        switch (state) {
            case 0:
                if (currentNode.name == "Work night" || currentNode.name == "Awkward encounter") {
                    isEndNode = true;
                    isUpdate = true;
                }
                else if (currentNode.listen.Contains(gesture)) {
                    int index = currentNode.listen.IndexOf(gesture);
                    changeMood(currentNode.change[index]);
                    currentNode = nodeDict[currentNode.toNode[index]];
                    isUpdate = true;
                }
                else if (timer <= 0) {
                    changeMood(currentNode.noResponseChange);
                    currentNode = nodeDict[currentNode.noResponse];
                    isUpdate = true;
                }
                break;
            case 1:
                isUpdate = true;
                break;
            case 2:
                if (!isExit) {
                    exit();
                    (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).setMood(name, attributes.mood);
                    isExit = true;
                }
                break;
            default:
                Debug.LogError("state out of range: " + state);
                break;
        }
    }

    private void say() {
        //do not play if dialog already played
        if (isPlayed) return;

        //get mood index
        int index = 1; //neu
        if (attributes.mood < -3) index = 2; //neg
        else if (attributes.mood > 3) index = 0; //pos

        //get sound file
        string dialogue = currentNode.dialogue[index];

        //play sound
        playDialogue(dialogue);

        //sets up the timer for the next node
        float audioTime = GetComponent<PatronAudio>().patronMouth.clip.length;
        timer = audioTime + currentNode.wait;

        //talking animation
        animate(currentNode.animation[index], audioTime);

        //text bubble
        bubble.text = currentNode.dialogue[index];

        //update lastSound
        lastSound = dialogue;

        //update isPlayed
        isPlayed = true;
    }
}
