using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkSpawnable : BaseSpawnableItem
{
    private int value;
    
    // Use this for initialization
    void Start()
    {
        System.Random r = new System.Random();

        value = r.Next(0, 9);

        Renderer renderer = GetComponent<Renderer>();

        if(value >=0 && value <6) //Bronze 60%
        {
            renderer.material = Resources.Load<Material>("SpawnBronce");
            value = 5;
        }
        if(value >= 6 && value < 8) //Silver 20%
        {
            renderer.material = Resources.Load<Material>("SpawnSilver");
            value = 10;
        }
        else if (value == 8) //Gold 10%
        {
            renderer.material = Resources.Load<Material>("SpawnGold");
            value = 15;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
            InkManager.Instance.CurrentInk += value;
        }
    }
}
