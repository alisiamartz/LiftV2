using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patronWaiting : MonoBehaviour {

    public GameObject[] FloorLights;

    public void lightOn(int floor) {
        FloorLights[floor].transform.Find("lightOn").gameObject.SetActive(true);
        FloorLights[floor].transform.Find("lightOff").gameObject.SetActive(false);
        FloorLights[floor].transform.Find("lightGoal").gameObject.SetActive(false);
    }

    public void lightOff(int floor) {
        FloorLights[floor].transform.Find("lightOn").gameObject.SetActive(false);
        FloorLights[floor].transform.Find("lightOff").gameObject.SetActive(true);
        FloorLights[floor].transform.Find("lightGoal").gameObject.SetActive(false);
    }

    public void lightGoal(int floor) {
        FloorLights[floor].transform.Find("lightGoal").gameObject.SetActive(true);
        FloorLights[floor].transform.Find("lightOn").gameObject.SetActive(false);
        FloorLights[floor].transform.Find("lightOff").gameObject.SetActive(false);
    }
}
