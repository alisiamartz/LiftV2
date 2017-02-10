using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    Dictionary<string, KeyCode> dict = new Dictionary<string, KeyCode>();

	// Use this for initialization
	void Start () {
        dict.Add("yes", KeyCode.T);
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime;
        var z = Input.GetAxis("Vertical") * Time.deltaTime;

        transform.Translate(x, 0, z);

        if (Input.GetKeyDown(dict["yes"]))
        {
            Debug.Log("key down yes");
        }
	}
}