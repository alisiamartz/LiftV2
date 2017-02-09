using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class JsonParser : MonoBehaviour {

    string path;
    string jsonString;
    Dictionary<string, string> dict = new Dictionary<string, string>();

    public BossEvents bossEvents = new BossEvents();

	// Use this for initialization
	void Start () {

        dict.Add("1", "one");
        dict.Add("2", "two");

        Debug.Log(dict["2"]);

        path = Application.streamingAssetsPath + "/../Scripts/ai/BossInfo.json";
        jsonString = File.ReadAllText(path);

        parser x = JsonUtility.FromJson<parser>(jsonString);

        parseEvents(x.eventList);
	}
	
	// Update is called once per frame
	void Update () {
		
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
    public List<string> utilReponse;
    public string noReponse;
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