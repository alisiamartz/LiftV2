using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class JsonParser : MonoBehaviour {

    string path;
    string jsonString;

    string[] test = { "hello", "there" };

	// Use this for initialization
	void Start () {
        path = Application.streamingAssetsPath + "/../Scripts/ai/BossInfo.json";
        jsonString = File.ReadAllText(path);

        parser x = JsonUtility.FromJson<parser>(jsonString);

        for (int i = 0; i < x.init.Length; i++)
        {
            x.init[i].print();
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class parser
{
    public events[] init;
}

[System.Serializable]
public class events
{
    public string type;
    public int[] utility;
    public int wait;
    public List<string> dialogue;
    public List<string> listen;
    public string defaultDialogue;
    public List<string> utilReponse;

    public void print()
    {
        Debug.Log(this.dialogue.Count);
    }
}