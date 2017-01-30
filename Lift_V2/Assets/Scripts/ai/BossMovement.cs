using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    //automatic behaviour -- for now
    GameObject player;

    float timer = 1;


	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;

        var pos = player.transform.position;

        if (Vector3.Distance(transform.position, pos) < 2)
        {
            MoveAwayFromPlayer();
        }
	}

    private void MoveAwayFromPlayer()
    {
        Vector3 direction = transform.position - player.transform.position;
        //direction.Normalize();

        //direction * (accerlation * (percentage)) * frame_percentage
        direction = direction * (1f * (2f - Vector3.Distance(transform.position, player.transform.position))) * Time.deltaTime;
        //debug
        /*
        if (timer <= 0)
        {
            timer = 1;
            var note = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.z, 2));
            Debug.Log(note);
        }
        */
        direction.y = 0;
        transform.Translate(direction);
    }
}
