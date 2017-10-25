using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraManager : MonoBehaviour {

    public GameObject[] ExtraScenarios;
    private List<GameObject> extraScenarios;
    private GameObject targetScenario;
    public GameObject extraParent;

    // Use this for initialization
    void Start () {
        //At the start of the game create lists for every floor from the array
        extraScenarios = new List<GameObject>();
        for (var i = 0; i < ExtraScenarios.Length; i++)
        {
            extraScenarios.Add(ExtraScenarios[i]);
        }
	}

    //Called when a new floor is loaded to spawn extras
    public void spawnExtras(stateList.floor target)
    {
        //Find all scenarios of our floor for Group A, and are correct for the time of day
        var Item = extraScenarios.FindAll(c => (c.GetComponent<scenarioHolder>().floorLocation == stateList.floor.lobby) && (c.GetComponent<scenarioHolder>().floorGrouping == stateList.group.GroupA));
        //If we have multiple matching options
        if (Item.Count > 1)
        {
            //Choose a random scenario
            targetScenario = Item[Random.Range(0, Item.Count)];
        }
        else
        {
            //Spawn a generic scenario
            targetScenario = extraScenarios[0];
        }

        //Remove the Scenario we want to spawn from the List so that it doesn't repeat
        var a = Instantiate(targetScenario, targetScenario.GetComponent<scenarioHolder>().scenarioLocation, Quaternion.identity);
        a.transform.parent = extraParent.transform;
    }

    //Called usually before spawnExtras & removes all current extras
    public void removeExtras()
    {
        foreach (Transform child in extraParent.transform)
        {
            Destroy(child.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            removeExtras();
            spawnExtras(stateList.floor.lobby);
        }
	}
}
