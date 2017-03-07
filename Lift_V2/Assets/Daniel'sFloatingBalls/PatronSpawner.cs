using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronSpawner : MonoBehaviour {

    public GameObject patronPrefab;

    public int spawnDelay;                         //The amoount of time inbetween each patron spawn

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Spawn a patron on a floor and set their target
    public void spawnPatron(GameObject startFloor, Vector3 startLoc, int targetFloor)
    {
        var newPatron = Instantiate(patronPrefab, startLoc, Quaternion.identity);
        newPatron.transform.parent = startFloor.transform;
        newPatron.GetComponent<PatronManager>().destinationFloor = targetFloor;
    }
}
