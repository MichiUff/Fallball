using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

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
    private float startPositionX = float.MaxValue;
    internal void Die()
    {
        MenuButtonManager.Paused = true;
        MenuButtonManager.GameoverScreen();
    }

    private Rigidbody2D playerRigidbody;
    private CircleCollider2D playerCollider;
    // Use this for initialization
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();

        playerRigidbody.freezeRotation = true;

        animator = transform.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        playerCollider.isTrigger = Swipe.IsDrawing;

        if (MenuButtonManager.Paused || Swipe.IsDrawing)
        {
            playerRigidbody.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            playerRigidbody.velocity = new Vector3(0, -DownwardMovementSpeed, 0);
            
            //Rotate on slide
            if (startPositionX != float.MaxValue)
            {
                if (transform.position.x < startPositionX)
                {
                    RotateLeft();
                    startPositionX = float.MaxValue;
                }
                else if (transform.position.x > startPositionX)
                {
                    RotateRight();
                    startPositionX = float.MaxValue;
                }
            }
        }
    }

    //Called in draw mode
    void OnTriggerEnter2D(Collider2D collision)
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

    void OnTriggerExit2D(Collider2D collision)
    {
        currentlyColliding = false;
    }

    //Called in game mode
    void OnCollisionEnter2D(Collision2D collision)
    {
        currentlyCollidingWithLine = true;
        animator.SetBool("Slide", true);

        startPositionX = transform.position.x;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        currentlyCollidingWithLine = false;
        animator.SetBool("Slide", false);
    }

    private void RotateLeft()
    {
        var tmp = transform.localScale;

        if (tmp.x > 0)
        {
            tmp.x *= -1;
            transform.localScale = tmp;
        }
    }

    private void RotateRight()
    {
        var tmp = transform.localScale;

        if (tmp.x < 0)
        {
            tmp.x *= -1;
            transform.localScale = tmp;
        }
    }
}