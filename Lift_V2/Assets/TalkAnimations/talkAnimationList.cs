using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkAnimationList : MonoBehaviour {

    public List<talkAnimation> adultress1 = new List<talkAnimation>();
    public List<talkAnimation> adultress2 = new List<talkAnimation>();
    public List<talkAnimation> adultress3 = new List<talkAnimation>();
    public List<talkAnimation> adultress4 = new List<talkAnimation>();
    public List<talkAnimation> adultress5 = new List<talkAnimation>();
    public List<talkAnimation> adultress6 = new List<talkAnimation>();
    public List<talkAnimation> adultress7 = new List<talkAnimation>();
    public List<talkAnimation> adultress8 = new List<talkAnimation>();
    public List<talkAnimation> adultress9 = new List<talkAnimation>();
    public List<talkAnimation> adultress10 = new List<talkAnimation>();
    public List<talkAnimation> adultress11 = new List<talkAnimation>();
    public List<talkAnimation> adultress12 = new List<talkAnimation>();

    public class talkAnimation {
        public float timeTilNext;
        public string adultress;

        public talkAnimation(float gap, string adult) {
            timeTilNext = gap;
            adultress = adult;
        }

    }

	// Use this for initialization
	void Start () {
        adultress1.Add(new talkAnimation(3.5f, "Date")); adultress1.Add(new talkAnimation(3.5f, "Adultress")); adultress1.Add(new talkAnimation(3.5f, "Adultress")); adultress1.Add(new talkAnimation(4f, "Adultress")); adultress1.Add(new talkAnimation(3.5f, "Date"));
        adultress2.Add(new talkAnimation(2.3f, "Date")); adultress2.Add(new talkAnimation(2.3f, "Adultress")); adultress2.Add(new talkAnimation(3f, "Adultress")); adultress2.Add(new talkAnimation(4f, "Date")); adultress2.Add(new talkAnimation(3f, "Adultress")); adultress2.Add(new talkAnimation(4f, "Adultress"));
        adultress3.Add(new talkAnimation(3.5f, "Date")); adultress3.Add(new talkAnimation(3.5f, "Adultress")); adultress3.Add(new talkAnimation(4f, "Adultress")); 
        adultress4.Add(new talkAnimation(5f, "Adultress")); adultress4.Add(new talkAnimation(2.5f, "Date")); adultress4.Add(new talkAnimation(2.5f, "Date")); adultress4.Add(new talkAnimation(2.5f, "Adultress")); adultress4.Add(new talkAnimation(2.5f, "Date"));
        adultress5.Add(new talkAnimation(4f, "Adultress")); adultress5.Add(new talkAnimation(4f, "Date")); adultress5.Add(new talkAnimation(3f, "Adultress")); adultress5.Add(new talkAnimation(3f, "Date")); adultress5.Add(new talkAnimation(3f, "Adultress"));
        adultress6.Add(new talkAnimation(4f, "Date")); adultress6.Add(new talkAnimation(3f, "Date")); adultress6.Add(new talkAnimation(3f, "Adultress")); adultress6.Add(new talkAnimation(4f, "Date")); adultress6.Add(new talkAnimation(3f, "Adultress")); adultress6.Add(new talkAnimation(4f, "Date"));
        adultress7.Add(new talkAnimation(3f, "Date")); adultress7.Add(new talkAnimation(3f, "Date")); adultress7.Add(new talkAnimation(3f, "Adultress")); adultress7.Add(new talkAnimation(3f, "Adultress")); adultress7.Add(new talkAnimation(3f, "Date")); adultress7.Add(new talkAnimation(3f, "Adultress"));
        adultress8.Add(new talkAnimation(3f, "Adultress")); adultress8.Add(new talkAnimation(4f, "Date")); adultress8.Add(new talkAnimation(2f, "Adultress")); adultress8.Add(new talkAnimation(2f, "Date")); adultress8.Add(new talkAnimation(3f, "Adultress")); adultress8.Add(new talkAnimation(2.5f, "Adultress")); adultress8.Add(new talkAnimation(2.5f, "Date")); adultress8.Add(new talkAnimation(2.5f, "Adultress"));
        adultress9.Add(new talkAnimation(3f, "Adultress")); adultress9.Add(new talkAnimation(3.5f, "Adultress")); adultress9.Add(new talkAnimation(3f, "Adultress")); adultress9.Add(new talkAnimation(2f, "Date")); adultress9.Add(new talkAnimation(3.5f, "Adultress")); adultress9.Add(new talkAnimation(3.5f, "Adultress"));
        adultress10.Add(new talkAnimation(3.5f, "Adultress")); adultress10.Add(new talkAnimation(3f, "Date"));
        adultress11.Add(new talkAnimation(3f, "Date")); adultress11.Add(new talkAnimation(3f, "Adultress")); adultress11.Add(new talkAnimation(4f, "Date")); adultress11.Add(new talkAnimation(4f, "Adultress"));
        adultress12.Add(new talkAnimation(4f, "Adultress"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<talkAnimation> fetchAnimationList(string audioLine) {
        if(audioLine == "adt1.2h") {
            return adultress1;
        }
        else if(audioLine == "adt2.1h") {
            return adultress2;
        }
        else if (audioLine == "adt2.2h" || audioLine == "adt3.5h") {
            return adultress3;
        }
        else if (audioLine == "adt2.3h") {
            return adultress4;
        }
        else if (audioLine == "adt3.1h") {
            return adultress5;
        }
        else if (audioLine == "adt3.2h") {
            return adultress6;
        }
        else if (audioLine == "adt3.3h") {
            return adultress7;
        }
        else if (audioLine == "adt3.4h") {
            return adultress8;
        }
        else if (audioLine == "adt4.2a") {
            return adultress9;
        }
        else if (audioLine == "adt4.2h") {
            return adultress10;
        }
        else if (audioLine == "adt4.2n") {
            return adultress11;
        }
        else {
            return adultress12;
        }
    }
}
