using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private ActionTick tick;
    private int SamePositionCount;
    private Vector3 oldPosition;

    public float OneScorePerMeters = 100;
    private float meterSinceLastScore;

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
        tick = new ActionTick(1000);
    }

    public int DownwardMovementSpeed = 100;

    public bool currentlyColliding = false;
    private bool currentlyCollidingWithLine = false; //Temporary value for switchover from collisoon to trigger, trigger should be ignored one time
    private GameObject currentCollidingLine;

    private float startPositionX = float.MaxValue;
    internal void Die()
    {
        animator.SetBool("Dead", true);

        MenuManager.Paused = true;
        MenuManager.GameoverScreen();
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

        if (MenuManager.Paused || Swipe.IsDrawing)
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

            if(tick.IsAction())
            {
                //Debug.Log(Vector3.Distance(oldPosition, transform.position));
                if(oldPosition != null && Vector3.Distance(oldPosition, transform.position) < 5)
                {
                    SamePositionCount++;
                }
                else
                {
                    SamePositionCount = 0;
                    meterSinceLastScore += Math.Abs(transform.position.y) - Math.Abs(oldPosition.y);
                    oldPosition = transform.position;
                }

                if(SamePositionCount >= 3)
                {
                    Debug.Log("We must break!");
                    if (currentCollidingLine != null)
                        Destroy(currentCollidingLine);
                }

                //Score a point while meters way done
                if(meterSinceLastScore > OneScorePerMeters)
                {
                    ScoreManager.Instance.CurrentScore += 1;
                    meterSinceLastScore = 0;
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
        if(collision.transform.tag == "Line")
            currentCollidingLine = collision.gameObject;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        currentlyCollidingWithLine = false;
        animator.SetBool("Slide", false);
        currentCollidingLine = null;
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