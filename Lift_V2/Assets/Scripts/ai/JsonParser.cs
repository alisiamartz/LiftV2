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
    public agentAttr agentAttr;
    public List<storyBeat> storyBeats;
}

[System.Serializable]
public class agentAttr
{
    public int[] utility; //happyniess sadness confusion anger
    public int goal;
}

[System.Serializable]
public class storyBeat
{
    public bool listen;
    public int wait;
    public List<string> gesture;
    public List<string> change;
    public string noGesture;
    public string negative;
    public string neutral;
    public string positive;
}

[System.Serializable]
public class change
{
    public string name;
    public int happiness;
    public int sadness;
    public int confusion;
    public int anger;
}

//legacy

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