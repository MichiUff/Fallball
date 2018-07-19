using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapModification : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Swipe.Instance.DrawEnded += Instance_DrawEnded;
    }

    public void Instance_DrawEnded(Vector2 start, Vector2 end)
    {
        //Only when on own map is painted
        var playerCoords = PlayerController.Instances.First().transform.position;
        var halfHeight = MapGenerator.RealHeight / 2;

        if (transform.position.y >= (playerCoords.y - halfHeight) && transform.position.y <= (playerCoords.y + halfHeight))
            Debug.Log("Draw is ended - Draw from " + start + "to " + end);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
