﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraPath : MonoBehaviour {
    //The class for nodes which extras use as waypoints

    private Mesh wireframe;

    public bool startNode = true;

    //The name of the animation an extra transitions to when they reach this node
    public stateList.animationState animationID;

    //The reference to the next path node, if no next node then the path is complete
    public GameObject nextNode;

    public void AddWaypoint()
    {
        var o = gameObject;

        if (nextNode)
        {
            o = nextNode;
        }

        //Create a clone of the existing waypoint, with the same rotation and parent floor
        var offset = new Vector3(0, 0, 1);
        var spawnPosition = o.transform.position + offset;

        GameObject newW = Instantiate(gameObject, spawnPosition, Quaternion.identity, o.transform.parent);
        o.GetComponent<extraPath>().nextNode = newW;
        newW.GetComponent<extraPath>().startNode = false;
    }

    private void OnDrawGizmos()
    {
        if (wireframe == null)
        {
            GameObject extraPatron = Resources.Load("Editor/wireframe") as GameObject;
            wireframe = extraPatron.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            transform.localScale = extraPatron.transform.localScale;
            transform.rotation = extraPatron.transform.rotation;
        }

        Gizmos.color = new Color(1, 0, 1, 1);

        Gizmos.DrawWireMesh(wireframe, transform.position, transform.rotation, transform.localScale);

        if (nextNode)
        {
            Gizmos.DrawLine(transform.position, nextNode.transform.position);
        }
    }
}
