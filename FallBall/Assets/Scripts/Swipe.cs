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

    public static Vector2 TapCoordinates;

    private static bool tap = false;
    private Vector2 startTouch, endTouch;
    private GameObject temporaryLine;

    private bool standalone, mobile;

    public delegate void DrawEnd(Vector2 start, Vector2 end);
    public event DrawEnd DrawEnded;

    public delegate void Tap(Vector2 pos);
    public static event Tap Taped;

    private void FixedUpdate()
    {
        if (MenuManager.Paused)
            return;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = GetWorldCoordianates(Input.mousePosition);

            IsDrawing = true;
            standalone = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouch = GetWorldCoordianates(Input.mousePosition);
            OnDrawEnd(startTouch, endTouch);
            
            CheckForTap();

            Reset();
        }
        else if (IsDrawing && standalone)
        {
            RemoveTemporaryLine();

            temporaryLine = MapModification.DrawALine(startTouch, GetWorldCoordianates(Input.mousePosition));

            PlayerController.FirstPlayer.currentlyColliding = false; //On Trigger Exit is not called when destroying
        }

        #endregion

        #region Mobile Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTouch = GetWorldCoordianates(Input.touches[0].position);

                IsDrawing = true;
                mobile = true;

            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                endTouch = GetWorldCoordianates(Input.touches[0].position);
                OnDrawEnd(startTouch, endTouch);

                CheckForTap();

                Reset();
            }
            else if (IsDrawing && mobile)
            {
                RemoveTemporaryLine();

                temporaryLine = MapModification.DrawALine(startTouch, GetWorldCoordianates(Input.touches[0].position));

                PlayerController.FirstPlayer.currentlyColliding = false; //On Trigger Exit is not called when destroying
            }
        }

        #endregion
    }

    private void Reset()
    {
        startTouch = Vector2.zero;
        endTouch = Vector2.zero;
        IsDrawing = standalone = mobile = false;

        RemoveTemporaryLine();
        PlayerController.FirstPlayer.currentlyColliding = false; //On Trigger Exit is not called when destroying

        temporaryLine = null;
    }

    private void OnDrawEnd(Vector2 start, Vector2 end)
    {
        if (DrawEnded != null && start != end)
            DrawEnded(start, end);
    }

    private void OnTap(Vector2 pos)
    {
        if (Taped != null)
            Taped(pos);
    }

    private Vector2 GetWorldCoordianates(Vector3 v)
    {
        v.z = 100;
        v = Camera.main.ScreenToWorldPoint(v);

        return v;
    }

    private void RemoveTemporaryLine()
    {
        if (temporaryLine != null)
        {
            temporaryLine.GetComponent<Line>().Destroy(); //TODO regarding event unsubscribe
            //GameObject.Destroy(temporaryLine);
        }
    }

    private void CheckForTap()
    {
        if ((endTouch - startTouch).magnitude < MapModification.MinimumLength)
        {
            OnTap(startTouch);
        }
    }
}