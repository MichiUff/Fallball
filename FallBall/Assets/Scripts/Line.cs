using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
    	
	// Update is called once per frame
	void Update ()
    {
        var dist = Vector3.Distance(PlayerController.FirstPlayer.transform.position, transform.position);

        if (dist > 100)
            Destroy(gameObject);
    }
}
