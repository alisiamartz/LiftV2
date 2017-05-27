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

        if (patronName == "Business3") return new Patron(prefab, 3);

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
            patronObject.AddComponent<BossDay1AI>();

            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().dayName = "Day1";
        }

        else if (patronName == "Boss2")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("2.2Boss.json");
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().dayName = "Day2";
        }

        else if (patronName == "Boss3")
        {
            patronObject.AddComponent<GenericAIv2>();
            //patronObject.GetComponent<GenericAIv2>().setFilename("5.4BossN.json");
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            //Updated to include different moods
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("5.4BossH.json");
            else if (mood < -3) patronObject.GetComponent<GenericAIv2>().setFilename("5.4BossA.json");
            else patronObject.GetComponent<GenericAIv2>().setFilename("5.4BossN.json");
            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().dayName = "Day5";
        }

        else if (patronName == "Business1")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("2.1Businessman.json");

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().dayName = "Day2";
        }

        else if (patronName == "Business2")
        {
            patronObject.AddComponent<GenericAIv2>();
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("3.2BusinessmanH.json");
            else if (mood < -3) patronObject.GetComponent<GenericAIv2>().setFilename("3.2BusinessmanA.json");
            else patronObject.GetComponent<GenericAIv2>().setFilename("3.2BusinessmanN.json");

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().dayName = "Day3";
        }

        else if (patronName == "Business3")
        {
            //Hard coded interaction
            patronObject.AddComponent<BusinessDay4AI>();
            patronObject.GetComponent<BusinessDay4AI>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().dayName = "Day4";
        }

        else if (patronName == "Business4") {
            patronObject.AddComponent<GenericAIv2>();
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("5.1BusinessmanH.json");
            else if (mood < -3) patronObject.GetComponent<GenericAIv2>().setFilename("5.1BusinessmanN.json");
            else patronObject.GetComponent<GenericAIv2>().setFilename("5.1BusinessmanA.json");

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().dayName = "Day5";
        }

        else if (patronName == "Tourist1")
        {
            patronObject.AddComponent<Tourist1AI>();
            patronObject.GetComponent<Tourist1AI>().setFilename("1.2Tourist.json");

            patronObject.GetComponent<PatronAudio>().patronName = "Tourist";
            patronObject.GetComponent<PatronAudio>().dayName = "Day1";
        }

        else if (patronName == "Tourist2")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("3.1Tourist.json");
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Tourist";
            patronObject.GetComponent<PatronAudio>().dayName = "Day3";
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
            patronObject.AddComponent<GenericAI>();
            patronObject.GetComponent<GenericAI>().setFilename("1.4Server.json");
            patronObject.GetComponent<GenericAI>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Server";
            patronObject.GetComponent<PatronAudio>().dayName = "Day1";
        }

        else if (patronName == "Server2")
        {
            patronObject.AddComponent<GenericAI>();
            string[] s = { "2.4ServerH.json", "2.4ServerN.json", "2.4ServerA.json" };
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAI>().setMood(mood);
            if (mood > 3) patronObject.GetComponent<GenericAI>().setFilename(s[0]);
            else if (mood < -3) patronObject.GetComponent<GenericAI>().setFilename(s[2]);
            else patronObject.GetComponent<GenericAI>().setFilename(s[1]);

            patronObject.GetComponent<PatronAudio>().patronName = "Server";
            patronObject.GetComponent<PatronAudio>().dayName = "Day2";
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
