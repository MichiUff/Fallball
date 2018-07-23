using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    PlayerController player;

	// Use this for initialization
	void Start () {
        player = PlayerController.FirstPlayer;
	}
	
	// Update is called once per frame
	void Update ()
    {
        var dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist > 100)
            Destroy(gameObject);
    }
}
