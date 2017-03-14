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
    public List<node> nodes;
    public List<change> changeList;
}

[System.Serializable]
public class agentAttr
{
    public int[] utility; //happiness sadness confusion anger
    public int goal;
}

[System.Serializable]
public class node
{
    public string name;
    public int wait;
    public List<string> choose;
    public List<string> dialogue;
    public List<string> listen;
    public List<string> change;
    public List<string> toNode;
    public string noResponse;
    public string noResponseChange;
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