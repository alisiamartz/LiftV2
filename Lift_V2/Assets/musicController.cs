using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class musicController : MonoBehaviour {

    public AudioClip elevatorMusic;
    public AudioClip mainTheme;
    public AudioClip bossTheme;
    public AudioClip businessTheme;
    public AudioClip touristTheme;
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called from other Scripts when patron enter
    //0 is main theme, 1 is bossTheme, 2 is businessTheme, 3 is touristTheme
    public void playCharacterTheme(int character) {
        var newClip = mainTheme;
        if(character == 0) { newClip = mainTheme; }
        if(character == 1) { newClip = bossTheme; }
        if(character == 2) { newClip = businessTheme; }
        if(character == 3) { newClip = touristTheme; }

        GetComponent<AudioSource>().clip = newClip;
        GetComponent<AudioSource>().Play();

        //StartCoroutine(ExecuteAfterTime(newClip.length));
    }

    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);

        //Set the audio clip back to the elevator music
        GetComponent<AudioSource>().clip = elevatorMusic;
        GetComponent<AudioSource>().Play();
    }

    public void characterExit() {
        GetComponent<AudioSource>().clip = elevatorMusic;
        GetComponent<AudioSource>().Play();
    }
}
