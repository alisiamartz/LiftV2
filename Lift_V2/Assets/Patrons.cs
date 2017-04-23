using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrons : MonoBehaviour {

    public class Patron {
        public GameObject prefab;
        public int startFloor;

        public Patron(GameObject preF, int sFloor) {
            prefab = preF;
            startFloor = sFloor;
        }
    }

    public Patron fetchPatron(string patronName) {

        if(patronName == "Boss1") {
            var startFloor = 1;
            var prefab = (GameObject)Resources.Load("/Patrons/yourPrefab");
            return new Patron(prefab, startFloor);
        }

        Debug.LogError(patronName + " is not a valid patronName. Format with the patron name and the conversation number: 'Boss1'");
        return null;
    }
}
