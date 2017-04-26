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
        path = Application.streamingAssetsPath + "/json/" + filename;
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
    //public List<change> changeList; //no longer used
}

[System.Serializable]
public class agentAttr
{
    public short mood; //range -10 <-> 10 neutral -3 <=> 3
    public int goal;
    public float patience;
}

[System.Serializable]
public class node
{
    public string name;
    public int wait;
    //public List<string> choose; //no longer used
    public List<string> dialogue;
    public List<string> listen;
    public List<short> change;
    public List<string> toNode;
    public string noResponse;
    public short noResponseChange;
}

//legacy

[System.Serializable]
public class change
{
    public string name;
    public int happiness;
    public int sadness;
    public int confusion;
    public int anger;
}