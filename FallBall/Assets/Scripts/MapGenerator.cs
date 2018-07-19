using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] availableMaps;
    public List<GameObject> currentMaps;
    private float screenHeightInPoints;

    public static float RealHeight;

    public static float PixelPerSecond;

    // Use this for initialization
    void Start ()
    {
        //float height = 480 * 0.24f;

        screenHeightInPoints = 480;// 2.0f * Camera.main.orthographicSize;

        RealHeight = screenHeightInPoints * currentMaps.First().transform.localScale.y;

        StartCoroutine(GeneratorCheck());
    }

    void AddMap(float farthestMapEndY)
    {
        int randomMapIndex = Random.Range(0, availableMaps.Length);

        GameObject Map = (GameObject)Instantiate(availableMaps[randomMapIndex]);

        float MapHeight = Map.transform.localScale.y * screenHeightInPoints;//TODO Scale is not height

        float MapCenter = farthestMapEndY - MapHeight;
        Map.transform.position = new Vector3(0, MapCenter, 90);

        currentMaps.Add(Map);
    }

    private void GenerateMapIfRequired()
    {
        List<GameObject> MapsToRemove = new List<GameObject>();

        bool addMaps = true;

        float playerY = transform.position.y;

        float removeMapWhenGreaterY = playerY + screenHeightInPoints * currentMaps.First().transform.localScale.y * 2;

        float addMapWhenNothingSmallerY = playerY - screenHeightInPoints * currentMaps.First().transform.localScale.y;
        
        foreach (var Map in currentMaps)
        {
            float MapHeight = Map.transform.localScale.y * screenHeightInPoints;//TODO
            float MapStartY = Map.transform.position.y + (MapHeight * 0.5f);
            float MapEndY = MapStartY - MapHeight;

            //Map is there
            if (MapEndY < addMapWhenNothingSmallerY)
            {
                addMaps = false;
            }

            //Map can be removed, because out of screen
            if (MapStartY > removeMapWhenGreaterY)
            {
                MapsToRemove.Add(Map);
            }
        }

        foreach (var Map in MapsToRemove)
        {
            currentMaps.Remove(Map);
            Swipe.Instance.DrawEnded -= Map.GetComponent<MapModification>().Instance_DrawEnded;
            Destroy(Map);
        }

        if (addMaps)
        {
            AddMap(currentMaps.Last().transform.position.y);
        }
    }

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateMapIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }
}
