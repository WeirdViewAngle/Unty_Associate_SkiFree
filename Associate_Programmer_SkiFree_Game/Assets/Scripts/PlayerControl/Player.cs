using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [System.Serializable]
    public struct PlayerStats
    {
        [Tooltip("Player Rotate Speed")]
        public float rotateSpeed;

        [Tooltip("Player lives")]
        public int lives;

        [Tooltip("Player default forward speed")]
        public float forwardSpeed;

        [Tooltip("Player maximum velocity")]
        public float maxPlayerVelocity;
    }

    public PlayerStats playerStats;

    [SerializeField] Rigidbody playerRB;

    void Update()
    {
        //Check for A/D input and rotate player
        CheckForInput();
    }

    private void FixedUpdate()
    {
        //Check max velocity and limits it
        MaxVelocityCheck();

        //Maintaining acceleration
        ForwardSpeedMaintain();
    }

    private void OnCollisionEnter(Collision collision)
    {
        UIManager._Instance.reduseHealthEvent.Invoke(1);
    }

    #region Player Movement
    void CheckForInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, playerStats.rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, -playerStats.rotateSpeed * Time.deltaTime);
        }
    }

    void MaxVelocityCheck()
    {
        if(playerRB.velocity.magnitude > playerStats.maxPlayerVelocity)
        {
            playerRB.velocity = playerRB.velocity.normalized * playerStats.maxPlayerVelocity;
        }
    }

    void ForwardSpeedMaintain()
    {
        playerRB.AddForce(transform.forward * playerStats.forwardSpeed * Time.deltaTime,
                          ForceMode.Impulse);
    }
    #endregion
}
