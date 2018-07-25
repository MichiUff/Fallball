using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnable : BaseSpawnableItem
{
    private int value;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController.FirstPlayer.Die();
        }
    }
}
