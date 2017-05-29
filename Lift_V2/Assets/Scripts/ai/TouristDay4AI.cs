using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouristDay4AI : Agent {

    // Use this for initialization
    void Start() {
        filename = "4.3Tourist.json";
        Init();
        patTimer = attributes.patience;
    }

    // Update is called once per frame
    void Update() {
        if (timer > 0 && isEntered) timer -= Time.deltaTime;

        if (!isEntered) {
            if (enter()) {
                isEntered = true;
            }
        }
        else if (!isStart) {
            say(currentNode);
            if (!isDoorOpen()) {
                if (attributes.mood > 3) {
                    currentNode = nodeDict["Delusional"];
                    delusional = true;
                } else currentNode = nodeDict["Facing one's fears"];
                isStart = true;
            }
            else if (timer <= 0) {
                isPlayed = false;
            }
        }
        else if (!isSetup) {
            if (timer < nodeDict["Start"].wait) {
                isPlayed = false;
                isSetup = true;
                timer = 0;
            }
        }
        else doState();
    }

    //declarations
    private bool isEntered = false;
    private bool isStart = false;
    private bool isSetup = false;
    private bool isPlayed = false;
    private bool flag = false;
    private bool delusional = false;
    

    private void doState() {

        node n = currentNode;

        if (timer > n.wait) {
            resetGesture();
            stopGestures();
            return;
        }

        if (n.listen.Count > 0) startGesture();

        string gesture = getGesture();

        switch (currentNode.name) {
            case "Delusional":
            case "Second visit":
            case "Life to the fullest":
                playDelusionalArc();
                break;
            case "waitingFloor":
                if (delusional) playDelusionalEnd();
                break;
            case "Ideal attendant":
                walkingOut();
                break;
            case "Facing one's fears":
                playHusbandStart();
                break;
            case "Getting Divorced":
                playHusbandDivorce();
                break;
            case "What now?":
                break;
            /*
            case "Start1":
            case "Home Value":
            case "Hard workers":
                normal(n, gesture);
                break;
            case "Missed opportunity":
            case "Perfect for the business":
            case "notFloor":
            case "Server":
            case "Artist":
            case "Other":
                leaveElevator();
                break;
            case "No Regrets":
            case "One Sided":
            case "Parasites":
                waitForFloor("Missed opportunity", "notFloor");
                break;
            case "Cutthroat":
                waitForFloor("Perfect for the business", "notFloor");
                break;
            case "Diamond in the rough":
                pickFloor();
                break;
            */
            default:
                break;
        }
    }

    private void playDelusionalArc() {
        say(currentNode);
        if (timer <= 0) {
            currentNode = nodeDict[currentNode.noResponse];
            isPlayed = false;
        }
    }

    private void playDelusionalEnd() {
        if (timer < nodeDict[lastSound].wait) {
            if (isDoorOpen()) {
                if (getFloorNumber() == attributes.goal) {
                    currentNode = nodeDict["Ideal attendant"];
                } else {
                    if (lastSound == notFloorNode.name) {
                        if (timer <= 0) say(notFloorNode, true);
                    } else say(notFloorNode, true);
                }
            } else if (timer <= 0) {
                say(currentNode, true);
            }
        }
    }

    private void playHusbandStart() {
        say(currentNode);
        if (info.getDivorce()) currentNode = nodeDict["What now?"];
        else currentNode = nodeDict["Getting divorced"];
    }

    private void playHusbandDivorce() {
        if (timer <= 0) {
            say(currentNode, true);
            currentNode = nodeDict["What now?"];
        }
    }

    private void playHusbandNormal() {
        if (timer <= nodeDict[lastSound].wait) {
            if (isDoorOpen()) {
                if (getFloorNumber() == attributes.goal) {
                    currentNode = endNode;
                } else {
                    if (lastSound == "notFloor") {
                        if (timer <= 0) say(notFloorNode, true);
                    } else say(notFloorNode, true);
                }
            } else {
                if (lastSound == "notFloor") {
                    say(currentNode, true);
                } else {
                    string gesture = getGesture();
                    if (currentNode.listen.Contains(gesture)) {
                        int index = currentNode.listen.IndexOf(gesture);
                        changeMood(currentNode.change[index]);
                        currentNode = nodeDict[currentNode.toNode[index]];
                    } else if (timer <= 0) {
                        changeMood(currentNode.noResponseChange);
                        currentNode = nodeDict[currentNode.noResponse];
                    }
                }
            }
        } else {
            resetGesture();
        }
    }

    private void walkingOut() {
        say(currentNode, true);
        stopTalking();
        exit();
        currentNode.name = "";
        info.setMood(name, attributes.mood);
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
        else if (timer <= 0) isPlayed = false;
    }

    private void leaveElevator() {
        if (!isExit) {
            stopTalking();
            exit();
            (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).setMood(name, attributes.mood);
            isExit = true;
        }
    }

    private void pickFloor() {
        if (isDoorOpen()) {
            int i = getFloorNumber();
            if (i == 6) currentNode = nodeDict["Server"];
            else if (i == 2) currentNode = nodeDict["Artist"];
            else currentNode = nodeDict["Other"];
            isPlayed = false;
        }
        else if (timer <= 0) isPlayed = false;
    }

    private void say(node n, bool force = false) {
        //do not play if dialog already played -- force will play no matter what
        if (!force && isPlayed) return;

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
    }
}
