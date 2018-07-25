using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkManager : MonoBehaviour
{
    public static InkManager Instance;

    public int MaxInk = 1000;
    private int currentInk;
    public int StartInk = 100;

    public int OneInkPerLength = 3;

    public int CurrentInk
    {
        get
        {
            return currentInk;
        }
        set
        {
            currentInk = value;

            if (currentInk < MapModification.MinimumLength)
                currentInk = 0; 

            if (currentInk > MaxInk)
                currentInk = MaxInk;

            OnInkUpdated();
        }
    }

    // Use this for initialization
    void Start()
    {
        Instance = this;
        currentInk = StartInk;
        OnInkUpdated();
    }

    public delegate void UpdateInk(int ink);
    public event UpdateInk UpdatedInk;

    void OnInkUpdated()
    {
        if (UpdatedInk != null)
            UpdatedInk(currentInk);
    }
}
