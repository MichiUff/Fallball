using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int downwardMovementSpeed = 20;

    private Rigidbody playerRigidbody;
    // Use this for initialization
    void Start ()
    {
        playerRigidbody = transform.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(MenuButtonManager.Paused)
            playerRigidbody.velocity = new Vector3(0, 0, 0);
        else
            playerRigidbody.velocity = new Vector3(0, -downwardMovementSpeed, 0);
    }

    void FixedUpdate()
    {
        
    }
}
