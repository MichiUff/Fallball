using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkManager : MonoBehaviour
{
    private static int maxInk = 1000;
    private static int currentInk = 100;

    public static int CurrentInk
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

            if (currentInk > maxInk)
                currentInk = maxInk;

            OnInkUpdated();
        }
    }

    // Use this for initialization
    void Start()
    {
        OnInkUpdated();
    }

    public delegate void UpdateInk(int ink);
    public static event UpdateInk UpdatedInk;

    static void OnInkUpdated()
    {
        if (UpdatedInk != null)
            UpdatedInk(currentInk);
    }
}
