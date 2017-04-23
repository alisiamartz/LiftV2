using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueSource : MonoBehaviour
{
    public string current = null;
    public string previous = null;

	void Update () {
        current = StatePatternAgent.dialogueString;
        //Debug.Log(current);
        if (current != previous)
        {
            GameObject myObject = GameObject.Find("_SFX_" + previous);
            if (myObject != null)
                myObject.GetComponent<SoundGroup>().pingSound();
            previous = current;
            previous.PlaySound(transform.position);
        }

    }
}

