using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkSpawn : MonoBehaviour
{
    PlayerController player;

    private int value;
    
    // Use this for initialization
    void Start()
    {
        player = PlayerController.FirstPlayer;

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

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist > 500)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
            InkManager.CurrentInk += value;
        }
    }
}
