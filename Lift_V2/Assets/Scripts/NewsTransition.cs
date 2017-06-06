using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsTransition : MonoBehaviour {

    public Material news;
    public Texture day1A;
    public Texture day1B;
    public Texture day1C;

    private GameObject objMood;

    // Use this for initialization
    void Start () {
        objMood = GameObject.FindGameObjectWithTag("HotelManager");
    }
	
	// Update is called once per frame
	public void DAY1() {
        Debug.Log(objMood.GetComponent<AIInfo>().bossMood);
        if (objMood.GetComponent<AIInfo>().bossMood > 3) { news.mainTexture = day1A; }
        else if (objMood.GetComponent<AIInfo>().bossMood < -3) { news.mainTexture = day1C; }
        else { news.mainTexture = day1B; }
    }

    public void DAY2()
    {
        news.mainTexture = day1A;
    }

    public void DAY3()
    {
        news.mainTexture = day1A;
    }

    public void DAY4()
    {
        news.mainTexture = day1A;
    }

    public void DAY5()
    {
        news.mainTexture = day1A;
    }
}
