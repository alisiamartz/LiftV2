using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSpawner : MonoBehaviour {

    public GameObject businessMan;
    public GameObject boss;
    public GameObject floorPanel;

    private int currentPatron = 0;
    private GameObject patronPrefab;

    public float spawnDelay;                         //The amoount of time inbetween each patron spawn

    private int spawnTimer;

    // Use this for initialization
    void Start()
    {
        nextPatron();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Spawn a patron on a floor and set their target
    public void spawnPatron(int startFloor, int targetFloor)
    {
        var manager = GetComponent<FloorManager>();

        //TO DO adjust rotation of patrons
        Quaternion basePatronRotation = patronPrefab.transform.rotation;

        var newPatron = Instantiate(patronPrefab, GetComponent<FloorManager>().fetchFloorWaypoint(startFloor).transform.position, basePatronRotation);
        newPatron.transform.parent = manager.floors[startFloor].transform;

        floorPanel.GetComponent<FloorsPanel>().lightOn(startFloor);
        GetComponent<FloorManager>().patrons[startFloor] += 1;
    }

    public void nextPatron()
    {
        if(currentPatron == 0)
        {
            patronPrefab = businessMan;
            spawnPatron(0, 5);
            currentPatron++;
        }
        else if(currentPatron == 1)
        {
            patronPrefab = boss;
            spawnPatron(5, 1);
            currentPatron++;
        }
    }
}
