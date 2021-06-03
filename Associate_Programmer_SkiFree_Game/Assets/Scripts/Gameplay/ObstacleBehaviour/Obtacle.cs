using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obtacle : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("Collision with: " + collision.gameObject.name);
        }
    }
}
