using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class scenarioHolder : MonoBehaviour {

    public Vector3 scenarioLocation;
    public stateList.floor floorLocation;
    public stateList.group floorGrouping;

    [Header("Parameters")]
    public bool Morning;
    public bool Middday;
    public bool Night;
    public bool Day1;
    public bool Day2;
    public bool Day3;

    [Header("Overhear")]
    public AudioClip overhearAudio;
    [Tooltip("The staring time it takes for the scenario to trigger. A lower time means the scenario is easier to trigger")]
    public float focusTime;
    private bool audioTriggered = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Application.isPlaying)
        {
            scenarioLocation = transform.localPosition;
        } 
	}
}
