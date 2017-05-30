using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkAnimationList : MonoBehaviour {

    public List<talkAnimation> adultress1 = new List<talkAnimation>();
    public List<talkAnimation> adultress2 = new List<talkAnimation>();

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
        adultress1.Add(new talkAnimation(4f, "Date")); adultress1.Add(new talkAnimation(3.5f, "Adultress")); adultress1.Add(new talkAnimation(3.5f, "Adultress")); adultress1.Add(new talkAnimation(3.5f, "Adultress")); adultress1.Add(new talkAnimation(3.5f, "Date"));
        adultress2.Add(new talkAnimation(4f, "Date"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<talkAnimation> fetchAnimationList(string audioLine) {
        if(audioLine == "adt1.2h") {
            return adultress1;
        }
        else {
            return adultress2;
        }
    }
}
