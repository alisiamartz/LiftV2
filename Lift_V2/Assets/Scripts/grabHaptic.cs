using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabHaptic : MonoBehaviour {

    //This script is a public function used to trigger varying strengths of haptic feedback 'bursts'
    //when an object is first grabbed

    private bool pulsing = false;
    private int timerMax = 12;
    private int pulseTimer = 0;
    private ushort pulseMag = 100;
    private int handsAffected = 2;


    public void Start() {
        //Find which device is left and which is right
        var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
        Debug.Log(SteamVR_Controller.Input(deviceIndex));
    }


    void RumbleController(float duration, float strength) {
        StartCoroutine(RumbleControllerRoutine(duration, strength));
    }

    IEnumerator RumbleControllerRoutine(float duration, float strength) {
        strength = Mathf.Clamp01(strength);
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime <= duration) {
            int valveStrength = Mathf.RoundToInt(Mathf.Lerp(0, 3999, strength));

            var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
            SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse((ushort)valveStrength);

            yield return null;
        }
    }

    //Strength is the magnitude of the haptic burst (ranging from 1-10)
    //Hands are the hands affected: 0 (left hand only), 1 (right hand only), 2 (both hands)

	public void triggerBurst(int strength, int hands)
    {
        pulsing = true;
        pulseMag = (ushort)(100 * strength);
        handsAffected = hands;
    }

    private void Update()
    {
        if (pulsing)
        {
            if (pulseTimer < timerMax)
            {
                if (handsAffected == 0 || handsAffected == 2)
                {
                    var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost); 
                    SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(pulseMag);
                }
                if (handsAffected == 1 || handsAffected == 2)
                {
                    var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
                    SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(pulseMag);
                }
                pulseTimer++;
            }
            else
            {
                pulsing = false;
                pulseTimer = 0;
            }
        }
    }
}
