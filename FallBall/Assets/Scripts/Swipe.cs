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
    private GameObject temporaryLine;
    //private DateTime startTime;
    //private float mapMovementY;

    private bool standalone, mobile;

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
            standalone = true;
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
        else if (IsDrawing && standalone)
        {
            if (temporaryLine != null)
                GameObject.Destroy(temporaryLine);

            temporaryLine = MapModification.DrawALine(startTouch, GetWorldCoordianates(Input.mousePosition), false);
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
                mobile = true;

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
            else if (IsDrawing && mobile)
            {
                if (temporaryLine != null)
                    GameObject.Destroy(temporaryLine);

                temporaryLine = MapModification.DrawALine(startTouch, GetWorldCoordianates(Input.touches[0].position), false);
            }
        }

        //if (startTouch != Vector2.zero)
        //mapMovementY += (float)Time.deltaTime * PlayerController.Instances.First().DownwardMovementSpeed;

        #endregion
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
        IsDrawing = standalone = mobile = false;

        //startTime = DateTime.MinValue;
        //mapMovementY = 0;

        if (temporaryLine != null)
            GameObject.Destroy(temporaryLine);

        temporaryLine = null;
    }

    private void OnDrawEnd(Vector2 start, Vector2 end)
    {
        if (DrawEnded != null && start != end)
            DrawEnded(start, end);
    }

    private Vector2 GetWorldCoordianates(Vector3 v)
    {
        v.z = 100;
        v = Camera.main.ScreenToWorldPoint(v);

        return v;
    }
}