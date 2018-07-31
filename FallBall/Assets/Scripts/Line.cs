using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    void Start()
    {
        Swipe.Taped += Swipe_Taped;
    }

    private void Swipe_Taped(Vector2 pos)
    {
        //Vector3 tmp = pos;

        //tmp.z = 90;

        //Debug.Log("pos: " + pos);
        //Debug.Log("tra: " + transform.position);




        //////transform.
        //////if (t.x - scale.x <= pos.x && t.x + scale.x >= pos.x &&
        //////    t.y - scale.y <= pos.y && t.y + scale.y >= pos.y)
        //////    Destroy();

        ////var ray = Camera.main.ScreenPointToRay(tmp);

        ////Debug.DrawRay(pos , tmp);
        //RaycastHit hit;

        ////if (Physics.Raycast(tmp, new Vector3(0,0,-10000), out hit))
        ////{
        ////    Debug.Log("Hit!");
        ////    if (hit.transform.gameObject == gameObject)
        ////    {
        ////        Destroy();
        ////    }
        ////}

        ////transform.GetComponent<BoxCollider2D>().bounds.Contains(pos);


        //var hitCollider = Physics.OverlapSphere(tmp, 1);

        //if(hitCollider.Length > 0)
        //{
        //    Debug.Log("HIT");
        //}
    }

    // Update is called once per frame
    void Update()
    { 
        var dist = Vector3.Distance(PlayerController.FirstPlayer.transform.position, transform.position);

        if (dist > 100)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Swipe.Taped -= Swipe_Taped;
        Destroy(gameObject);
    }
}
