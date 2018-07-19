using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static List<PlayerController> Instances = new List<PlayerController>();

    void Awake()
    {
        Instances.Add(this);
    }

    public int DownwardMovementSpeed = 20;

    private Rigidbody playerRigidbody;
    // Use this for initialization
    void Start ()
    {
        playerRigidbody = transform.GetComponent<Rigidbody>();
    }
	
	void FixedUpdate ()
    {
        if(!MenuButtonManager.Paused)
        {
            playerRigidbody.velocity = new Vector3(0, -DownwardMovementSpeed, 0);
        }
        else
            playerRigidbody.velocity = new Vector3(0, 0, 0);
    }
}
