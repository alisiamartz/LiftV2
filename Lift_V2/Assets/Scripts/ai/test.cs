using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime;
        var z = Input.GetAxis("Vertical") * Time.deltaTime;

        transform.Translate(x, 0, z);
	}
}
