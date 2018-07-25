using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] spawnableItems;
    public GameObject[] availableMaps;
    public List<GameObject> currentMaps;
    private float screenHeightInPoints;
    private float screenWidthInPoints;

    public static float RealHeight;
    public static float RealWidth;

    public static float PixelPerSecond;

    // Use this for initialization
    void Start ()
    {
        //float height = 480 * 0.24f;

        //TODO
        screenHeightInPoints = 480;// 2.0f * Camera.main.orthographicSize; 
        screenWidthInPoints = 320;// 2.0f * Camera.main.orthographicSize;

        RealHeight = screenHeightInPoints * currentMaps.First().transform.localScale.y;
        RealWidth = screenWidthInPoints * currentMaps.First().transform.localScale.x;

        StartCoroutine(GeneratorCheck());
    }

    void AddMap(float farthestMapEndY)
    {
        int randomMapIndex = UnityEngine.Random.Range(0, availableMaps.Length);

        GameObject Map = (GameObject)Instantiate(availableMaps[randomMapIndex]);
        
        float MapCenter = farthestMapEndY - RealHeight;
        Map.transform.position = new Vector3(0, MapCenter, 90);

        SpawnSelectable(MapCenter);

        currentMaps.Add(Map);
    }

    private void SpawnSelectable(float mapCenterY)
    {
        var widthWithOffset = (float)(RealWidth/2 - 0.1 * RealWidth); 
        var x = UnityEngine.Random.Range(-widthWithOffset, widthWithOffset);
        var y = UnityEngine.Random.Range(mapCenterY - RealHeight/2, mapCenterY + RealHeight/2);
        
        int randomSpawnIndex = UnityEngine.Random.Range(0, spawnableItems.Length);
        GameObject item = Instantiate(spawnableItems[randomSpawnIndex]);
        item.transform.position = new Vector3(x, y, 90);
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

    void FixedUpdate()
    {
        GenerateMapIfRequired();

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
