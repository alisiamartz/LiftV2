using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObjects : MonoBehaviour {

    private Rigidbody rb;
    public GameObject item1;
    public GameObject item2;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("objA")) {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("objB")) {
            other.gameObject.SetActive(false);
        }
    }
        // Update is called once per frame
        void Update () {
		
	}
}
