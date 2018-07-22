using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapModification : MonoBehaviour
{
    public static Transform Cube;
    public static float Ancho = 1;
    public static float Alto = 1;

    private static SphereCollider playerCollider;
    private static Material materialLineInvalid;

    private static float panelPosition = 90; //TODO hardcoded....

    // Use this for initialization
    void Start()
    {
        Swipe.Instance.DrawEnded += Instance_DrawEnded;
        Cube = Resources.Load<Transform>("Line");
        materialLineInvalid = Resources.Load<Material>("LineInvalid");
        playerCollider = PlayerController.Instances.First().GetComponent<SphereCollider>();//Works only with one player TODO
    }

    public void Instance_DrawEnded(Vector2 start, Vector2 end)
    {
        //Only when on own map is painted
        var playerCoords = PlayerController.Instances.First().transform.position;
        var halfHeight = MapGenerator.RealHeight / 2;

        if (transform.position.y >= (playerCoords.y - halfHeight) && transform.position.y <= (playerCoords.y + halfHeight))
        {
            Debug.Log("Draw is ended - Draw from " + start + "to " + end);

            DrawALine(start, end);
        }
    }

    public static GameObject DrawALine(Vector3 start, Vector3 end, bool collider = true)
    {
        end.z = panelPosition;
        start.z = panelPosition;

        Vector3 posC = ((end - start) * 0.5F) + start;
        float lengthC = (end - start).magnitude; //C#
        float sineC = (end.y - start.y) / lengthC;
        float angleC = Mathf.Asin(sineC) * Mathf.Rad2Deg;
        if (end.x < start.x) { angleC = 0 - angleC; }

        Transform myLine = Instantiate(Cube, posC, Quaternion.identity);
        var lineCollider = myLine.GetComponent<BoxCollider>();

        myLine.localScale = new Vector3(lengthC, Ancho, Alto);
        myLine.rotation = Quaternion.Euler(0, 0, angleC);

        Debug.Log(lineCollider.bounds);
        Debug.Log(PlayerController.Instances.First().transform.position);

        if (lineCollider.bounds.Contains(PlayerController.Instances.First().transform.position))
        {
            myLine.GetComponent<Renderer>().material = materialLineInvalid;

            if (collider)
                GameObject.Destroy(myLine.gameObject);
            else
                lineCollider.enabled = collider;
        }
        else
        {
            lineCollider.enabled = collider;
        }
        
        return myLine.gameObject;
    }
}
