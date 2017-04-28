using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class AIInfo : MonoBehaviour {

    Regex touristRegex = new Regex(@"Tourist");
    Regex bossRegex = new Regex(@"Boss");
    Regex businessRegex = new Regex(@"Business");

    private int bossMood;
    private int businessMood;
    [SerializeField]
    private int touristMood;
    
    public void setMood(string name, int mood) {
        if (touristRegex.IsMatch(name)) touristMood = mood;
        else if (bossRegex.IsMatch(name)) bossMood = mood;
        else if (businessRegex.IsMatch(name)) businessMood = mood;
        else throw new System.ArgumentOutOfRangeException("PATRON NOT FOUND, TRIED TO PASS: " + name);
    }

    public int getMood(string name) {
        if (touristRegex.IsMatch(name)) return touristMood;
        else if (bossRegex.IsMatch(name)) return bossMood;
        else if (businessRegex.IsMatch(name)) return businessMood;
        else throw new System.ArgumentOutOfRangeException("PATRON NOT FOUND, TRIED TO PASS: " + name);
    }
}
