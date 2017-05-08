using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronAudio : MonoBehaviour {

    public AudioSource patronMouth;
    public string patronName;
    public string dayName;

	// Use this for initialization
	void Start () {
        Debug.LogError(gameObject.name + " does not have a mouth assigned");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playDialogue(string dialogue) {
        var path = "Dialogue/" + patronName + "/" + dayName + "/" + dialogue;
        patronMouth.clip = Resources.Load(path) as AudioClip;
        patronMouth.Play();
    }

    public void pauseDialogue() {
        
    }

    public void resumeDialogue() {

    }
}
