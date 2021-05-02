using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public GameObject snowBall, player;
    public float throwDistance;
    public int throwSpeed;
    private bool justThown = false;

    void Start()
    {
        
    }

    void Update()
    {
       float distanceToTarget = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToTarget < throwDistance && !justThown)
        {
            justThown = true;
            GameObject tempSnowBall = Instantiate(snowBall,transform.position,transform.rotation);
            Rigidbody tempRb = tempSnowBall.GetComponent<Rigidbody>();
            Vector3 targetDirection = (player.transform.position-transform.position).normalized;
            
            //Add a small throw angle
            targetDirection += new Vector3(0, 0.33f, 0);
            tempRb.AddForce(targetDirection * throwSpeed);
            Invoke("ThrowOver", 1f);
        }

    }

    void ThrowOver()
    {
        justThown = false;
    }
}
