using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

    public bool doorOpen;

    public string LobbyTag;
    public string Floor1Tag;
    public string Floor2Tag;
    public string Floor3Tag;
    public string Floor4Tag;
    public string Floor5Tag;

    private string[] floors;
    private int activeFloorIndex;

	// Use this for initialization
	void Start () {
		floors[0] = LobbyTag;
        floors[1] = Floor1Tag;
        floors[2] = Floor2Tag;
        floors[3] = Floor3Tag;
        floors[4] = Floor4Tag;
        floors[5] = Floor5Tag;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called from elevator movement when new floor is reached
    public void loadNewFloor(int targetFloor) {
        //Turn off the previously active floor
        GameObject.FindGameObjectWithTag(floors[activeFloorIndex]).SetActive(false);
        //Turn on the next floor
        GameObject.FindGameObjectWithTag(floors[targetFloor]).SetActive(true);

        activeFloorIndex = targetFloor;
    }
}
