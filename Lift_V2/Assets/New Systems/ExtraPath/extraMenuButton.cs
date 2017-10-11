using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class extraMenuButton : MonoBehaviour {

    //Add a menu item
    [MenuItem("LiftTools/Create Extra Path")]

    static void CreateExtraPath(MenuCommand menuCommand)
    {
        GameObject w = new GameObject("Extra Waypoint");

        w.AddComponent<extraPath>();

        //Assign parent to currently active floor
        GameObjectUtility.SetParentAndAlign(w, GameObject.FindGameObjectWithTag("Floor"));

        //Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(w, "Create " + w.name);
        Selection.activeObject = w;
    }
}
