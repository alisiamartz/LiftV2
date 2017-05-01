using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patronWaiting : MonoBehaviour {

    public GameObject FloorBLight;
    public GameObject FloorLLight;
    public GameObject Floor2Light;
    public GameObject Floor3Light;
    public GameObject Floor4Light;
    public GameObject Floor5Light;
    public GameObject Floor6Light;

    private GameObject[] FloorLights;

    // Use this for initialization
    void Start () {
        //Fill the Array with the parents for the lights
        FloorLights[0] = FloorBLight; FloorLights[1] = FloorLLight; FloorLights[2] = Floor2Light; FloorLights[3] = Floor3Light; FloorLights[4] = Floor4Light; FloorLights[5] = Floor5Light; FloorLights[6] = Floor6Light;
    }

    public void lightOn(int floor) {
        FloorLights[floor].transform.Find("lightOn").gameObject.SetActive(true);
        FloorLights[floor].transform.Find("lightOff").gameObject.SetActive(false);
    }

    public void lightOff(int floor) {
        FloorLights[floor].transform.Find("lightOn").gameObject.SetActive(false);
        FloorLights[floor].transform.Find("lightOff").gameObject.SetActive(true);
    }
}
