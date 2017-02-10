using System.Collections;
using System.Collections.Generic;
using System.Linq; //for parsing arrays
using UnityEngine;

public class BossActions : MonoBehaviour {
    //utility
    public float happiness;
    public float sadness;
    public float confusion;
    public float anger;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Hello()
    {
        //static action
        Debug.Log("Hello, are you ready?");
    }

    public void Introduction0(int response = -1)
    {
        //will do action based on response

        //0 for yes 1 for no
        switch(response)
        {
            case 0:
                happiness += 10;
                Debug.Log("Good!");
                break;
            case 1:
                sadness += 10;
                Debug.Log("Oh.");
                break;
            default:
                confusion += 10;
                Debug.Log("The silent type huh?");
                break;
        }
    }

    public void Introduction1()
    {
        //will do action based on utility
        float[] util = {happiness, sadness, confusion};
        int urgent = util.ToList().IndexOf(util.Max());
        switch(urgent)
        {
            case 0:
                Debug.Log("Let's get started.");
                break;
            case 1:
                Debug.Log("Well you should be ready after this.");
                break;
            case 2:
                Debug.Log("Well at least you're not talking. That's good.");
                break;
            default:
                Debug.Log("!!!Something went wrong with the program!!!"); //should never be reached
                break;
        }
    }
}
