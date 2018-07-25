using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawnableItem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Remove when 3 times playerheight over player
        if (PlayerController.FirstPlayer.transform.position.y < transform.position.y - 3 * PlayerController.FirstPlayer.transform.localScale.y)
            Destroy(gameObject);
    }
}
