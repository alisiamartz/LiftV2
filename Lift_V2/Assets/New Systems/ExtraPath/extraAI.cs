using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraAI : MonoBehaviour {

    //The initial node is established when the extra is spawned from extraManager
    private GameObject currentNode;

    private stateList.animationState currentState;

    public float speed;
    
    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //If there is a next node, move towards it
        var nextNode = currentNode.GetComponent<extraPath>().nextNode;

        if (nextNode)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, step);

            if (transform.position == nextNode.transform.position)
            {
                currentNode = nextNode;
                currentState = nextNode.GetComponent<extraPath>().animationID;
            }
        }
	}

    public void setNode(GameObject target)
    {
        currentNode = target;
    }
}
