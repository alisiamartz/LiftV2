using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour {

    public string filename; //with .json

    string path;
    string jsonString;
    JsonParser jp = new JsonParser();
    CreateCharacters cc = new CreateCharacters();

    // Use this for initialization
    void Start () {
        jp.parse(filename);
        cc.create(jp.parsedJson, "Business Man");
	}
	
	// Update is called once per frame
	void Update () {

    }

    //
}
