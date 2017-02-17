using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonParser {

    private string path;
    private string jsonString;
    private jsonClass parsedJson;

    public jsonClass parse(string filename)
    {
        path = Application.streamingAssetsPath + "/../Scripts/ai/" + filename;
        jsonString = File.ReadAllText(path);
        parsedJson = JsonUtility.FromJson<jsonClass>(jsonString);
        return parsedJson;
    }
}

[System.Serializable]
public class jsonClass
{
    public events[] eventList;
    public actions[] actionList;

}

[System.Serializable]
public class events
{
    public string type;
    public int[] utility; //happiness, sadness, confusion, anger
    public int wait;
    public List<string> dialogue;
    public List<string> listen;
    public List<string> utilResponse; //"neutral" is always last
    public List<string> action;
    public string noResponse;
    public string otherReponse;
}

[System.Serializable]
public class actions
{
    public string name;
    public change change;
}

[System.Serializable]
public class change
{
    public int happiness;
    public int sadness;
    public int confusion;
    public int anger;
}