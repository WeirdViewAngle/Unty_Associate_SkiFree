using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstablePushBack : Obtacle
{
    public float pushBackForce;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            playerScript.characterDisableEvent.Invoke(pushBackForce);
        }
    }
}
