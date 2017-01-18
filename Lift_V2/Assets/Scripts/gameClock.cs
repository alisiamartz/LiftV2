using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameClock : MonoBehaviour {

    public Text timeLeft;
    private double timeFunc;
    private double dayLength;
    private double timer;
    private int mins;
    private int sec;
    private string displayMin;
    private string displaySec;
    public Text dayNum;
    private int dayCount;
    private bool isDay;

	// Use this for initialization
	void Start () {
        dayLength = 300.0; // set the desired value of time per day here in seconds
        timer = 0.0;
        dayCount = 1;
        isDay = true;
        dayNum.text = "Day: " + dayCount.ToString();
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (isDay) { 
            timeFunc = dayLength - timer; // makes timeFunc count down from dayLength in realtime
            displayTimeCorrectly();
            timer = Time.timeSinceLevelLoad;
            if (timeFunc <= 0) {
                isDay = false;
                dayCount += 1;
                dayNum.text = dayCount.ToString();
            } 
        }
	}

    // this was created to ensure we don't get time displays like '2:5' when it should be '2:05'
    void displayTimeCorrectly () {
        mins = (int)timeFunc / 60;
        sec = (int)timeFunc % 60;

        if (mins == 0) {
            displayMin = "";
        } else { displayMin = mins.ToString("f0"); }
        
        if (sec < 10) {
            displaySec = "0" + sec.ToString("f0");
        } else { displaySec = sec.ToString("f0"); }

        timeLeft.text = displayMin + " : " + displaySec;
    }
}
