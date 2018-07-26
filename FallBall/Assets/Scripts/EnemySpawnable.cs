using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnable : BaseSpawnableItem
{
    private int value;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTER!");
        if (collision.transform.tag == "Player")
        {
            PlayerController.FirstPlayer.Die();
        }
    }
}
