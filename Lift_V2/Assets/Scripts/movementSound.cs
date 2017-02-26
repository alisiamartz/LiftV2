using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementSound : MonoBehaviour {

    public float timeToEngine = 5.0f;
    private bool revvingUp = false;
    private bool elevatorStopped = true;
    private float timer = 0.0f;

    public string elevatorStartSound;
    public string elevatorEngineSound;

    // Update is called once per frame
    void Update () {
        if(GetComponent<ElevatorMovement>().liftSpeedCurrent != 0 && elevatorStopped == true) {
            elevatorStartSound.PlaySound();
            revvingUp = true;
            elevatorStopped = false;
        }
        else if(GetComponent<ElevatorMovement>().liftSpeedCurrent == 0 && elevatorStopped == false) {
            Debug.Log("Wow");
            //Stop playing the elevator engine sound
            GameObject myObject = GameObject.Find("_SFX_ElevatorMovement");
            myObject.GetComponent<SoundGroup>().pingSound();
            elevatorStopped = true;
        }

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
