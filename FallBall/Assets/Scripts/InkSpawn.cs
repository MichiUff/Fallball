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

        value = r.Next(5, 10);
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
