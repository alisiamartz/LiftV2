using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabHaptic : MonoBehaviour {

    public void RumbleController(float duration, float strength, string whichHand) {
        if (whichHand != "left" && whichHand != "right" && whichHand != "both") {
            Debug.LogError("RumbleController requires a third argument of 'left', 'right' or 'both'. Set to both instead");
            StartCoroutine(RumbleControllerRoutine(duration, strength, "both"));
        }
        else {
            StartCoroutine(RumbleControllerRoutine(duration, strength, whichHand));
        }
    }

    IEnumerator RumbleControllerRoutine(float duration, float strength, string whichHand) {
        strength = Mathf.Clamp01(strength);
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime <= duration) {
            int valveStrength = Mathf.RoundToInt(Mathf.Lerp(0, 3999, strength));
            if(whichHand == "left" || whichHand == "both") {
                var deviceIndex = GameObject.FindGameObjectWithTag("Player").GetComponent<SteamVR_ControllerManager>().leftControlIndex;
                SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort)strength);
            }
            if(whichHand == "right" || whichHand == "both") {
                var deviceIndex = GameObject.FindGameObjectWithTag("Player").GetComponent<SteamVR_ControllerManager>().rightControlIndex;
                SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort)strength);
            }

            yield return null;
        }
    }
}
