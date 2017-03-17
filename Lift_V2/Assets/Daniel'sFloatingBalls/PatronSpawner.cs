using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatronSpawner : MonoBehaviour
{

    public GameObject patronPrefab;
    public GameObject floorPanel;

    public float spawnDelay;                         //The amoount of time inbetween each patron spawn

    private int spawnTimer;

    // Use this for initialization
    void Start()
    {
        if (spawnDelay != 0)
        {
            InvokeRepeating("thinkPatron", 5.0f, spawnDelay);
        }
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

    public void thinkPatron()
    {
        var manager = GetComponent<FloorManager>();
        var currentPatrons = manager.patrons;
        var spawnFloor = Random.Range(0, currentPatrons.Length);
        if (currentPatrons[spawnFloor] != 1)
        {
            var targetFloor = 0;
            //Set target floor as a random flooor y steps away
            if (spawnFloor < currentPatrons.Length / 2)
            {
                targetFloor = Random.Range(currentPatrons.Length / 2, currentPatrons.Length);
            }
            else
            {
                targetFloor = Random.Range(0, currentPatrons.Length / 2);
            }
            spawnPatron(spawnFloor, targetFloor);
        }
        else
        {
            thinkPatron();
        }
    }
}
