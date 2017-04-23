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
            var startFloor = 0;
            var prefab = Resources.Load("Patrons/Boss1") as GameObject;
            Debug.Log(prefab.gameObject.name);
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Boss2") {
            var startFloor = 0;
            var prefab = Resources.Load("Patrons/Boss2") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Boss3") {
            var startFloor = 5;
            var prefab = Resources.Load("Patrons/Boss3") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Business1") {
            var startFloor = 0;
            var prefab = Resources.Load("Patrons/Business1") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Business2") {
            var startFloor = 2;
            var prefab = Resources.Load("Patrons/Business2") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Business3") {
            var startFloor = 2;
            var prefab = Resources.Load("Patrons/Business3") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Tourist1") {
            var startFloor = 5;
            var prefab = Resources.Load("Patrons/Tourist1") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Tourist2") {
            var startFloor = 3;
            var prefab = Resources.Load("Patrons/Tourist2") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Tourist3") {
            var startFloor = 3;
            var prefab = Resources.Load("Patrons/Tourist3") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Adultress1") {
            var startFloor = 0;
            var prefab = Resources.Load("Patrons/Adultress1") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Adultress2") {
            var startFloor = 0;
            var prefab = Resources.Load("Patrons/Adultress2") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Adultress3") {
            var startFloor = 3;
            var prefab = Resources.Load("Patrons/Adultress3") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Artist1") {
            var startFloor = 1;
            var prefab = Resources.Load("Patrons/Artist1") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Artist2") {
            var startFloor = 4;
            var prefab = Resources.Load("Patrons/Artist2") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Artist3") {
            var startFloor = 1;
            var prefab = Resources.Load("Patrons/Artist3") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Server1") {
            var startFloor = 5;
            var prefab = Resources.Load("Patrons/Server1") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Server2") {
            var startFloor = 5;
            var prefab = Resources.Load("Patrons/Server2") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Server3") {
            var startFloor = 5;
            var prefab = Resources.Load("Patrons/Server3") as GameObject;
            return new Patron(prefab, startFloor);
        }

        if (patronName == "Server4") {
            var startFloor = 5;
            var prefab = Resources.Load("Patrons/Server4") as GameObject;
            return new Patron(prefab, startFloor);
        }

        Debug.LogError(patronName + " is not a valid patronName. Format with the patron name and the conversation number: 'Boss1'");
        return null;
    }
}
