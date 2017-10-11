using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraManager : MonoBehaviour {

    public GameObject[] QuartersPaths;
    public GameObject[] LobbyPaths;

    public GameObject patron;

    // Use this for initialization
    void Start () {
		for(var i = 0; i < LobbyPaths.Length; i++)
        {
            var targetPath = LobbyPaths[i];
            var p = Instantiate(patron, targetPath.transform.position, Quaternion.identity, targetPath.transform.parent);
            p.GetComponent<extraAI>().setNode(targetPath);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
