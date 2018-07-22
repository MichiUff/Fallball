using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    #region Singelton

    public static Swipe Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new System.Exception("Swipe is a Singelton instance! You cant create more than one instance!");
    }

    #endregion

    public static bool IsDrawing = false;
    private Vector2 startTouch;
    //private DateTime startTime;
    //private float mapMovementY;

    public delegate void DrawEnd(Vector2 start, Vector2 end);
    public event DrawEnd DrawEnded;

    private void FixedUpdate()
    {
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = GetWorldCoordianates(Input.mousePosition);
            //startTime = DateTime.Now;
            IsDrawing = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //Map movement must be removed from vector
            //var mapMovementY = (float)(DateTime.Now - startTime).TotalSeconds * PlayerController.Instances.First().DownwardMovementSpeed;
            //var tmp = (Vector2)Input.mousePosition;
            //tmp.y += mapMovementY;

            OnDrawEnd(startTouch, GetWorldCoordianates(Input.mousePosition));
            Reset();
        }
        #endregion

        #region Mobile Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTouch = GetWorldCoordianates(Input.touches[0].position);
                    //Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                //startTime = DateTime.Now;
                IsDrawing = true;

            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                //Map movement must be removed from vector
                //var mapMovementY = (float) (DateTime.Now - startTime).TotalSeconds * PlayerController.Instances.First().DownwardMovementSpeed;
                //var tmp = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                //tmp.y += mapMovementY;

                OnDrawEnd(startTouch, GetWorldCoordianates(Input.touches[0].position));
                Reset();
            }
        }

        //if (startTouch != Vector2.zero)
            //mapMovementY += (float)Time.deltaTime * PlayerController.Instances.First().DownwardMovementSpeed;

        #endregion
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
        IsDrawing = false;

        //startTime = DateTime.MinValue;
        //mapMovementY = 0;
    }

    private void OnDrawEnd(Vector2 start, Vector2 end)
    {
        if (DrawEnded != null)
            DrawEnded(start, end);
    }

    private Vector2 GetWorldCoordianates(Vector3 v)
    {
        v.z = 100;
        v = Camera.main.ScreenToWorldPoint(v);

        return v;
    }
}