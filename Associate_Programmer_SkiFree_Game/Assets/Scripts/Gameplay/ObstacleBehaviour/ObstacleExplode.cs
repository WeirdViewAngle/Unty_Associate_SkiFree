using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleExplode : Obtacle
{
    protected override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            base.OnCollisionEnter(collision);
            Destroy(gameObject);
        }
    }
}
