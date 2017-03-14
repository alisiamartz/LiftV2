using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for parsing lists
using System.Linq;
using Edwon.VR.Gesture;
//ui
using UnityEngine.UI;


public class Agent : MonoBehaviour {

    /*
    public float happiness = 0;
    public float sadness = 0;
    public float confusion = 0;
    public float anger = 0;

    //delegate events in an array
    public delegate bool Event();
    public List<Event> timeline = new List<Event>();

    //timer
    public float wait = 0;
    public bool flag = true; //true, ready to change wait. false, do not change wait

    //dictionaries
    public Dictionary<string, change> actionDict;

    //set by agent controller
    public GestureList gl;
    public Text bubble;

    public void setGestureList(ref GestureList gl)
    {
        this.gl = gl;
    }

    public void setBubble(ref Text b)
    {
        bubble = b;
    }

    public void doChange(change c)
    {
        happiness += c.happiness;
        sadness += c.sadness;
        confusion += c.confusion;
        anger += c.anger;
    }

    public bool action(events e)
    {
        if (flag)
        {
            //Debug.Log(e.dialogue[0]);\
            bubble.text = (e.dialogue[0]);
            wait = e.wait;
            flag = false;
        }
        wait -= Time.deltaTime;

        if (wait <= 0) return true;
        else return false;
    }

    public bool listen(events e)
    {
        if (flag)
        {
            gl.resetGesture();
            wait = e.wait;
            flag = false;
        }
        wait -= Time.deltaTime;

        //check listened responses
        for (int i = 0; i < e.listen.Count; i++)
        {
            if (gl.getGesture() == e.listen[i])
            {
                doChange(actionDict[e.action[i]]);
                return true;
            }
        }

        //no response
        if (wait <= 0)
        {
            doChange(actionDict[e.noResponse]);
            return true;
        } else
        {
            return false;
        }
    }

    //for events.utility, neutral shall always be last in the index
    public bool utility(events e)
    {
        if (flag)
        {
            List<float> util = new List<float>();
            for (int i = 0; i < e.utilResponse.Count; i++)
            {
                switch (e.utilResponse[i])
                {
                    case "happiness":
                        util.Add(happiness);
                        break;
                    case "sadness":
                        util.Add(sadness);
                        break;
                    case "confusion":
                        util.Add(confusion);
                        break;
                    case "anger":
                        util.Add(confusion);
                        break;
                    case "neutral":
                        //do nothing
                        break;
                    default:
                        Debug.Log("check utilResponse for some event");
                        break;
                }
            }
            int urgentIndex1 = util.ToList().IndexOf(util.Max()); //most urgent index
            float urgent1 = util[urgentIndex1]; //value of most urgent
            util[urgentIndex1] = float.MinValue;
            int urgentIndex2 = util.ToList().IndexOf(util.Max()); //second most urgent index
            float urgent2 = util[urgentIndex2]; //value of secxond most urgent

            //get correct utility response
            if (Mathf.Abs(urgent1 - urgent2) <= 2)
            {
                //Debug.Log(e.dialogue[e.dialogue.Count - 1]);
                bubble.text = e.dialogue[e.dialogue.Count - 1];
            }
            else
            {
                //Debug.Log(e.dialogue[urgentIndex1]);
                bubble.text = e.dialogue[urgentIndex1];
            }

            wait = e.wait;
            flag = false;
        }
        wait -= Time.deltaTime;

        if (wait <= 0) return true;
        else return false;
    }

    public bool end()
    {
        //do nothing
        return false;
    }

    */
}