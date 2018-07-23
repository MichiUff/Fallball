using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapModification : MonoBehaviour
{
    public static Transform ValidLine;
    public static Transform InvalidLine;
    public static float Ancho = 1;
    public static float Alto = 1;
    
    private static Material materialLineInvalid;

    private static float panelPosition = 90; //TODO hardcoded....

    // Use this for initialization
    void Start()
    {
        Swipe.Instance.DrawEnded += Instance_DrawEnded;
        ValidLine = Resources.Load<Transform>("ValidLine");
        InvalidLine = Resources.Load<Transform>("InvalidLine");
    }

    public void Instance_DrawEnded(Vector2 start, Vector2 end)
    {
        //Only when on own map is painted
        var playerCoords = PlayerController.Instances.First().transform.position;
        var halfHeight = MapGenerator.RealHeight / 2;

        //Is on my map and Player is not colliding
        if (transform.position.y >= (playerCoords.y - halfHeight) && transform.position.y <= (playerCoords.y + halfHeight) && !PlayerController.FirstPlayer.currentlyColliding)
            DrawALine(start, end);
    }

    /// <summary>
    /// Creates a line in view
    /// </summary>
    /// <param name="start">Start point</param>
    /// <param name="end">End point</param>
    /// <returns></returns>
    public static GameObject DrawALine(Vector3 start, Vector3 end)
    {
        end.z = panelPosition;
        start.z = panelPosition;

        Vector3 posC = ((end - start) * 0.5F) + start;
        float lengthC = (end - start).magnitude; 
        float sineC = (end.y - start.y) / lengthC;
        float angleC = Mathf.Asin(sineC) * Mathf.Rad2Deg;
        if (end.x < start.x) { angleC = 0 - angleC; }

        Transform myLine;
        Debug.Log("HÖ: " + PlayerController.FirstPlayer.currentlyColliding);
        if (!PlayerController.FirstPlayer.currentlyColliding)
            myLine = Instantiate(ValidLine, posC, Quaternion.identity);
        else
        {
            Debug.Log("Invalid!!!");
            myLine = Instantiate(InvalidLine, posC, Quaternion.identity);
        }

        var lineCollider = myLine.GetComponent<BoxCollider>();

        myLine.localScale = new Vector3(lengthC, Ancho, Alto);
        myLine.rotation = Quaternion.Euler(0, 0, angleC);

        return myLine.gameObject;
    }
}
