using UnityEngine;
using System.Collections;

public class ClockAnimator : MonoBehaviour {

    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f;

    public Transform hours, minutes;

    void Update() {
        float time = Time.timeSinceLevelLoad;
        float minute = time % 60f;
        float hour = time / 60f;

        hours.localRotation =
            Quaternion.Euler(0f, 0f, hour * -hoursToDegrees + 4* hoursToDegrees);
        minutes.localRotation =
            Quaternion.Euler(0f, 0f, minute * -minutesToDegrees);
    }
}
