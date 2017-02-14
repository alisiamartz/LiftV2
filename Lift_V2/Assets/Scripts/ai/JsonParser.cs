using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class JsonParser : MonoBehaviour {

    string path;
    string jsonString;
    public parser parsedJson;

    BossEvents bossEvents = new BossEvents();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void parse(string filename)
    {
        path = Application.streamingAssetsPath + "/../Scripts/ai/" + filename;
        jsonString = File.ReadAllText(path);
        parsedJson = JsonUtility.FromJson<parser>(jsonString);
    }

    public void parseEvents(events[] eventsList)
    {
        for (int i = 0; i < eventsList.Length; i++)
        {
            events e = eventsList[i];
            switch(e.type)
            {
                case "init":
                    //do something
                    Debug.Log("init");
                    break;
                case "action":
                    //do something
                    Debug.Log("action");
                    break;
                case "listen":
                    //do something
                    Debug.Log("listen");
                    break;
                case "utility":
                    //do something
                    Debug.Log("utility");
                    break;
                default:
                    Debug.Log("Error: something wrong with json type at element " + i);
                    return;
            }
        }
    }
}

[System.Serializable]
public class parser
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