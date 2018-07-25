using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController FirstPlayer
    {
        get
        {
            return Instances.First();
        }
    }
    public static List<PlayerController> Instances = new List<PlayerController>();

    void Awake()
    {
        Instances.Clear();
        Instances.Add(this);
    }

    public int DownwardMovementSpeed = 20;

    public bool currentlyColliding = false;
    private bool currentlyCollidingWithLine = false; //Temporary value for switchover from collisoon to trigger, trigger should be ignored one time

    internal void Die()
    {
        MenuButtonManager.Paused = true;
        MenuButtonManager.GameoverScreen();
    }

    private Rigidbody playerRigidbody;
    private Collider playerCollider;
    // Use this for initialization
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        playerCollider.isTrigger = Swipe.IsDrawing;

        if (MenuButtonManager.Paused || Swipe.IsDrawing)
        {
            playerRigidbody.velocity = new Vector3(0, 0, 0);
        }
        else
            playerRigidbody.velocity = new Vector3(0, -DownwardMovementSpeed, 0);
    }

    //Called in draw mode
    void OnTriggerEnter(Collider collision)
    {

        if (!currentlyCollidingWithLine)
        {
            currentlyColliding = Swipe.IsDrawing;
        }
        else //Catch init call of on trigger enter, which occurs when player on top of line and Collision is changed to trigger
        {
            currentlyCollidingWithLine = false;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        currentlyColliding = false;
    }

    //Called in game mode
    void OnCollisionEnter(Collision collision)
    {
        currentlyCollidingWithLine = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        currentlyCollidingWithLine = false;
    }
}