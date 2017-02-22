using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementSound : MonoBehaviour {

    public float timeToEngine = 5.0f;
    private bool revvingUp = false;
    private float timer = 0.0f;

    public string elevatorStartSound;
    public string elevatorEngineSound;

	// Use this for initialization
	void Start () {
        elevatorStartSound.PlaySound();
        revvingUp = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(revvingUp == true) {
            if(timer < timeToEngine) {
                timer += 0.1f;
            }
            else {
                elevatorEngineSound.PlaySound();
                revvingUp = false;
            }
        }
	}
}
