using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

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

        //TEST
        GameObject prefab;

        Regex touristRegex = new Regex(@"Tourist");
        Regex bossRegex = new Regex(@"Boss");
        Regex businessRegex = new Regex(@"Business");

        GameObject touristPatron = Resources.Load("Patrons/Tourist1") as GameObject;
        GameObject bossPatron = Resources.Load("Patrons/Boss1") as GameObject;
        GameObject businessPatron = Resources.Load("Patrons/Business1") as GameObject;

        if (touristRegex.IsMatch(patronName)) prefab = touristPatron;
        else if (bossRegex.IsMatch(patronName)) prefab = bossPatron;
        else if (businessRegex.IsMatch(patronName)) prefab = businessPatron;
        else throw new System.ArgumentOutOfRangeException("ANDREW U DUN GOOFED");

        if (patronName == "Boss1") return new Patron(prefab, 0);

        if (patronName == "Boss2") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Boss3") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Business1") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Business2") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Business3") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Tourist1") return new Patron(prefab, 1);

        if (patronName == "Tourist2") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Tourist3") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Adultress1") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Adultress2") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Adultress3") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Artist1") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Artist2") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Artist3") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Server1") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Server2") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Server3") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Server4") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        throw new System.ArgumentException("Patron Name not found: " + patronName);

        /*
        if(patronName == "Boss1") {
            var startFloor = 0;
            var prefab = Resources.Load("Patrons/Boss1") as GameObject;
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
            var startFloor = 1;
            var prefab = Resources.Load("Patrons/Tourist1") as GameObject;

            //TESTING SOMETHING HERE
            prefab.AddComponent<GenericAI>();
            prefab.GetComponent<GenericAI>().setFilename("1.2Tourist.json");

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
        */
    }

    public void configPatron(ref GameObject patronObject, string patronName)
    {

        if (patronName == "Boss1")
        {
            patronObject.AddComponent<Boss1AI>();
        }

        else if (patronName == "Boss2")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Boss3")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Business1")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Business2")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Business3")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Tourist1")
        {
            patronObject.AddComponent<GenericAI>();
            patronObject.GetComponent<GenericAI>().setFilename("1.2Tourist.json");
            return;
        }

        else if (patronName == "Tourist2")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Tourist3")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Adultress1")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Adultress2")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Adultress3")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Artist1")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Artist2")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Artist3")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Server1")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Server2")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Server3")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Server4")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else
        {
            throw new System.ArgumentException("Patron Name not found: " + patronName);
        }
    }
}
