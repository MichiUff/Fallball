using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapModification : MonoBehaviour
{
    public Transform Cube;
    public float Ancho = 3;
    public float Alto = 10;

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
        {
            Debug.Log("Draw is ended - Draw from " + start + "to " + end);

            DrawALine(new Vector3(start.x, start.y, 90), new Vector3(end.x, end.y, 90));
        }
    }

    void DrawALine(Vector3 start, Vector3 end)
    {
        Vector3 posC = ((end - start) * 0.5F) + start;
        float lengthC = (end - start).magnitude; //C#
        float sineC  = (end.y - start.y) / lengthC;
        float angleC = Mathf.Asin(sineC) * Mathf.Rad2Deg;
        if (end.x < start.x) { angleC = 0 - angleC; }


        Transform myLine = Instantiate(Cube, posC, Quaternion.identity);
        myLine.localScale = new Vector3(lengthC, Ancho, Alto);

        myLine.rotation = Quaternion.Euler(0, 0, angleC);
    }

}
