using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Patrons {

    public class Patron {
        public GameObject prefab;
        public int startFloor;

        public Patron(GameObject preF, int sFloor) {
            prefab = preF;
            startFloor = sFloor;
        }
    }

    public Patron fetchPatron(string patronName) {

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
        else throw new System.ArgumentOutOfRangeException("PREFAB NOT FOUND, TRIED TO PASS: " + patronName);

        if (patronName == "Boss1") return new Patron(prefab, 0);

        if (patronName == "Boss2") return new Patron(prefab, 1);

        if (patronName == "Boss3") return new Patron(prefab, 6);

        if (patronName == "Business1") return new Patron(prefab, 1);

        if (patronName == "Business2") return new Patron(prefab, 2);

        if (patronName == "Business3") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Tourist1") return new Patron(prefab, 1);

        if (patronName == "Tourist2") return new Patron(prefab, 3);

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

        throw new System.ArgumentOutOfRangeException("STARTING FLOOR NOT FOUND, TRIED TO PASS: " + patronName);
    }

    public void configPatron(ref GameObject patronObject, string patronName)
    {

        if (patronName == "Boss1")
        {
            patronObject.AddComponent<Boss1AI>();

            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().patronName = "Day1";
        }

        else if (patronName == "Boss2")
        {
            patronObject.AddComponent<GenericAI>();
            patronObject.GetComponent<GenericAI>().setFilename("2.2Boss.json");
            patronObject.GetComponent<GenericAI>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().patronName = "Day2";
        }

        else if (patronName == "Boss3")
        {
            patronObject.AddComponent<GenericAI>();
            patronObject.GetComponent<GenericAI>().setFilename("3.2Boss.json");
            patronObject.GetComponent<GenericAI>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().patronName = "Day3";
        }

        else if (patronName == "Business1")
        {
            patronObject.AddComponent<GenericAI>();
            patronObject.GetComponent<GenericAI>().setFilename("2.1Businessman.json");

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().patronName = "Day2";
        }

        else if (patronName == "Business2")
        {
            patronObject.AddComponent<GenericAI>();
            string[] s = { "3.1BusinessmanH.json", "3.1BusinessmanN.json", "3.1BusinessmanA.json" };
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAI>().setMood(mood);
            if (mood > 3) patronObject.GetComponent<GenericAI>().setFilename(s[0]);
            else if (mood < -3) patronObject.GetComponent<GenericAI>().setFilename(s[2]);
            else patronObject.GetComponent<GenericAI>().setFilename(s[1]);

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().patronName = "Day3";
        }

        else if (patronName == "Business3")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Tourist1")
        {
            patronObject.AddComponent<Tourist1AI>();
            patronObject.GetComponent<Tourist1AI>().setFilename("1.2Tourist.json");

            patronObject.GetComponent<PatronAudio>().patronName = "Tourist";
            patronObject.GetComponent<PatronAudio>().patronName = "Day1";
        }

        else if (patronName == "Tourist2")
        {
            patronObject.AddComponent<GenericAI>();
            patronObject.GetComponent<GenericAI>().setFilename("3.1Tourist.json");
            patronObject.GetComponent<GenericAI>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Tourist";
            patronObject.GetComponent<PatronAudio>().patronName = "Day2";
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
