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
        {
            Debug.Log("Draw is ended - Draw from " + start + "to " + end);

            DrawLine(new Vector3(start.x, start.y, 90), new Vector3(end.x, end.y, 90), Color.green);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        //lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        //GameObject.Destroy(myLine, duration);
    }

}
